using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vallescar
{
    public static class LogVallescar
    {

        static string log_path = @"c:\inf\logs\";
        static string log_path_live = @"\\192.168.1.12\it\compartit\logs\";


        static public void init_app(string filename)
        {
            if (Directory.Exists(Path.GetDirectoryName(log_path_live)))
            {
                using (StreamWriter sw = new StreamWriter(log_path_live + @"\APP_INIT_" + Environment.MachineName + "_" + filename))
                {
                    sw.WriteLine("INIT");
                    sw.Close();
                }
            }
        }

        static public void init_app(string versio, string filename)
        {
            if (Directory.Exists(Path.GetDirectoryName(log_path_live)))
            {
                using (StreamWriter sw = new StreamWriter(log_path_live + @"\APP_INIT_" + Environment.MachineName + "_" + filename))
                {
                    sw.WriteLine("INIT VERSIO " + versio);
                    sw.Close();
                }
            }
        }

        static public void log_app(string log,string filename)
        {
            if (Directory.Exists(Path.GetDirectoryName(log_path_live)))
            {
                using (StreamWriter sw = new StreamWriter(log_path_live + @"\APP_INIT_" + Environment.MachineName + "_" + filename, append: true))
                {
                    sw.WriteLine(DateTime.Now.ToString() + " " + log);
                    sw.Close();
                }
            }
        }

        static public void stop_app(string filename)
        {
            try
            {
                if (File.Exists(log_path_live + @"\APP_INIT_" + Environment.MachineName + "_" + filename))
                    File.Delete(log_path_live + @"\APP_INIT_" + Environment.MachineName + "_" + filename);
            }catch(Exception e)
            {
                throw new ArgumentException("No s'ha pogut eliminat l'arxiu log " + e.Message);
            }
        }

        

        static public string write_log(string missatge,string filename)
        {
            string log_filename = log_path + filename;

            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(log_filename)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(log_filename));
                }

                using (StreamWriter sw = new StreamWriter(log_filename, append:true))
                {
                    sw.WriteLine(DateTime.Now.ToString() + " " + missatge);
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return "";

        }

    }
}
