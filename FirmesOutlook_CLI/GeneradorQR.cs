using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.Common;
using ZXing.QrCode.Internal;
using ZXing.Rendering;
using ZXing;
using Image = System.Drawing.Image;
using IronBarCode;

namespace FirmesOutlook_CLI
{
    static internal class GeneradorQR
    {
        public static string LinkAQr(string username, string logo_filename)
        {
            // DADES USUARI

            byte[] vcard;
            string temp_string = "";

            ZXing.BarcodeWriter barcode = new ZXing.BarcodeWriter();
            EncodingOptions encodingOptions = new EncodingOptions() { Width = 512, Height = 512, Margin = 0, PureBarcode = false };
            encodingOptions.Hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);

            barcode.Renderer = new BitmapRenderer();
            barcode.Options = encodingOptions;

            barcode.Format = BarcodeFormat.QR_CODE;


            Bitmap bitmap = barcode.Write($"http://vallescar.com/vcard/{username}.vcf");


            // MIDA LOGO 768
            Bitmap logo = new Bitmap(logo_filename);

            Graphics g = Graphics.FromImage(bitmap);
            Point p = new Point(139, 139);

            g.DrawImage(logo, p);

            string temp_file = Path.GetTempFileName() + ".png";
            bitmap.Save(temp_file);


            return temp_file;
        }

        public static string LinkAQrAlt(string username, string logo_filename)
        {
            Image logo = Image.FromFile(logo_filename);
            var qr = QRCodeWriter.CreateQrCodeWithLogoImage("https://www.vallescar.es", logo);
            string temp_file = Path.GetTempFileName() + ".png";
            qr.SaveAsPng(temp_file);
            return temp_file;
        }


    }
}
