using MintLynk.Infrastructure.Identity;
using System.Security.Claims;

namespace MintLynk.Web.Extensions
{
    public static class UserExtensions
    {
        public static string GetFullName(this ApplicationUser user)
        {
            var firstName = user.FirstName ?? string.Empty;
            var middleName = user.MiddleName ?? string.Empty;
            var lastName = user.LastName ?? string.Empty;

            return string.Join(" ", new[] { firstName, middleName, lastName }
                .Where(s => !string.IsNullOrWhiteSpace(s))).Trim();
        }

        public static string? GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
