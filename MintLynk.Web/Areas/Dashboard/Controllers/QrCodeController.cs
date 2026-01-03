using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MintLynk.Application.Dtos;
using MintLynk.Application.Extensions;
using MintLynk.Application.Helper;
using MintLynk.Application.Interfaces;
using MintLynk.Domain.Enums;
using MintLynk.Domain.Models;
using MintLynk.Web.Extensions;
using MintLynk.Web.Models;
using System;
using System.Text.Json;

namespace MintLynk.Web.Areas.Dashboard.Controllers
{
    [Authorize(Roles = "User")]
    [Area("Dashboard")]
    public class QrCodeController : Controller
    {
        private readonly ILogger<QrCodeController> _logger;
        private readonly IUrlShorteningService _urlShorteningService;
        private readonly ILinkStatsService _linkStatsService;
        private readonly IConfiguration _configuration;
        public QrCodeController(ILogger<QrCodeController> logger, IUrlShorteningService urlShorteningService, IConfiguration configuration, ILinkStatsService linkStatsService)
        {
            _logger = logger;
            _urlShorteningService = urlShorteningService;
            _configuration = configuration;
            _linkStatsService = linkStatsService;
        }

        [Route("app/dashboard/qr-codes")]
        public async Task<IActionResult> Index(string q, int page = 1, int pageSize = 15)
        {
            var appVariables = _configuration.GetSection("AppVariables").Get<AppVariables>();
            var userId = User.GetUserId() ?? "";
            var linksData = await _urlShorteningService.GetAllAsync(Guid.Parse(userId));

            if (!string.IsNullOrWhiteSpace(q))
            {
                linksData = linksData.Where(l =>
                    (!string.IsNullOrEmpty(l.Title) && l.Title.Contains(q, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(l.DestinationUrl) && l.DestinationUrl.Contains(q, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            }

            var viewModel = linksData.Where(x=>x.LinkType == (int)SmartLinkType.QrCode).ToList();

            // Pagination logic
            var totalItems = viewModel.Count;
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var pagedLinks = viewModel
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            var finalViewModel = pagedLinks.Select(dto =>
            {
                var viewDto = dto.ToSmartLinkViewDto();
                viewDto.ShortUrl = $"{appVariables?.Domain}/{viewDto.ShortUrl}";
                viewDto.QrCode = QrCodeHelper.GenerateQrCode(appVariables?.Domain ?? "", viewDto.ShortUrl);
                viewDto.TotalEngagement = _linkStatsService.GetAllAsync(dto.EntityId).Result.Count();
                return viewDto;
            }).Where(x => x.LinkType == (int)SmartLinkType.QrCode).ToList();

            return View(finalViewModel);
        }

        [HttpGet]
        [Route("app/dashboard/qr-codes/create")]
        public IActionResult Create()
        {
            var viewModel = new SmartLinkViewDto();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("app/dashboard/qr-codes/create")]
        public IActionResult Create(SmartLinkViewDto model)
        {
            if (!ModelState.IsValid) {
                ModelState.AddModelError("", "Please fill in all required fields.");
                return View(model);
            }
            var appVariables = _configuration.GetSection("AppVariables").Get<AppVariables>();
            if (string.IsNullOrWhiteSpace(model.DestinationUrl))
            {
                ModelState.AddModelError("", "Destination URL is required.");
                return View(model);
            }
            if (!UrlHelper.IsValidUrl(model.DestinationUrl))
            {
                ModelState.AddModelError("", "Please enter a valid URL (must start with http:// or https://).");
                return View(model);
            }

            var dataModel = new SmartLinkDto();
            dataModel.DestinationUrl = model.DestinationUrl;
            dataModel.LinkType = (int)SmartLinkType.QrCode;
            dataModel.CreatedBy = User.GetUserId();
            dataModel.LastModifiedBy = User.GetUserId();
            dataModel.Title = UrlHelper.GetPageTitle(model.DestinationUrl).Result;

            var shortLink = _urlShorteningService.CreateAsync(dataModel).Result;
            if (shortLink == null)
            {
                ModelState.AddModelError("", "Failed to create short URL.");
                return View(model);
            }
            var domain = appVariables != null ? appVariables.Domain : "";
            var qrCode = QrCodeHelper.GenerateQrCode(domain, shortLink.ShortUrl);
            var redirectUrl = $"{shortLink.EntityId}/detail";
            return Redirect(redirectUrl);
        }

        [Route("app/dashboard/qr-codes/{entityId}/detail")]
        public async Task<IActionResult> Detail(string entityId)
        {
            var appVariables = _configuration.GetSection("AppVariables").Get<AppVariables>();
            var userId = User.GetUserId() ?? "";
            var linksData = await _urlShorteningService.GetAsync(entityId, SmartLinkLookupType.Entity);
            
            if(linksData == null)
            {
                _logger.LogWarning($"Short link with EntityId {entityId} not found for user {userId}.");
                return NotFound();
            }
            if(linksData.LinkType != (int)SmartLinkType.QrCode)
            {
                _logger.LogWarning($"Short link with EntityId {entityId} is not a QR code.");
                return NotFound();
            }
            if (linksData.Status == (int)Status.Deleted)
            {
                _logger.LogWarning($"Short link with EntityId {entityId} has been deleted.");
                return NotFound();
            }
            if (linksData.CreatedBy != userId)
            {
                _logger.LogWarning($"User {userId} attempted to access a short link not created by them: {entityId}.");
                return Forbid();
            }

            var viewModel = linksData.ToSmartLinkViewDto();
            viewModel.ShortUrl = $"{appVariables?.Domain}/{viewModel.ShortUrl}";
            viewModel.QrCode = QrCodeHelper.GenerateQrCode(appVariables?.Domain ?? "", linksData.ShortUrl);

            var domain =UrlHelper.ExtractDomain(viewModel.DestinationUrl);
            var model = new LinkDetailViewModel
            {
                EntityId = viewModel.EntityId,
                ShortUrl = viewModel.ShortUrl,
                DestinationUrl = viewModel.DestinationUrl,
                Title = viewModel.Title ?? viewModel.ShortUrl,
                QrCode = QrCodeHelper.GenerateQrCode(appVariables?.Domain ?? "", linksData.ShortUrl),
                FavIconUrl = UrlHelper.FormatFavIcon(domain),
                CreatedAt = viewModel.Created,
                // UtmParameters = linksData.UtmParameters != null ? JsonSerializer.Deserialize<UtmParameters>(linksData.UtmParameters) : new UtmParameters();
            };

            var linkStats = await _linkStatsService.GetAllAsync(entityId);
            if (linkStats != null)
            {
                model.TotalEngagement = linkStats.Count();
                model.TotalQrScan = linkStats.Count(x => x.LinkType == SmartLinkType.QrCode.ToString());
                model.TotalLinkClick = linkStats.Count(x => x.LinkType == SmartLinkType.ShortUrl.ToString());
                model.SevenDaysEngagement = linkStats.Count(x => x.Created >= DateTimeOffset.UtcNow.AddDays(-7));

                // Group by browser
                model.Reports.BrowserReports = linkStats
                    .GroupBy(x => string.IsNullOrWhiteSpace(x.Browser) ? "Unknown" : x.Browser)
                    .Select(g => new BrowserReportViewModel
                    {
                        Name = g.Key,
                        Engagements = g.LongCount(),
                        Percentage = model.TotalEngagement > 0
                            ? Math.Round((decimal)g.LongCount() * 100 / model.TotalEngagement, 2)
                            : 0
                    })
                    .OrderBy(b => b.Name)
                    .ToList();

                // Group by device type
                model.Reports.DeviceReport = linkStats
                    .GroupBy(x => string.IsNullOrWhiteSpace(x.DeviceType) ? "Unknown" : x.DeviceType)
                    .Select(g => new DeviceReportViewModel
                    {
                        Name = g.Key,
                        Engagements = g.LongCount(),
                        Percentage = model.TotalEngagement > 0
                            ? Math.Round((decimal)g.LongCount() * 100 / model.TotalEngagement, 2)
                            : 0
                    })
                    .OrderBy(d => d.Name)
                    .ToList();

                // Group by operating system
                model.Reports.OsReport = linkStats
                    .GroupBy(x => string.IsNullOrWhiteSpace(x.OperatingSystem) ? "Unknown" : x.OperatingSystem)
                    .Select(g => new OperatingSystemReportViewModel
                    {
                        Name = g.Key,
                        Engagements = g.LongCount(),
                        Percentage = model.TotalEngagement > 0
                            ? Math.Round((decimal)g.LongCount() * 100 / model.TotalEngagement, 2)
                            : 0
                    })
                    .OrderBy(o => o.Name)
                    .ToList();

                // Group by country
                model.Reports.CountryReport = linkStats
                    .GroupBy(x => string.IsNullOrWhiteSpace(x.Country) ? "Unknown" : x.Country)
                    .Select(g => new CountryReportViewModel
                    {
                        Name = g.Key,
                        Engagements = g.LongCount(),
                        Percentage = model.TotalEngagement > 0
                            ? Math.Round((decimal)g.LongCount() * 100 / model.TotalEngagement, 2)
                            : 0
                    })
                    .OrderByDescending(c => c.Engagements)
                    .ToList();

                // Group by city (assuming x.Location is city, adjust if you have a separate City property)
                model.Reports.CityReport = linkStats
                    .GroupBy(x => string.IsNullOrWhiteSpace(x.Location) ? "Unknown" : x.Location)
                    .Select(g => new CityReportViewModel
                    {
                        Name = g.Key,
                        Engagements = g.LongCount(),
                        Percentage = model.TotalEngagement > 0
                            ? Math.Round((decimal)g.LongCount() * 100 / model.TotalEngagement, 2)
                            : 0
                    })
                    .OrderByDescending(c => c.Engagements)
                    .ToList();

                // Group by date (using only the date part of Created)
                model.Reports.DateReport = linkStats
                    .GroupBy(x => x.Created.Date)
                    .Select(g => new DateReportViewModel
                    {
                        CreatedAt = g.Key.Date,
                        Date = g.Key.ToString("MMM-dd"),
                        Count = g.LongCount(),
                        QrScan = g.LongCount(x=>x.LinkType == SmartLinkType.QrCode.ToString()),
                        LinkClick = g.LongCount(x => x.LinkType == SmartLinkType.ShortUrl.ToString())
                    })
                    .OrderBy(d => d.CreatedAt) // Chronological order
                    .ToList();
            }
            else
            {
                _logger.LogWarning($"No link stats found for EntityId {entityId}.");
            }


            return View(model);
        }

        [HttpGet]
        [Route("app/dashboard/qr-codes/{entityId}/edit")]
        public async Task<IActionResult> Edit(string entityId)
        {
            var appVariables = _configuration.GetSection("AppVariables").Get<AppVariables>();
            var userId = User.GetUserId() ?? "";
            var linksData = await _urlShorteningService.GetAsync(entityId, SmartLinkLookupType.Entity);

            if (linksData == null)
            {
                _logger.LogWarning($"Short link with EntityId {entityId} not found for user {userId}.");
                return NotFound();
            }
            if (linksData.Status == (int)Status.Deleted)
            {
                _logger.LogWarning($"Short link with EntityId {entityId} has been deleted.");
                return NotFound();
            }
            if (linksData.CreatedBy != userId)
            {
                _logger.LogWarning($"User {userId} attempted to access a short link not created by them: {entityId}.");
                return Forbid();
            }

            var viewModel = linksData.ToSmartLinkViewDto();
            viewModel.ShortUrl = $"/{viewModel.ShortUrl}";
            viewModel.QrCode = QrCodeHelper.GenerateQrCode(appVariables?.Domain ?? "", viewModel.ShortUrl);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("app/dashboard/qr-codes/{entityId}/edit")]
        public IActionResult Edit(string entityId, SmartLinkViewDto model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please fill in all required fields.");
                return View(model);
            }
            var appVariables = _configuration.GetSection("AppVariables").Get<AppVariables>();
            if (string.IsNullOrWhiteSpace(model.DestinationUrl))
            {
                ModelState.AddModelError("", "Destination URL is required.");
                return View(model);
            }
            if (!UrlHelper.IsValidUrl(model.DestinationUrl))
            {
                ModelState.AddModelError("", "Please enter a valid URL (must start with http:// or https://).");
                return View(model);
            }

            var data = _urlShorteningService.GetAsync(model.EntityId ?? "", SmartLinkLookupType.Entity).Result;
            if (data != null)
            {
                data.Title = model.Title ?? UrlHelper.GetPageTitle(model.DestinationUrl).Result; ;
                data.UtmParameters = model.UtmParameters != null ? JsonSerializer.Serialize(model.UtmParameters) : string.Empty;
                data.LastModified = DateTime.UtcNow;
                data.Created = data.Created;
                data.CreatedBy = data.CreatedBy;
                data.LastModifiedBy = User.GetUserId();
                var shortLink = _urlShorteningService.UpdateAsync(data).Result;
                if (shortLink == null)
                {
                    ModelState.AddModelError("", "Failed to update short URL.");
                    return View(model);
                }
            }
            var domain = appVariables != null ? appVariables.Domain : "";
            var qrCode = QrCodeHelper.GenerateQrCode(domain, model.ShortUrl ?? "");
            var redirectUrl = $"/app/dashboard/qr-codes/{model.EntityId}/detail";
            return Redirect(redirectUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("app/dashboard/qr-codes/{entityId}/delete")]
        public IActionResult Delete(string entityId)
        {
            var response = new JsonModel();

            var link = _urlShorteningService.GetAsync(entityId, SmartLinkLookupType.Entity).Result;
            if (link == null)
            {
                response.ResponseType = "error";
                response.ResponseMessage = "Failed to delete QR code";
                return Json(response);
            }

            if (link.LinkType != (int)SmartLinkType.QrCode)
            {
                response.ResponseType = "error";
                response.ResponseMessage = "Failed to delete QR code.";
                return Json(response);
            }

            var data = _urlShorteningService.Delete(entityId).Result;
            if ((data))
            {
                response.ResponseType = "success";
                response.ResponseMessage = "QR code deleted successfully.";
                return Json(response);
            }
            else
            {
                response.ResponseType = "error";
                response.ResponseMessage = "Failed to delete QR code.";

            }
            return Json(response);
        }
    }
}
