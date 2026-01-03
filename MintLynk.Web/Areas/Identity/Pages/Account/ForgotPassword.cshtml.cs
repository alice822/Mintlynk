// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;
using MintLynk.Application.Common.Models;
using MintLynk.Application.Helper;
using MintLynk.Application.Interfaces;
using MintLynk.Domain.Enums;
using MintLynk.Domain.Models;
using MintLynk.Infrastructure.Identity;
using MintLynk.Web.Extensions;
using MintLynk.Web.Helper;
using MintLynk.Web.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace MintLynk.Web.Areas.Identity.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICommunicationService _communicationService;
        private readonly IUrlShorteningService _urlShorteningService;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly string _fromName;
        private readonly string _fromEmail;
        public  readonly string _bccEmails;
        private readonly string _key;
        private readonly string _iv;
        public ForgotPasswordModel(UserManager<ApplicationUser> userManager, ICommunicationService communicationService, IWebHostEnvironment env, IConfiguration configuration, IUrlShorteningService urlShorteningService)
        {
            _userManager = userManager;
            _communicationService = communicationService;
            _env = env;
            _configuration = configuration;
            _fromName = _configuration.GetSection("Smtp").GetSection("SenderName").Value ?? "";
            _fromEmail = _configuration.GetSection("Smtp").GetSection("SenderEmail").Value ?? "";
            _bccEmails = _configuration.GetSection("Smtp").GetSection("BccEmails").Value ?? "";
            _key = _configuration.GetSection("Security").GetSection("Key").Value ?? "";
            _iv = _configuration.GetSection("Security").GetSection("Iv").Value ?? "";
            _urlShorteningService = urlShorteningService;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            public string Status { get; set; }
        }

        public IActionResult OnGetAsync(string email = null, string message = null)
        {
            Input = new InputModel
            {
                Email = email,
                Status = message
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                Input.Status = "Request received. If an account exists with thsi email, you will receive a password reset link in your mail.";
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    return Page();
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(EncryptionHelper.Encrypt(code, _key, _iv)));
                var email = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(EncryptionHelper.Encrypt(user.Email, _key, _iv)));
                var date = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(EncryptionHelper.Encrypt(DateTime.UtcNow.ToString(), _key, _iv)));

                var appVariables = _configuration.GetSection("AppVariables").Get<AppVariables>();
                var domain = appVariables != null ? appVariables.Domain : "";
                if (string.IsNullOrEmpty(domain))
                {
                    domain = $"{Request.Scheme}://{Request.Host}";
                }
                var callbackUrl = $"{domain}/reset-password?uc={code}&ui={email}&ud={date}";
                try
                {

                    var dataModel = new SmartLinkDto();
                    dataModel.DestinationUrl = callbackUrl;
                    dataModel.LinkType = (int)SmartLinkType.ShortUrl;
                    dataModel.CreatedBy = "Password Reset";
                    dataModel.LastModifiedBy = "Password Reset";
                    dataModel.Title = await UrlHelper.GetPageTitle(callbackUrl);

                    var shortLink = await _urlShorteningService.QuickShortUrl(dataModel);
                    if (shortLink != null)
                    {
                        
                        var shortUrl = $"{domain}/{shortLink.ShortUrl}";
                        callbackUrl = shortUrl;
                    }
                }
                catch(Exception ex)
                {

                }

                var filePath = Path.Combine(_env.ContentRootPath, "MailTemplates", "passwordreset.txt");
                var mailContent = "";
                if(System.IO.File.Exists(filePath))
                {
                    mailContent = System.IO.File.ReadAllText(filePath);
                    var fullName = user.GetFullName();
                    mailContent = mailContent.Replace("#username#", fullName).Replace("#resetlink#", callbackUrl);
                    var mailModel = new MailModel
                    {
                        ToEmail = Input.Email,
                        Subject = "Reset Password",
                        MailBody = mailContent,
                        FromEmail = _fromEmail,
                        FromName = _fromName,
                        BccEmails = _bccEmails,
                        ToName = fullName
                    };

                    var isMailSend = _communicationService.SendMail(mailModel);
                }
                
                return Page();
            }

            return Page();
        }
    }
}
