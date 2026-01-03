using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MintLynk.Application.Common.Models;
using MintLynk.Application.Dtos;
using MintLynk.Application.Extensions;
using MintLynk.Application.Interfaces;
using MintLynk.Domain.Entities;
using MintLynk.Domain.Enums;
using MintLynk.Domain.Interfaces;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace MintLynk.Application.Services
{
    public class CommunicationService : ICommunicationService
    {
        private readonly INotificationCenterRepository _notificationRepository;
        private readonly IConfiguration _configuration;
        private readonly int _hostPort;
        private readonly string _hostAddress;
        private readonly string _password;
        private readonly string _username;
        private readonly ILogger<CommunicationService> _logger;
        public CommunicationService(INotificationCenterRepository notificationRepository, IConfiguration configuration, ILogger<CommunicationService> logger) 
        { 
            _notificationRepository = notificationRepository;
            _configuration = configuration;
            _logger = logger;
            _hostAddress = _configuration.GetSection("Smtp").GetSection("Host").Value ?? "";
            _hostPort = Convert.ToInt32(_configuration.GetSection("Smtp").GetSection("Port").Value);
            _password = _configuration.GetSection("Smtp").GetSection("Pass").Value ?? "";
            _username = _configuration.GetSection("Smtp").GetSection("User").Value ?? "";
        }

        public async Task<NotificationCenterDto> CreateAsync(NotificationCenterDto notification)
        {
            if (notification == null) throw new ArgumentNullException(nameof(notification));
            var entity = notification.ToNotificationCenter();
            var result = await _notificationRepository.CreateAsync(entity);
            return result.ToNotificationCenterDto();
        }

        public async Task<bool> DeleteAsync(Guid id)
        { 
            return await _notificationRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<NotificationCenterDto>> GetAllAsync()
        {
            var data = await _notificationRepository.GetAllAsync();
            if (data != null)
            {
                return data.Select(x => x.ToNotificationCenterDto());
            }
            return Enumerable.Empty<NotificationCenterDto>();
        }

        public async Task<IEnumerable<NotificationCenterDto>> GetAllAsync(string receiverId)
        {
            var data = await _notificationRepository.GetAllAsync(receiverId);
            if (data != null)
            {
                return data.Select(x => x.ToNotificationCenterDto());
            }
            return Enumerable.Empty<NotificationCenterDto>();
        }

        public async Task<NotificationCenterDto?> GetAsync(Guid id)
        {
            var data = await _notificationRepository.GetAsync(id);

            if (data == null)
            {
                return null;
            }
            return data.ToNotificationCenterDto();
        }

        public List<NotificationCenterDto> GetImmediateMails(int check)
        {
            var data = _notificationRepository.GetImmediateMails(check);
            if (data != null)
            {
                return data.Select(x => x.ToNotificationCenterDto()).ToList();
            }
            return new List<NotificationCenterDto>();
        }

        public List<NotificationCenterDto> GetScheduledMails()
        {
            var data = _notificationRepository.GetScheduledMails();
            if (data != null)
            {
                return data.Select(x => x.ToNotificationCenterDto()).ToList();
            }
            return new List<NotificationCenterDto>();
        }

        public async Task<bool> SendMail(MailModel mail)
        {
            try
            {
                _logger.LogInformation("Send mail to " + mail.ToEmail + " processing.");
                var from = new MailAddress(mail.FromEmail, mail.FromName, System.Text.Encoding.UTF8);
                var to = new MailAddress(mail.ToEmail, mail.ToName);
                var message = new MailMessage(from, to)
                {
                    Body = mail.MailBody,
                    BodyEncoding = System.Text.Encoding.UTF8,
                    Subject = mail.Subject,
                    SubjectEncoding = System.Text.Encoding.UTF8,
                    IsBodyHtml = true
                };
                if (!string.IsNullOrEmpty(mail.BccEmails))
                {
                    var bccList = mail.BccEmails.Split(',');
                    foreach (var bcc in bccList)
                    {
                        message.Bcc.Add(new MailAddress(bcc));
                    }
                }

                if (mail.FileAttachment != null)
                {
                    var attachment = new Attachment(new MemoryStream(mail.FileAttachment), mail.FileName, mail.ContentType);
                    message.Attachments.Add(attachment);
                }

                var smtpClient = new SmtpClient(_hostAddress)
                {
                    Port = _hostPort,
                    Credentials = new NetworkCredential(_username, _password),
                    EnableSsl = true,
                };

                await smtpClient.SendMailAsync(message);
                _logger.LogInformation("Mail send to " + mail.ToEmail);
                return true;
            }
            catch (SmtpFailedRecipientsException err)
            {                
                _logger.LogError("Failed to send mail to + " + mail.ToEmail + ". SMTP failed detail: " + err.Message + " Inner: " + err?.InnerException?.Message ?? "");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to send mail to + " + mail.ToEmail + ". Exception detail: " + ex.Message + " Inner: " + ex?.InnerException?.Message ?? "");
                return false;
            }
        }

        public async Task<NotificationCenterDto> UpdateAsync(NotificationCenterDto notification)
        {
            if (notification == null) throw new ArgumentNullException(nameof(notification));
            var entity = notification.ToNotificationCenter();
            var result = await _notificationRepository.UpdateAsync(entity);
            return result.ToNotificationCenterDto();
        }
    }
}