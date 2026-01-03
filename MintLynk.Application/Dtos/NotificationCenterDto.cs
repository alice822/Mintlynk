using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MintLynk.Application.Dtos 
{
    public partial class NotificationCenterDto
    {
        public new Guid Id { get; set; }

        public int? Priority { get; set; }
        public int? ChannelType { get; set; }
        public int? NotificationType { get; set; }
        public int? ApplicationArea { get; set; }
        
        public string? Entity { get; set; }
        public Guid? EntityId { get; set; }
        public string? FromEmail { get; set; }
        public string? FromName { get; set; }
        public string? ToEmail { get; set; }
        public string? ToName { get; set; }
        public string? ReplyToEmail { get; set; }
        public string? ReplyToName { get; set; }
        public string? CcEmail { get; set; }
        public string? BccEmail { get; set; }
        public string? AttachmentLocation { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public string? Link { get; set; }

        public bool? SendImmediately { get; set; }

        public int? RecheckCount { get; set; }
        public DateTime? ReminderDuration { get; set; }
        public Guid? GroupId { get; set; }
        public Guid? ReceiverId { get; set; }
        public Guid? SenderId { get; set; }
        public int? Status { get; set; }

        public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;

        public string? CreatedBy { get; set; }

        public DateTimeOffset LastModified { get; set; }

        public string? LastModifiedBy { get; set; }
    }
}
