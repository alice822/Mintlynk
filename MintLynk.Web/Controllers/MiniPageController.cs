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
    public class MiniPageController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMiniPageService _miniPageService;
        private readonly ILinkStatsService _linkStatsService;

        public MiniPageController(ILogger<HomeController> logger, IConfiguration configuration, IMiniPageService miniPageService, ILinkStatsService linkStatsService)
        {
            _logger = logger;
            _configuration = configuration;
            _miniPageService = miniPageService;
            _linkStatsService = linkStatsService;
        }

        [HttpGet("p/{alias}")]
        public async Task<IActionResult> Index(string alias)
        {
            if (string.IsNullOrWhiteSpace(alias))
            {
                _logger.LogWarning("MiniPage alias is null or empty.");
                return RedirectToAction("Error", new { message = "Invalid mini page link." });
            }
            var miniPage = await _miniPageService.GetAsync(alias);
            if (miniPage == null)
            {
                _logger.LogWarning("MiniPage not found for alias: {Alias}", alias);
                return RedirectToAction("Error404");
            }
            try
            {
                var userDetail = CommonHelper.GetVisitorDetail(HttpContext);
                userDetail.LinkId = miniPage.Id.ToString();
                userDetail.LinkType = SmartLinkType.MiniPage.ToString();
                userDetail.Referrer = !string.IsNullOrEmpty(userDetail.Referrer) ? userDetail.Referrer : "None";
                await _linkStatsService.CreateAsync(userDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error logging visitor details for MiniPage with alias: {Alias}", alias);
            }

            return View(miniPage);
        }
    }
}
