using QRCoder;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintLynk.Application.Helper
{
    public static class QrCodeHelper
    {
        public static string GenerateQrCode(string domain, string shortUrl)
        {
            var url = $"{domain}/qr/{shortUrl}";
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);

            // Orange foreground, white background
            byte[] orangeRgba = new byte[] { 255, 165, 0, 255 };      // #FFA500
            byte[] whiteRgba = new byte[] { 255, 255, 255, 255 };    // #FFFFFF

            var pngQrCode = new PngByteQRCode(qrCodeData);
            byte[] qrBytes = pngQrCode.GetGraphic(20, orangeRgba, whiteRgba);

            using var qrImage = Image.Load<Rgba32>(qrBytes);

            // Calculate logo size and position (keep logo small for scannability)
            int logoSize = qrImage.Width / 5; // 20% of QR code width
            int logoX = (qrImage.Width - logoSize) / 2;
            int logoY = (qrImage.Height - logoSize) / 2;

            var logoPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "qrlogo.png");
            if (File.Exists(logoPath))
            {
                using var logoImage = Image.Load<Rgba32>(logoPath);
                logoImage.Mutate(x => x.Resize(logoSize, logoSize));

                // Background size (make it larger than logo)
                int bgSize = (int)(logoSize * 1.5);
                int bgX = (qrImage.Width - bgSize) / 2;
                int bgY = (qrImage.Height - bgSize) / 2;

                // Draw a larger white circle behind the logo
                var backgroundCircle = new EllipsePolygon(bgX + bgSize / 2, bgY + bgSize / 2, bgSize / 2);
                qrImage.Mutate(x => x.Fill(Color.White, backgroundCircle));

                // Overlay the logo image
                qrImage.Mutate(x => x.DrawImage(logoImage, new Point(logoX, logoY), 1f));
            }

            using var ms = new MemoryStream();
            qrImage.SaveAsPng(ms);
            var base64 = Convert.ToBase64String(ms.ToArray());

            return $"data:image/png;base64,{base64}";
        }

        public static string GenerateStandQrCode(string domain, string shortUrl, string organization, string code, string leftNoteText, string rightNoteText)
        {
            var url = $"{domain}/qr/{shortUrl}";
            var logoPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "qrlogo.jpg");

            int dpi = 300;
            int qrSize = 600;
            int canvasWidth = 850;
            int canvasHeight = 1100;

            // Generate QR Code
            using var qrGenerator = new QRCodeGenerator();
            using var qrData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);

            // Orange foreground, white background
            byte[] orangeRgba = new byte[] { 255, 165, 0, 255 };      // #FFA500
            byte[] whiteRgba = new byte[] { 255, 255, 255, 255 };    // #FFFFFF

            using var qrCode = new PngByteQRCode(qrData);
            byte[] qrBytes = qrCode.GetGraphic(20, orangeRgba, whiteRgba);
            using var qrImage = Image.Load<Rgba32>(qrBytes);
            qrImage.Mutate(x => x.Resize(qrSize, qrSize));

            using var finalImage = new Image<Rgba32>(canvasWidth, canvasHeight, Color.White);

            var fontFamily = SystemFonts.Families.First(f => f.Name == "Arial");
            var titleFont = new Font(fontFamily, 40, FontStyle.Bold);
            var textFont = new Font(fontFamily, 28);
            var noteFont = new Font(fontFamily, 24, FontStyle.Italic);

            finalImage.Metadata.VerticalResolution = dpi;
            finalImage.Metadata.HorizontalResolution = dpi;

            finalImage.Mutate(ctx =>
            {
                int bottomBoxHeight = 100;
                int bottomBoxY = canvasHeight - bottomBoxHeight;

                if (File.Exists(logoPath))
                {
                    using var logo = Image.Load<Rgba32>(logoPath);

                    int maxWidth = 225;
                    int maxHeight = 75;

                    double ratioX = (double)maxWidth / logo.Width;
                    double ratioY = (double)maxHeight / logo.Height;
                    double ratio = Math.Min(ratioX, ratioY);

                    int newWidth = (int)(logo.Width * ratio);
                    int newHeight = (int)(logo.Height * ratio);

                    logo.Mutate(x => x.Resize(newWidth, newHeight));

                    int topTextY = 60;
                    int logoY = topTextY + 50;
                    int bottomTextY = logoY + newHeight + 10;
                    int scanTextY = logoY + newHeight + 30;

                    int logoX = (canvasWidth - newWidth) / 2;

                    // "Find US IN" text above logo
                    var findUsText = "Find US IN";
                    var findUsTextSize = TextMeasurer.MeasureSize(findUsText, new TextOptions(titleFont));
                    float findUsTextX = (canvasWidth - findUsTextSize.Width) / 2;
                    ctx.DrawText(findUsText, titleFont, Color.Black, new PointF(findUsTextX, topTextY));

                    // Draw logo
                    ctx.DrawImage(logo, new Point(logoX, logoY), 1f);

                    // "Scan QR code to visit" below logo
                    var scanText = "Scan QR code to visit";
                    var scanTextSize = TextMeasurer.MeasureSize(scanText, new TextOptions(textFont));
                    float scanTextX = (canvasWidth - scanTextSize.Width) / 2;
                    ctx.DrawText(scanText, textFont, Color.Black, new PointF(scanTextX, scanTextY));

                    // Draw QR code
                    int qrX = (canvasWidth - qrImage.Width) / 2;
                    int qrY = bottomTextY + (int)scanTextSize.Height + 25;
                    ctx.DrawImage(qrImage, new Point(qrX, qrY), 1);

                    // Organization name below QR
                    var orgTextSize = TextMeasurer.MeasureSize(organization, new TextOptions(titleFont));
                    float orgX = (canvasWidth - orgTextSize.Width) / 2;
                    int orgY = qrY + qrImage.Height + 30;
                    ctx.DrawText(organization, titleFont, Color.Black, new PointF(orgX, orgY));

                    // Code below organization
                    var codeText = $"Code: {code}";
                    var codeTextSize = TextMeasurer.MeasureSize(codeText, new TextOptions(textFont));
                    float codeX = (canvasWidth - codeTextSize.Width) / 2;
                    int codeY = orgY + 55;
                    ctx.DrawText(codeText, textFont, Color.Black, new PointF(codeX, codeY));

                    // Bottom half red and gray boxes
                    var redRect = new Rectangle(0, bottomBoxY, canvasWidth / 2, bottomBoxHeight);
                    var grayRect = new Rectangle(canvasWidth / 2, bottomBoxY, canvasWidth / 2, bottomBoxHeight);

                    ctx.Fill(Color.Red, redRect);
                    ctx.Fill(Color.Gray, grayRect);

                    // Left note text centered in left half
                    var leftNoteSize = TextMeasurer.MeasureSize(leftNoteText, new TextOptions(noteFont));
                    float leftNoteX = (redRect.Width - leftNoteSize.Width) / 2;
                    float noteY = bottomBoxY + (bottomBoxHeight - leftNoteSize.Height) / 2;
                    ctx.DrawText(leftNoteText, noteFont, Color.White, new PointF(leftNoteX, noteY));

                    // Right note text centered in right half
                    var rightNoteSize = TextMeasurer.MeasureSize(rightNoteText, new TextOptions(noteFont));
                    float rightNoteX = grayRect.Left + (grayRect.Width - rightNoteSize.Width) / 2;
                    ctx.DrawText(rightNoteText, noteFont, Color.White, new PointF(rightNoteX, noteY));
                }
            });

            using var ms = new MemoryStream();
            finalImage.SaveAsPng(ms);
            var base64 = Convert.ToBase64String(ms.ToArray());
            return $"data:image/png;base64,{base64}";
        }
    }
}
