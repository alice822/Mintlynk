using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using MintLynk.Application.Dtos;
using MintLynk.Application.Helper;
using MintLynk.Application.Interfaces;
using MintLynk.Application.Services;
using MintLynk.Domain.Entities.SmartLink;
using MintLynk.Domain.Enums;
using MintLynk.Domain.Interfaces;
using MintLynk.Domain.Models;
using MintLynk.Web.Extensions;
using MintLynk.Web.Models;
using System.Diagnostics;
using System.Net;
using static QRCoder.PayloadGenerator;

namespace MintLynk.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUrlShorteningService _urlShorteningService;
        private readonly ILinkStatsService _linkStatsService;
        private readonly IConfiguration _configuration;
        private readonly LinkPreviewService _previewService;

        public HomeController(ILogger<HomeController> logger, IUrlShorteningService urlShorteningService, IConfiguration configuration, ILinkStatsService linkStatsService, LinkPreviewService previewService)
        {
            _logger = logger;
            _urlShorteningService = urlShorteningService;
            _configuration = configuration;
            _linkStatsService = linkStatsService;
            _previewService = previewService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> GenerateShortUrl(string originalUrl)
        {
            var appVariables = _configuration.GetSection("AppVariables").Get<AppVariables>();
            var response = new JsonModel();
            if (string.IsNullOrWhiteSpace(originalUrl))
            {
                response.ResponseMessage = "Original URL cannot be empty.";
                response.ResponseType = "Error";
                return Json(response);
            }
            
            var dataModel = new SmartLinkDto();
            dataModel.DestinationUrl = originalUrl;
            dataModel.LinkType = (int)SmartLinkType.ShortUrl;
            dataModel.CreatedBy = User.GetUserId();
            dataModel.LastModifiedBy = User.GetUserId();
            dataModel.Title = await UrlHelper.GetPageTitle(originalUrl);

            var shortLink = await _urlShorteningService.QuickShortUrl(dataModel);
            if (shortLink == null)
            {
                response.ResponseMessage = "Failed to create short URL.";
                response.ResponseType = "Error";
                return Json(response);
            }
            var domain = appVariables != null ? appVariables.Domain : "";
            var qrCode = QrCodeHelper.GenerateQrCode(domain, shortLink.ShortUrl);
            var shortUrl = $"{domain}/{shortLink.ShortUrl}";
            response.ResponseMessage = "Your short URL is ready now.";
            response.ResponseType = "Success";
            response.JsonContent = shortUrl;
            response.QrCodeImage = qrCode; // Add this property to JsonModel

            return Json(response);
        }

        [HttpGet("{shortUrl}")]
        public async Task<IActionResult> ShortUrlRedirect(string shortUrl)
        {
            if (string.IsNullOrWhiteSpace(shortUrl))
            {
                _logger.LogWarning("ShortUrlRedirect called with empty shortUrl.");
                return RedirectToAction("Error", new { message = "Invalid short URL." });
            }

            var modelData = await _urlShorteningService.GetAsync(shortUrl, MintLynk.Domain.Enums.SmartLinkLookupType.ShortUrl);
            if (modelData == null)
            {
                _logger.LogWarning("Short URL not found: {ShortUrl}", shortUrl);
                return NotFound();
            }

            try
            {
                var userDetail = CommonHelper.GetVisitorDetail(HttpContext);
                userDetail.LinkId = modelData.EntityId;
                userDetail.LinkType = SmartLinkType.ShortUrl.ToString();
                userDetail.Referrer = !string.IsNullOrEmpty(userDetail.Referrer) ? userDetail.Referrer : "None";
                await _linkStatsService.CreateAsync(userDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error logging visitor details for shortUrl: {ShortUrl}", shortUrl);
            }

            if (!Uri.IsWellFormedUriString(modelData.DestinationUrl, UriKind.Absolute))
            {
                _logger.LogWarning("Invalid destination URL for shortUrl: {ShortUrl}", shortUrl);
                return RedirectToAction("Error", new { message = "Invalid destination URL." });
            }

            modelData.DestinationUrl = WebUtility.HtmlDecode(modelData.DestinationUrl);

            // Generate a random number between 1 and 100
            var random = new Random();
            int chance = random.Next(1, 101); // 1 to 100

            bool showAdPage = chance <= 70; // 70% chance to show the ad page

            var linkPreview = await _previewService.GetPreviewAsync(modelData.DestinationUrl);

            var viewModel = new AdsPreviewDto
            {
                DestinationUrl = modelData.DestinationUrl,
                IsLinkExpired = false,
                LinkPreview = linkPreview
            };

            if (showAdPage)
            {
                return View("AdPage", viewModel);
            }

           return RedirectPermanent(modelData.DestinationUrl);
        }

        public static async Task<(string title, string description)> GetTitleAndDescriptionAsync(string url)
        {
            try
            {
                using var client = new HttpClient();
                var html = await client.GetStringAsync(url);

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);

                var title = htmlDoc.DocumentNode.SelectSingleNode("//title")?.InnerText?.Trim() ?? "No title";

                var descNode = htmlDoc.DocumentNode.SelectSingleNode("//meta[@name='description']") ??
                               htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:description']");
                var description = descNode?.GetAttributeValue("content", "") ?? "No description";

                return (title, description);
            }
            catch
            {
                return ("", "");
            }
        }

        [HttpGet("qr/{shortUrl}")]
        public async Task<IActionResult> QrCode(string shortUrl)
        {
            if (string.IsNullOrWhiteSpace(shortUrl))
            {
                _logger.LogWarning("QrCode called with empty shortUrl.");
                return RedirectToAction("Error", new { message = "Invalid QR code link." });
            }

            var modelData = await _urlShorteningService.GetAsync(shortUrl, MintLynk.Domain.Enums.SmartLinkLookupType.ShortUrl);
            if (modelData == null)
            {
                _logger.LogWarning("Short URL not found: {ShortUrl}", shortUrl);
                return RedirectToAction("Error", new { message = "Short URL not found." });
            }

            try
            {
                var userDetail = CommonHelper.GetVisitorDetail(HttpContext);
                userDetail.LinkId = modelData.EntityId;
                userDetail.LinkType = SmartLinkType.QrCode.ToString();
                await _linkStatsService.CreateAsync(userDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error logging visitor details for shortUrl: {ShortUrl}", shortUrl);
            }

            if (!Uri.IsWellFormedUriString(modelData.DestinationUrl, UriKind.Absolute))
            {
                _logger.LogWarning("Invalid destination URL for shortUrl: {ShortUrl}", shortUrl);
                return RedirectToAction("Error", new { message = "Invalid destination URL." });
            }

            return RedirectPermanent(modelData.DestinationUrl);
        }

        [Route("privacy-policy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("cookie-policy")]
        public IActionResult Cookie()
        {
            return View();
        }

        [Route("terms")]
        public IActionResult Terms()
        {
            return View();
        }

        [Route("why/free")]
        public IActionResult ItsFree()
        {
            return View();
        }

        [Route("why/secure")]
        public IActionResult ItsSecure()
        {
            return View();
        }

        [Route("why/easy-to-use")]
        public IActionResult EasyToUse()
        {
            return View();
        }

        [Route("why/simple-powerful")]
        public IActionResult SimplePowerful()
        {
            return View();
        }

        [Route("about-us")]
        public IActionResult AboutUs()
        {
            return View();
        }

        [Route("partners")]
        public IActionResult Partners()
        {
            return View();
        }

        [Route("features/short-links")]
        public IActionResult ShortLinks()
        {
            return View();
        }

        [Route("features/qr-generator")]
        public IActionResult QrGenerator()
        {
            return View();
        }

        [Route("features/landing-pages")]
        public IActionResult LandingPages()
        {
            return View();
        }

        [Route("features/link-stats")]
        public IActionResult LinkStats()
        {
            return View();
        }

        [Route("solution/retail")]
        public IActionResult Retail()
        {
            return View();
        }

        [Route("solution/hospitality")]
        public IActionResult Hospitality()
        {
            return View();
        }

        [Route("solution/healthcare")]
        public IActionResult Healthcare()
        {
            return View();
        }

        [Route("solution/digital-marketing")]
        public IActionResult DigitalMarketing()
        {
            return View();
        }

        [Route("solution/professional-services")]
        public IActionResult ProfessionalServices()
        {
            return View();
        }

        [Route("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("error/404")]
        public IActionResult Error404() => View("Error404");

        [Route("error/500")]
        public IActionResult Error500() => View("Error500");

        [Route("error/403")]
        public IActionResult Error403() => View("Error403");
    }
}
