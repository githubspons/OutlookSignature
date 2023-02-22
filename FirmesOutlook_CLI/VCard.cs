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




            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding encoding = Encoding.GetEncoding("windows-1252");

            vcard = encoding.GetBytes(temp_string.ToCharArray());

            return vcard;


        }
    }
}
