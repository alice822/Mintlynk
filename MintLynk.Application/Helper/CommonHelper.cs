using Microsoft.AspNetCore.Http;
using MintLynk.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MintLynk.Application.Helper
{
    public static class CommonHelper
    {
        public static UrlVisitDto GetVisitorDetail(HttpContext context)
        {
            var visitorIp = GetVisitorIp(context);
            var location = GetLocation(visitorIp).Result;
            var country = GetCountry(visitorIp).Result;
            var (browser, os, userAgent) = GetBrowserAndOs(context);
            return new UrlVisitDto
            {
                VisitorIp = visitorIp,
                UserAgent = userAgent,
                Referrer = context.Request.Headers["Referer"].ToString(),
                OperatingSystem = os,
                Browser = browser,
                BrowserVersion = context.Request.Headers["User-Agent"].ToString(),
                DeviceType = GetDeviceType(context),
                Location = location,
                Country = country,
                Created = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                CreatedBy = "System",
                LastModifiedBy = "System"                
            };
        }
        private static string GetVisitorIp(HttpContext httpContext)
        {
            return httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        }

        private static async Task<string> GetLocation(string ipAddress)
        {
            try
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.GetStringAsync($"https://ipinfo.io/{ipAddress}/json");
                dynamic locationData = JsonSerializer.Deserialize<dynamic>(response);
                return locationData?.city ?? "Unknown";
            }
            catch (Exception)
            {
                // Log the exception if needed
                return "Unknown";
            }
        }

        private static (string Browser, string OperatingSystem, string userAgent) GetBrowserAndOs(HttpContext httpContext)
        {
            var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
            var browser = string.Empty;
            var os = string.Empty;

            if (userAgent.Contains("Windows")) os = "Windows";
            else if (userAgent.Contains("Macintosh")) os = "macOS";
            else if (userAgent.Contains("Linux")) os = "Linux";
            else os = "Unknown OS";

            if (userAgent.Contains("Chrome")) browser = "Chrome";
            else if (userAgent.Contains("Firefox")) browser = "Firefox";
            else if (userAgent.Contains("Safari")) browser = "Safari";
            else if (userAgent.Contains("Edge")) browser = "Edge";
            else browser = "Unknown";

            return (browser, os, userAgent);
        }

        public static string GetDeviceType(HttpContext httpContext)
        {
            var userAgent = httpContext.Request.Headers["User-Agent"].ToString().ToLower();

            if (userAgent.Contains("mobile"))
            {
                if (userAgent.Contains("tablet") || userAgent.Contains("ipad"))
                    return "Tablet";
                return "Mobile";
            }

            if (userAgent.Contains("bot") || userAgent.Contains("crawl") || userAgent.Contains("spider"))
            {
                return "Bot";
            }

            return "Desktop";
        }

        private static async Task<string> GetCountry(string ipAddress)
        {
            try
            {
                var url = $"http://www.geoplugin.net/json.gp?ip={ipAddress}";
                using var httpClient = new HttpClient();
                var response = await httpClient.GetStringAsync(url);

                using var doc = JsonDocument.Parse(response);
                if (doc.RootElement.TryGetProperty("geoplugin_countryName", out var country))
                {
                    return country.GetString() ?? "Unknown";
                }
            }
            catch(Exception)
            {
                // Log the exception if needed
            }

            return "Unknown";
        }
    }
}
