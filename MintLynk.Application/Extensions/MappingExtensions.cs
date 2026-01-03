using MintLynk.Application.Dtos;
using MintLynk.Application.Helper;
using MintLynk.Domain.Entities;
using MintLynk.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MintLynk.Application.Extensions
{
    public static class MappingExtensions
    {
        public static UrlVisitDto ToUrlVisitDto(this LinkHistory entity)
        {
            return new UrlVisitDto
            {
                LinkId = entity.LinkId,
                VisitorIp = entity.UserIp,
                UserAgent = entity.UserAgent,
                Referrer = entity.Referrer,
                OperatingSystem = entity.OperatingSystem,
                Browser = entity.Browser,
                BrowserVersion = entity.BrowserVersion,
                DeviceType = entity.DeviceType,
                Location = entity.Location,
                Country = entity.Country,
                LinkType = entity.LinkType,
                Created = entity.Created,
                CreatedBy = entity.CreatedBy,
                LastModified = entity.LastModified,
                LastModifiedBy = entity.LastModifiedBy                
            };
        }

        public static LinkHistory ToSmartLinkHistory(this UrlVisitDto dto)
        {
            return new LinkHistory
            {
                LinkId = dto.LinkId,
                UserIp = dto.VisitorIp,
                UserAgent = dto.UserAgent,
                Referrer = dto.Referrer,
                OperatingSystem = dto.OperatingSystem,
                Browser = dto.Browser,
                BrowserVersion = dto.BrowserVersion,
                DeviceType = dto.DeviceType,
                Location = dto.Location,
                Country = dto.Country,
                LinkType = dto.LinkType,
                Created = dto.Created,
                CreatedBy = dto.CreatedBy,
                LastModified = dto.LastModified,
                LastModifiedBy = dto.LastModifiedBy
            };
        }

        public static SmartLinkViewDto ToSmartLinkViewDto(this SmartLinkDto dto)
        {
            var utParams = !string.IsNullOrWhiteSpace(dto.UtmParameters) ? JsonSerializer.Deserialize<UtmParameters>(dto.UtmParameters) : null;
            string domain = UrlHelper.ExtractDomain(dto.DestinationUrl);

            return new SmartLinkViewDto
            {
                Id = dto.Id,
                Title = dto.Title == dto.EntityId ? "" : dto.Title,
                Description = dto.Description,
                ShortUrl = dto.ShortUrl,
                Created = dto.Created,
                CreatedBy = dto.CreatedBy,
                LastModified = dto.LastModified,
                LastModifiedBy = dto.LastModifiedBy,
                DestinationUrl= dto.DestinationUrl,
                EntityId= dto.EntityId,
                ExpirationDate = dto.ExpirationDate,
                HasExpirationDate = dto.HasExpirationDate,
                LinkType = dto.LinkType,
                UtmParameters= utParams != null ? utParams : new UtmParameters(),
                Status = dto.Status,
                Tags= dto.Tags ?? string.Empty,
                FavIconUrl = UrlHelper.FormatFavIcon(domain),               
            };
        }

        public static NotificationCenter ToNotificationCenter(this NotificationCenterDto dto)
        {
            return new NotificationCenter
            {
                Id = dto.Id,
                Title = dto.Title,
                ApplicationArea = dto.ApplicationArea,
                AttachmentLocation = dto.AttachmentLocation,
                BccEmail = dto.BccEmail,
                CcEmail = dto.CcEmail,
                ChannelType = dto.ChannelType,
                Created = dto.Created,
                CreatedBy = dto.CreatedBy,
                LastModified = dto.LastModified,
                LastModifiedBy = dto.LastModifiedBy,
                EntityId= dto.EntityId,
                Entity = dto.Entity,
                FromEmail = dto.FromEmail,
                FromName = dto.FromName,
                GroupId = dto.GroupId,
                Link = dto.Link,
                NotificationType = dto.NotificationType,
                Priority = dto.Priority,
                ReceiverId = dto.ReceiverId,
                RecheckCount = dto.RecheckCount,
                ReminderDuration = dto.ReminderDuration,
                ReplyToEmail = dto.ReplyToEmail,
                ReplyToName = dto.ReplyToName,
                SenderId = dto.SenderId,
                SendImmediately = dto.SendImmediately,
                Status = dto.Status,
                Text = dto.Text,
                ToEmail = dto.ToEmail,
                ToName = dto.ToName               
            };
        }

        public static NotificationCenterDto ToNotificationCenterDto(this NotificationCenter dto)
        {
            return new NotificationCenterDto
            {
                Id = dto.Id,
                Title = dto.Title,
                ApplicationArea = dto.ApplicationArea,
                AttachmentLocation = dto.AttachmentLocation,
                BccEmail = dto.BccEmail,
                CcEmail = dto.CcEmail,
                ChannelType = dto.ChannelType,
                Created = dto.Created,
                CreatedBy = dto.CreatedBy,
                LastModified = dto.LastModified,
                LastModifiedBy = dto.LastModifiedBy,
                EntityId = dto.EntityId,
                Entity = dto.Entity,
                FromEmail = dto.FromEmail,
                FromName = dto.FromName,
                GroupId = dto.GroupId,
                Link = dto.Link,
                NotificationType = dto.NotificationType,
                Priority = dto.Priority,
                ReceiverId = dto.ReceiverId,
                RecheckCount = dto.RecheckCount,
                ReminderDuration = dto.ReminderDuration,
                ReplyToEmail = dto.ReplyToEmail,
                ReplyToName = dto.ReplyToName,
                SenderId = dto.SenderId,
                SendImmediately = dto.SendImmediately,
                Status = dto.Status,
                Text = dto.Text,
                ToEmail = dto.ToEmail,
                ToName = dto.ToName
            };
        }

        public static MiniPageDto ToMiniPageDto(this MiniPage entity)
        {
            return new MiniPageDto
            {
                Id = entity.Id,
                Type = entity.Type,
                Title = entity.Title,
                Alias = entity.Alias,
                PageContent = entity.PageContent,
                Template = entity.Template,
                Status = entity.Status,
                Created = entity.Created,                
                Modified = entity.LastModified,
                CreatedBy = entity.CreatedBy ?? "",
                LastModifiedBy = entity.LastModifiedBy ?? ""               
            };
        }

        public static MiniPage ToMiniPage(this MiniPageDto dto)
        {
            return new MiniPage
            {
                Id = dto.Id,
                Type = dto.Type,
                Title = dto.Title,
                Alias = dto.Alias,
                PageContent = dto.PageContent,
                Template = dto.Template,
                Status = dto.Status,
                Created = dto.Created ?? DateTimeOffset.UtcNow,
                CreatedBy = dto.CreatedBy ?? "",
                LastModifiedBy = dto.LastModifiedBy ?? "",
                LastModified = dto.Modified ?? DateTimeOffset.UtcNow
            };
        }
    }
}
