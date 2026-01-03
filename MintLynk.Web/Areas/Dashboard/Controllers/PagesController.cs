using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
using System.Threading.Tasks;

namespace MintLynk.Web.Areas.Dashboard.Controllers
{
    [Authorize(Roles = "User")]
    [Area("Dashboard")]
    public class PagesController : Controller
    {
        private readonly ILogger<PagesController> _logger;
        private readonly IUrlShorteningService _urlShorteningService;
        private readonly ILinkStatsService _linkStatsService;
        private readonly IConfiguration _configuration;
        private readonly IMiniPageService _miniPageService;

        private const string TemplatesCacheKey = "TemplatesCacheKey";
        private readonly IMemoryCache _cache;

        public PagesController(ILogger<PagesController> logger, IUrlShorteningService urlShorteningService, IConfiguration configuration, ILinkStatsService linkStatsService, IMiniPageService miniPageService, IMemoryCache cache)
        {
            _logger = logger;
            _urlShorteningService = urlShorteningService;
            _configuration = configuration;
            _linkStatsService = linkStatsService;
            _miniPageService = miniPageService;
            _cache = cache;
        }

        [Route("app/dashboard/landing-pages")]
        public async Task<IActionResult> Index(string q, int page = 1, int pageSize = 15)
        {
            var appVariables = _configuration.GetSection("AppVariables").Get<AppVariables>();
            var userId = User.GetUserId() ?? "";
            var linksData = await _miniPageService.GetAllAsync(userId);

            if (!string.IsNullOrWhiteSpace(q))
            {
                linksData = linksData.Where(l =>
                    (!string.IsNullOrEmpty(l.Title) && l.Title.Contains(q, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            }

            var viewModel = linksData.ToList();

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
                var viewDto = dto;
                return viewDto;
            }).ToList();

            return View(finalViewModel);
        }

        public class ProfessionalProfileServiceViewModel
        {
            public ProfessionalProfile? ProfessionalProfile { get; set; } = new();
            public ProfessionalService? ProfessionalService { get; set; } = new();
            
            public bool? IsTemplateSelected { get; set; } = false;
            
            public List<TemplateType>? Templates { get; set; }

            public string? SelectedTemplateAlias { get; set; }
        }

        [HttpGet]
        [Route("app/dashboard/landing-pages/create/")]
        public IActionResult Create()
        {
            var viewModel = new ProfessionalProfileServiceViewModel
            {
                Templates = GetTemplatesFromJson()
            };

            return View(viewModel);
        }


        [HttpGet]
        [Route("app/dashboard/landing-pages/create/{templateAlias}")]
        public IActionResult Create(string? templateAlias)
        {
            var viewModel = new ProfessionalProfileServiceViewModel
            {
                IsTemplateSelected = true,
                SelectedTemplateAlias = templateAlias
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("app/dashboard/landing-pages/create/{templateAlias}")]
        public async Task<IActionResult> Create(ProfessionalProfileServiceViewModel model, IFormFile BannerImageFile, IFormFile ImageUrlFile)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please fill in all required fields.");
                return View(model);
            }

            var userId = User.GetUserId();

            // Handle Banner Image upload
            if (BannerImageFile != null && BannerImageFile.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(BannerImageFile.FileName)}";
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);
                var filePath = Path.Combine(uploadPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await BannerImageFile.CopyToAsync(stream);
                }
                model.ProfessionalProfile.BannerImage = "/uploads/" + fileName;
            }

            // Handle Profile Image upload
            if (ImageUrlFile != null && ImageUrlFile.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(ImageUrlFile.FileName)}";
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);
                var filePath = Path.Combine(uploadPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageUrlFile.CopyToAsync(stream);
                }
                model.ProfessionalProfile.ImageUrl = "/uploads/" + fileName;
            }

            // Map to MiniPageDto and save as before
            var profileDto = new MiniPageDto
            {
                Id = Guid.NewGuid(),
                Type = 1,
                Title = model.ProfessionalProfile.Title,
                CreatedBy = userId,
                LastModifiedBy = userId,
                Created = DateTimeOffset.UtcNow,
                Modified = DateTimeOffset.UtcNow,
                Status = (int)MiniPageStatus.Draft,
                Template = model.SelectedTemplateAlias,
                Alias=model.ProfessionalProfile.Alias                
            };

            var content = new ProfessionalProfile
            {
                Title = model.ProfessionalProfile.Title,
                Intro = model.ProfessionalProfile.Intro,
                Bio = model.ProfessionalProfile.Bio,
                BannerImage = model.ProfessionalProfile.BannerImage,
                ImageUrl = model.ProfessionalProfile.ImageUrl,
                ImageAlt = model.ProfessionalProfile.ImageAlt,
                Address = model.ProfessionalProfile.Address,
                ContactNumber = model.ProfessionalProfile.ContactNumber,
                Email = model.ProfessionalProfile.Email,
                Website = model.ProfessionalProfile.Website,
                SocialLinks = model.ProfessionalProfile.SocialLinks,
                Profession = model.ProfessionalProfile.Profession,
                Achievements = model.ProfessionalProfile.Achievements,
                Experience = model.ProfessionalProfile.Experience,
                Qualification = model.ProfessionalProfile.Qualification
            };
            profileDto.PageContent = JsonSerializer.Serialize(content, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

            var data = await _miniPageService.CreateAsync(profileDto);

            var redirectUrl = $"{data.Id}/detail";
            return Redirect(redirectUrl);
        }

        private List<TemplateType> GetTemplatesFromJson()
        {
            if (_cache.TryGetValue(TemplatesCacheKey, out List<TemplateType> cachedTemplates))
            {
                return cachedTemplates;
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "data", "pages", "templates.json");
            if (!System.IO.File.Exists(filePath))
            {
                _logger.LogWarning("Template JSON file not found at {FilePath}", filePath);
                return new List<TemplateType>();
            }

            var json = System.IO.File.ReadAllText(filePath);
            var templates = JsonSerializer.Deserialize<List<TemplateType>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<TemplateType>();

            // Cache for 30 minutes (adjust as needed)
            _cache.Set(TemplatesCacheKey, templates, TimeSpan.FromMinutes(30));
            return templates;
        }

        [HttpGet]
        [Route("app/dashboard/landing-pages/{entityId}/detail")]
        public async Task<IActionResult> Detail(string entityId)
        {
            var appVariables = _configuration.GetSection("AppVariables").Get<AppVariables>();
            var userId = User.GetUserId() ?? "";
            var linksData = await _urlShorteningService.GetAsync(entityId, SmartLinkLookupType.Entity);
            
            if(linksData == null)
            {
                _logger.LogWarning($"Page with EntityId {entityId} not found for user {userId}.");
                return NotFound();
            }
            if(linksData.LinkType != (int)SmartLinkType.QrCode)
            {
                _logger.LogWarning($"Page with EntityId {entityId} is not a QR code.");
                return NotFound();
            }
            if (linksData.Status == (int)Status.Deleted)
            {
                _logger.LogWarning($"Page with EntityId {entityId} has been deleted.");
                return NotFound();
            }
            if (linksData.CreatedBy != userId)
            {
                _logger.LogWarning($"User {userId} attempted to access a Page not created by them: {entityId}.");
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
        [Route("app/dashboard/landing-pages/{entityId}/edit")]
        public async Task<IActionResult> Edit(string entityId)
        {
            var viewModel = new ProfessionalProfileServiceViewModel
            {
                IsTemplateSelected = true,
            };

            var appVariables = _configuration.GetSection("AppVariables").Get<AppVariables>();
            var userId = User.GetUserId() ?? "";
            var linksData = await _miniPageService.GetAsync(Guid.Parse(entityId));

            if (linksData == null)
            {
                _logger.LogWarning($"Page with EntityId {entityId} not found for user {userId}.");
                return NotFound();
            }
            if (linksData.Status == (int)Status.Deleted)
            {
                _logger.LogWarning($"Page with EntityId {entityId} has been deleted.");
                return NotFound();
            }
            if (linksData.CreatedBy != userId)
            {
                _logger.LogWarning($"User {userId} attempted to access a Page not created by them: {entityId}.");
                return Forbid();
            }

            viewModel.SelectedTemplateAlias = linksData.Template;

            if (linksData.PageContent != null)
            {
                if(linksData.Template == "t2z01")
                {
                    viewModel.ProfessionalProfile = JsonSerializer.Deserialize<ProfessionalProfile>(linksData.PageContent, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        WriteIndented = true
                    });
                    viewModel.ProfessionalProfile.Alias = linksData.Alias;
                }
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("app/dashboard/landing-pages/{entityId}/edit")]
        public async Task<IActionResult> Edit(string entityId, ProfessionalProfileServiceViewModel model, IFormFile? BannerImageFile, IFormFile? ImageUrlFile)
        {
            ModelState.Remove("ProfessionalProfile.Alias");
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please fill in all required fields.");
                return View(model);
            }
            var userId = User.GetUserId();
            var appVariables = _configuration.GetSection("AppVariables").Get<AppVariables>();

            // Handle Banner Image upload
            if (BannerImageFile != null && BannerImageFile.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(BannerImageFile.FileName)}";
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);
                var filePath = Path.Combine(uploadPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await BannerImageFile.CopyToAsync(stream);
                }
                model.ProfessionalProfile.BannerImage = "/uploads/" + fileName;
            }

            // Handle Profile Image upload
            if (ImageUrlFile != null && ImageUrlFile.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(ImageUrlFile.FileName)}";
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);
                var filePath = Path.Combine(uploadPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageUrlFile.CopyToAsync(stream);
                }
                model.ProfessionalProfile.ImageUrl = "/uploads/" + fileName;
            }

            var existingPage = await _miniPageService.GetAsync(Guid.Parse(entityId));
            if(existingPage != null)
            {
                existingPage.Title = model.ProfessionalProfile.Title;
                existingPage.Modified = DateTime.UtcNow;
                existingPage.LastModifiedBy = userId;

                var content = new ProfessionalProfile
                {
                    Title = model.ProfessionalProfile.Title,
                    Intro = model.ProfessionalProfile.Intro,
                    Bio = model.ProfessionalProfile.Bio,
                    BannerImage = model.ProfessionalProfile.BannerImage,
                    ImageUrl = model.ProfessionalProfile.ImageUrl,
                    ImageAlt = model.ProfessionalProfile.ImageAlt,
                    Address = model.ProfessionalProfile.Address,
                    ContactNumber = model.ProfessionalProfile.ContactNumber,
                    Email = model.ProfessionalProfile.Email,
                    Website = model.ProfessionalProfile.Website,
                    SocialLinks = model.ProfessionalProfile.SocialLinks,
                    Profession = model.ProfessionalProfile.Profession,
                    Achievements = model.ProfessionalProfile.Achievements,
                    Experience = model.ProfessionalProfile.Experience,
                    Qualification = model.ProfessionalProfile.Qualification
                };
                existingPage.PageContent = JsonSerializer.Serialize(content, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                });

                await _miniPageService.UpdateAsync(existingPage);
                var redirectUrl = $"/app/dashboard/landing-pages/{entityId}/detail";
                return Redirect(redirectUrl);
            }

            return View(model);
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
