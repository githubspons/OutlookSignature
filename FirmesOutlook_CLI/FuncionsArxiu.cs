using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace Vallescar
{
    static class FuncionsArxiu
    {


        public static string CopiarArxius(string origen,string desti)
        {
            try
            {
                string origen_md5 = CalculateMD5(origen);
                string desti_md5 = CalculateMD5(desti);

                if (origen_md5.Equals(desti_md5))
                    return "";

                File.Copy(origen, desti, true);
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return "";
        }

        public static string copiar_arxius_no_md5(string origen,string desti)
        {
            try
            {
                File.Copy(origen, desti, true);
            }catch(Exception e)
            {
                return e.Message;
            }
            return "";
        }

        public static bool FileExists(String filename)
        {
            return File.Exists(filename);
        }

        public static DateTime get_file_date(string filename)
        {
            DateTime dateTime = new DateTime();
            FileInfo fileInfo = new FileInfo(filename);
            dateTime = fileInfo.CreationTime;
            return dateTime;
        }

        public static string CalculateMD5(string filename)
        {
            string result = "";
            try
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(filename))
                    {
                        var hash = md5.ComputeHash(stream);
                        result = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                        stream.Close();
                    }
                    
                }
            }
            catch (Exception)
            {
                return "NO_MD5";
            }
            
            return result;
        }


        static public bool son_arxius_diferents(string origen_json_config,string desti_json_config)
        {
            
            string remote_md5 = CalculateMD5(origen_json_config);
            string local_md5 = CalculateMD5(desti_json_config);

            if (remote_md5.Equals(local_md5))
                return false;
             
            return true;
        }



  
    }
}
