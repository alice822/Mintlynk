using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintLynk.Application.Dtos
{
    public class AdsPreviewDto
    {
        public AdsPreviewDto()
        {
            LinkPreview = new LinkPreview();
        }

        public bool IsLinkExpired { get; set; }

        public LinkPreview LinkPreview { get; set; }

        public string DestinationUrl { get; set; }
    }
}
