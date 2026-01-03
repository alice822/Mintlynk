using HtmlAgilityPack;
using Microsoft.Extensions.Caching.Memory;
using MintLynk.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintLynk.Application.Services
{
    public class LinkPreviewService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;

        public LinkPreviewService(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
        }

        public async Task<LinkPreview> GetPreviewAsync(string url)
        {
            if (_cache.TryGetValue(url, out LinkPreview cachedPreview))
            {
                return cachedPreview;
            }

            try
            {
                using var client = new HttpClient();

                // Mimic a browser by setting headers
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/115.0.0.0 Safari/537.36");
                client.DefaultRequestHeaders.Accept.ParseAdd("text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("en-US,en;q=0.5");

                var response = await client.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();
                var doc = new HtmlDocument();
                doc.LoadHtml(content);

                var title = doc.DocumentNode.SelectSingleNode("//title")?.InnerText?.Trim();
                var description = doc.DocumentNode
                    .SelectSingleNode("//meta[@name='description']")?
                    .GetAttributeValue("content", null);

                var ogImage = doc.DocumentNode
                    .SelectSingleNode("//meta[@property='og:image']")?
                    .GetAttributeValue("content", null);

                var snippet = doc.DocumentNode
                    .SelectSingleNode("//p")?.InnerText?.Trim();

                var favicon = $"https://www.google.com/s2/favicons?domain={new Uri(url).Host}&sz=64";

                var preview = new LinkPreview
                {
                    Title = title,
                    Description = description,
                    OgImage = ogImage,
                    FaviconUrl = favicon,
                    HtmlSnippet = snippet
                };

                _cache.Set(url, preview, TimeSpan.FromDays(30));

                return preview;
            }
            catch
            {
                return null;
            }
        }
    }
}
