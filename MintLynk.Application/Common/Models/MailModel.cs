using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintLynk.Application.Common.Models
{
    public class MailModel
    {
        public string FromEmail { get; set; } = string.Empty;
        public string FromName { get; set; } = string.Empty;
        public string ToEmail { get; set; } = string.Empty;
        public string ToName { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string MailBody { get; set; } = string.Empty;
        public string BccEmails { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;

        public string FileName { get; set; } = string.Empty;
        public byte[]? FileAttachment { get; set; }
    }
}
