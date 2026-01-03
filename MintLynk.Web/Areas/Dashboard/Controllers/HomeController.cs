using Microsoft.AspNetCore.Mvc;
using MintLynk.Application.Helper;
using MintLynk.Application.Interfaces;
using MintLynk.Domain.Enums;
using MintLynk.Web.Areas.Dashboard.Models;
using MintLynk.Web.Extensions;
using MintLynk.Web.Models;

namespace MintLynk.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUrlShorteningService _urlShorteningService;
        private readonly ILinkStatsService _linkStatsService;
        private readonly IConfiguration _configuration;
        public HomeController(ILogger<HomeController> logger, IUrlShorteningService urlShorteningService, IConfiguration configuration, ILinkStatsService linkStatsService)
        {
            _logger = logger;
            _urlShorteningService = urlShorteningService;
            _configuration = configuration;
            _linkStatsService = linkStatsService;
        }
        public IActionResult Index()
        {
            var model = new DashboardViewModel();
            var appVariables = _configuration.GetSection("AppVariables").Get<AppVariables>();
            var userId = User.GetUserId();

            var userLinks = _urlShorteningService.GetAllAsync(Guid.Parse(userId ?? "")).Result;
            var entityIds = userLinks.Select(x => x.EntityId).Distinct().ToList();
            if (entityIds.Count > 0)
            {
                var linkStats = _linkStatsService.GetAllAsync(entityIds).Result;

                if (linkStats != null)
                {
                    model.Reports.TotalEngagement = linkStats.Count();
                    model.Reports.TotalQrScan = linkStats.Count(x => x.LinkType == SmartLinkType.QrCode.ToString());
                    model.Reports.TotalLinkClick = linkStats.Count(x => x.LinkType == SmartLinkType.ShortUrl.ToString());
                    model.Reports.SevenDaysEngagement = linkStats.Count(x => x.Created >= DateTimeOffset.UtcNow.AddDays(-7));

                    // Group by browser
                    model.Reports.BrowserReports = linkStats
                        .GroupBy(x => string.IsNullOrWhiteSpace(x.Browser) ? "Unknown" : x.Browser)
                        .Select(g => new BrowserReportViewModel
                        {
                            Name = g.Key,
                            Engagements = g.LongCount(),
                            Percentage = model.Reports.TotalEngagement > 0
                                ? Math.Round((decimal)g.LongCount() * 100 / model.Reports.TotalEngagement, 2)
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
                            Percentage = model.Reports.TotalEngagement > 0
                                ? Math.Round((decimal)g.LongCount() * 100 / model.Reports.TotalEngagement, 2)
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
                            Percentage = model.Reports.TotalEngagement > 0
                                ? Math.Round((decimal)g.LongCount() * 100 / model.Reports.TotalEngagement, 2)
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
                            Percentage = model.Reports.TotalEngagement > 0
                                ? Math.Round((decimal)g.LongCount() * 100 / model.Reports.TotalEngagement, 2)
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
                            Percentage = model.Reports.TotalEngagement > 0
                                ? Math.Round((decimal)g.LongCount() * 100 / model.Reports.TotalEngagement, 2)
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
                            QrScan = g.LongCount(x => x.LinkType == SmartLinkType.QrCode.ToString()),
                            LinkClick = g.LongCount(x => x.LinkType == SmartLinkType.ShortUrl.ToString())
                        })
                        .OrderBy(d => d.CreatedAt) // Chronological order
                        .ToList();

                    model.TopLinks = userLinks.Select(link =>
                    {
                        var totalEngagement = linkStats.Where(stat => stat.LinkId == link.EntityId).Count();
                        return new TopLinkViewModel
                        {
                            EntityId = link.EntityId,
                            Title = link.Title,
                            ShortUrl = $"{appVariables?.Domain}/{link.ShortUrl}",
                            DestinationUrl = link.DestinationUrl,
                            FavIconUrl = UrlHelper.FormatFavIcon(UrlHelper.ExtractDomain(link.DestinationUrl)),
                            CreatedAt = link.Created,
                            TotalEngagement = totalEngagement
                        };
                    })
                    .OrderByDescending(x => x.TotalEngagement)
                    .Take(5) // Top 5 links, adjust as needed
                    .ToList();

                    // Calculate start of this week (Monday)
                    var today = DateTimeOffset.UtcNow.Date;
                    int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
                    var thisWeekStart = today.AddDays(-1 * diff);
                    var lastWeekStart = thisWeekStart.AddDays(-7);
                    var lastWeekEnd = thisWeekStart.AddSeconds(-1);

                    // This week: from thisWeekStart to now
                    model.Reports.ThisWeekEngageent = linkStats.Count(x => x.Created >= thisWeekStart);

                    // Last week: from lastWeekStart to lastWeekEnd
                    model.Reports.LastWeekEngageent = linkStats.Count(x => x.Created >= lastWeekStart && x.Created < thisWeekStart);

                    long last = model.Reports.LastWeekEngageent;
                    long current = model.Reports.ThisWeekEngageent;
                    decimal growth = last == 0 ? (current > 0 ? 100 : 0) : ((decimal)(current - last) / last) * 100;
                    model.Reports.WeeklyGrowthRate = Math.Round(growth, 2);

                    // Build SevenDaysReport: last 7 days, one entry per day (chronological order)
                    var last7Days = Enumerable.Range(0, 7)
                        .Select(i => DateTimeOffset.UtcNow.Date.AddDays(-6 + i))
                        .ToList();

                    model.Reports.SevenDaysReport = last7Days
                        .Select(day => new DateReportViewModel
                        {
                            CreatedAt = day,
                            Date = day.ToString("MMM-dd"),
                            Count = linkStats.Count(x => x.Created.Date == day),
                            QrScan = linkStats.Count(x => x.Created.Date == day && x.LinkType == SmartLinkType.QrCode.ToString()),
                            LinkClick = linkStats.Count(x => x.Created.Date == day && x.LinkType == SmartLinkType.ShortUrl.ToString())
                        })
                        .ToList();
                }
            }
            
            return View(model);
        }
    }
}
