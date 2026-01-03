using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintLynk.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string ConfirmationToken { get; set; }
        public string MagicToken { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int? Gender { get; set; }
        public string ProfilePicPath { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
        public bool? NewsLetterSubscription { get; set; }
        public bool? TermsAndConditionAccepted { get; set; }
        public string Intro { get; set; }

        public Guid GroupId { get; set; }

        public string? UserAlias { get; set; }
    }
}
