using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintLynk.Application.Helper
{
    public class ColorHelper
    {
        public static byte[] HexToRgba(string hex, byte alpha = 255)
        {
            if (string.IsNullOrWhiteSpace(hex))
                throw new ArgumentException("Hex color cannot be null or empty.", nameof(hex));

            hex = hex.Replace("#", "");
            if (hex.Length == 3)
            {
                // Expand short form (#abc -> #aabbcc)
                hex = string.Concat(hex.Select(c => $"{c}{c}"));
            }

            if (hex.Length != 6)
                throw new ArgumentException("Hex color must be 6 characters.", nameof(hex));

            byte r = Convert.ToByte(hex.Substring(0, 2), 16);
            byte g = Convert.ToByte(hex.Substring(2, 2), 16);
            byte b = Convert.ToByte(hex.Substring(4, 2), 16);

            return new byte[] { r, g, b, alpha };
        }
    }
}
