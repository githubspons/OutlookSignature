using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vallescar;

namespace FirmesOutlook_CLI
{
    static internal class VCard
    {
        public static string UsuariAVcardString(UserInfo usuari)
        {
            byte[] vcard;
            string temp_string = "";
            string temp_file = Path.GetTempFileName() + ".png";

            temp_string += "BEGIN:VCARD" + Environment.NewLine;
            temp_string += $"N:{usuari.nom_usuari_sense_empresa}{Environment.NewLine}";
            temp_string += $"EMAIL:{usuari.email}{Environment.NewLine}";
            temp_string += $"TEL;TYPE=work,CELL:{usuari.mobil}{Environment.NewLine}";
            temp_string += $"TEL;TYPE=work,VOICE:{usuari.telefon}{Environment.NewLine}";
            temp_string += $"ADR;TYPE=work,PREF:;;{usuari.adreca.Replace('º', ' ')};{usuari.ciutat};Bcn;{usuari.codi_postal};Esp{Environment.NewLine}";
            temp_string += $"TITLE:{usuari.departament}{Environment.NewLine}";
            temp_string += $"ORG:{usuari.empressa}{Environment.NewLine}";
            temp_string += $"URL:{usuari.url}{Environment.NewLine}";
            temp_string += "VERSION:3.0" + Environment.NewLine;
            temp_string += "END:VCARD" + Environment.NewLine;

            return temp_string;
            /*
             BEGIN:VCARD
             N:Smith;John;
             TEL;TYPE=work,VOICE:(111) 555-1212
             TEL;TYPE=home,VOICE:(404) 386-1017
             TEL;TYPE=fax:(866) 408-1212
             EMAIL:smith.j@smithdesigns.com
             ORG:Smith Designs LLC
             TITLE:Lead Designer
             ADR;TYPE=WORK,PREF:;;151 Moore Avenue;Grand Rapids;MI;49503;United States of America
             URL:https://www.smithdesigns.com
             VERSION:3.0
             END:VCARD
            */
            /*

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding encoding = Encoding.GetEncoding("windows-1252");

            byte[] fileBytes = encoding.GetBytes(temp_string.ToCharArray());



            using (StreamWriter sw = new StreamWriter(Path.GetTempFileName() + ".sw.vcf"))
            {
                sw.Write(temp_string);
                sw.Close();
            }


            using (FileStream stream = new FileStream(Path.GetTempFileName() + ".fs.vcf", FileMode.Create, FileAccess.ReadWrite))
            {
                stream.Write(fileBytes, 0, fileBytes.Length);
            }


            /*
             BEGIN:VCARD
            VERSION:3.0
            FN;CHARSET=UTF-8:Sergi Pons Fabregas
            N;CHARSET=UTF-8:Fabregas;Sergi;Pons;;
            EMAIL;CHARSET=UTF-8;type=WORK,INTERNET:spons@vallescar.es
            TEL;TYPE=CELL:607305691
            TEL;TYPE=WORK,VOICE:937451372
            ADR;CHARSET=UTF-8;TYPE=WORK:;;Ctra.Terrassa 225 2 planta;Sabadell;Barcelona;08205;Espanya
            TITLE;CHARSET=UTF-8:Departament IT
            ORG;CHARSET=UTF-8:Vallescar Holding
            URL;type=WORK;CHARSET=UTF-8:www.vallescar.es
            REV:2022-08-17T08:00:07.796Z
            END:VCARD
            */

            //vcard = Encoding.UTF8.GetBytes(temp_string);

            // VCARD A BYTE ARRAY I QR
            /*
            var qr = QRCodeWriter.CreateQrCode(fileBytes, 256, QRCodeWriter.QrErrorCorrectionLevel.Low);
            qr.SaveAsJpeg(temp_file);
            */

            // URL A QR
            /*
            Image logo = Image.FromFile("logo_256.png");
            var qr = QRCodeWriter.CreateQrCodeWithLogoImage("https://www.vallescar.es", logo);            
            qr.SaveAsPng(temp_file);
            */
            /*
            return temp_file;
            */
        }


        public static byte[] UsuariAVcardByteArray(UserInfo usuari)
        {
            byte[] vcard;

            string temp_string = "";
            string temp_file = Path.GetTempFileName() + ".png";

            temp_string += "BEGIN:VCARD" + Environment.NewLine;
            temp_string += $"N:{usuari.nom_usuari_sense_empresa}{Environment.NewLine}";
            temp_string += $"EMAIL:{usuari.email}{Environment.NewLine}";
            temp_string += $"TEL;TYPE=work,CELL:{usuari.mobil}{Environment.NewLine}";
            temp_string += $"TEL;TYPE=work,VOICE:{usuari.telefon}{Environment.NewLine}";
            temp_string += $"ADR;TYPE=work,PREF:;;{usuari.adreca.Replace('º', ' ')};{usuari.ciutat};Bcn;{usuari.codi_postal};Esp{Environment.NewLine}";
            temp_string += $"TITLE:{usuari.departament}{Environment.NewLine}";
            temp_string += $"ORG:{usuari.empressa}{Environment.NewLine}";
            temp_string += $"URL:{usuari.url}{Environment.NewLine}";
            temp_string += "VERSION:3.0" + Environment.NewLine;
            temp_string += "END:VCARD" + Environment.NewLine;

            /*
             BEGIN:VCARD
             N:Smith;John;
             TEL;TYPE=work,VOICE:(111) 555-1212
             TEL;TYPE=home,VOICE:(404) 386-1017
             TEL;TYPE=fax:(866) 408-1212
             EMAIL:smith.j@smithdesigns.com
             ORG:Smith Designs LLC
             TITLE:Lead Designer
             ADR;TYPE=WORK,PREF:;;151 Moore Avenue;Grand Rapids;MI;49503;United States of America
             URL:https://www.smithdesigns.com
             VERSION:3.0
             END:VCARD
            */


            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding encoding = Encoding.GetEncoding("windows-1252");

            vcard = encoding.GetBytes(temp_string.ToCharArray());

            return vcard;

            /*
            using (StreamWriter sw = new StreamWriter(Path.GetTempFileName() + ".sw.vcf"))
            {
                sw.Write(temp_string);
                sw.Close();
            }


            using (FileStream stream = new FileStream(Path.GetTempFileName() + ".fs.vcf", FileMode.Create, FileAccess.ReadWrite))
            {
                stream.Write(fileBytes, 0, fileBytes.Length);
            }
            */

            /*
             BEGIN:VCARD
            VERSION:3.0
            FN;CHARSET=UTF-8:Sergi Pons Fabregas
            N;CHARSET=UTF-8:Fabregas;Sergi;Pons;;
            EMAIL;CHARSET=UTF-8;type=WORK,INTERNET:spons@vallescar.es
            TEL;TYPE=CELL:607305691
            TEL;TYPE=WORK,VOICE:937451372
            ADR;CHARSET=UTF-8;TYPE=WORK:;;Ctra.Terrassa 225 2 planta;Sabadell;Barcelona;08205;Espanya
            TITLE;CHARSET=UTF-8:Departament IT
            ORG;CHARSET=UTF-8:Vallescar Holding
            URL;type=WORK;CHARSET=UTF-8:www.vallescar.es
            REV:2022-08-17T08:00:07.796Z
            END:VCARD
            */

            //vcard = Encoding.UTF8.GetBytes(temp_string);

            // VCARD A BYTE ARRAY I QR
            /*
            var qr = QRCodeWriter.CreateQrCode(fileBytes, 256, QRCodeWriter.QrErrorCorrectionLevel.Low);
            qr.SaveAsJpeg(temp_file);
            */

            // URL A QR
            /*
            Image logo = Image.FromFile("logo_256.png");
            var qr = QRCodeWriter.CreateQrCodeWithLogoImage("https://www.vallescar.es", logo);            
            qr.SaveAsPng(temp_file);
            */

        }
    }
}
