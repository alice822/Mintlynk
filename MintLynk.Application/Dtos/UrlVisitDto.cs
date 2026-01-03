using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintLynk.Application.Dtos
{
    public class UrlVisitDto
    {
        public string LinkId { get; set; }

        public string VisitorIp { get; set; } = string.Empty;

        public string UserAgent { get; set; } = string.Empty;

        public string Referrer { get; set; } = string.Empty;

        public string OperatingSystem { get; set; } = string.Empty;

        public string Browser { get; set; } = string.Empty;

        public string BrowserVersion { get; set; } = string.Empty;

        public string DeviceType { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string LinkType { get; set; } = string.Empty;

        public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;

        public string? CreatedBy { get; set; }

        public DateTimeOffset LastModified { get; set; }

        public string? LastModifiedBy { get; set; }
    }
}
