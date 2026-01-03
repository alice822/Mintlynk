namespace MintLynk.Web.Helper
{
    public class NameParts
    {
        public string FirstName { get; set; } = "";
        public string MiddleName { get; set; } = "";
        public string LastName { get; set; } = "";
    }

    public static class StringExtensions
    {
        public static NameParts SplitFullName(this string fullName)
        {
            var result = new NameParts();

            if (string.IsNullOrWhiteSpace(fullName))
                return result;

            var parts = fullName.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 1)
            {
                result.FirstName = parts[0];
            }
            else if (parts.Length == 2)
            {
                result.FirstName = parts[0];
                result.LastName = parts[1];
            }
            else
            {
                result.FirstName = parts[0];
                result.LastName = parts[^1];
                result.MiddleName = string.Join(" ", parts.Skip(1).Take(parts.Length - 2));
            }

            return result;
        }
    }

}
