using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintLynk.Application.Dtos
{
    public class LinkPreview
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? FaviconUrl { get; set; }
        public string? OgImage { get; set; }
        public string? HtmlSnippet { get; set; }
    }
}
