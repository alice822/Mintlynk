using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using MintLynk.Application.Common.Interfaces;
using MintLynk.Application.Common.Models;
using MintLynk.Application.Helper;
using MintLynk.Application.Interfaces;
using MintLynk.Application.Services;
using MintLynk.Domain.Enums;
using MintLynk.Domain.Models;
using MintLynk.Infrastructure.Identity;
using MintLynk.Web.Extensions;
using MintLynk.Web.Helper;
using MintLynk.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;

namespace MintLynk.Web.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly ICommunicationService _communicationService;
        private readonly IUrlShorteningService _urlShorteningService;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly string _fromName;
        private readonly string _fromEmail;
        public readonly string _bccEmails;
        private readonly string _key;
        private readonly string _iv;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger, ICommunicationService communicationService, IConfiguration configuration, IUrlShorteningService urlShorteningService, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
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
        public InputModel? Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string? ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme>? ExternalLogins { get; set; }

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
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Full Name")]
            public string FullName { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();
                var name = Input?.FullName.SplitFullName();
                user.FirstName = name?.FirstName ?? string.Empty;
                user.MiddleName = name?.MiddleName ?? string.Empty;
                user.LastName = name?.LastName ?? string.Empty;
                user.UserName = Input?.Email;
                user.Email = Input?.Email;
                user.EmailConfirmed = false;
                user.TwoFactorEnabled = false;
                user.PhoneNumberConfirmed = false;
                user.Status = (int)UserStatus.AwaitingConfirmation;
                user.ConfirmationToken = "";
                user.MagicToken = "";
                user.CreatedOn = DateTime.UtcNow;
                

                await _userStore.SetUserNameAsync(user, Input?.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input?.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    _ = await _userManager.AddToRoleAsync(user, "User");
                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(EncryptionHelper.Encrypt(code, _key, _iv)));
                    userId = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(EncryptionHelper.Encrypt(userId, _key, _iv)));
                    var date = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(EncryptionHelper.Encrypt(DateTime.UtcNow.ToString(), _key, _iv)));

                    var appVariables = _configuration.GetSection("AppVariables").Get<AppVariables>();
                    var domain = appVariables != null ? appVariables.Domain : "";
                    if (string.IsNullOrEmpty(domain))
                    {
                        domain = $"{Request.Scheme}://{Request.Host}";
                    }
                    var callbackUrl = $"{domain}/confirm-registration?uc={code}&ui={userId}";
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
                    catch (Exception ex)
                    {

                    }

                    var filePath = Path.Combine(_env.ContentRootPath, "MailTemplates", "registration.txt");
                    var mailContent = "";
                    if (System.IO.File.Exists(filePath))
                    {
                        mailContent = System.IO.File.ReadAllText(filePath);
                        var fullName = user.GetFullName();
                        mailContent = mailContent.Replace("#username#", fullName).Replace("#confirmationlink#", callbackUrl);
                        var mailModel = new MailModel
                        {
                            ToEmail = Input.Email,
                            Subject = "Registration Confirmation",
                            MailBody = mailContent,
                            FromEmail = _fromEmail,
                            FromName = _fromName,
                            BccEmails = _bccEmails,
                            ToName = fullName
                        };

                        var isMailSend = _communicationService.SendMail(mailModel);
                    }

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { ui = EncryptionHelper.Encrypt(userId, _key, _iv), returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}