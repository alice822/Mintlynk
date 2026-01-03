using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintLynk.Domain.Entities
{
    public class LinkHistory : BaseEntity
    {
        public string LinkId { get; set; } = string.Empty;

        public string UserIp { get; set; } = string.Empty;

        public string UserAgent { get; set; } = string.Empty;

        public string Referrer { get; set; } = string.Empty;

        public string OperatingSystem { get; set; } = string.Empty;

        public string Browser { get; set; } = string.Empty;

        public string BrowserVersion { get; set; } = string.Empty;

        public string DeviceType { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string LinkType { get; set; } = string.Empty;
    }
}
