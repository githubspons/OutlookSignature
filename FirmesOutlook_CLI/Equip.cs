using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Equip
    {

        public string server_ip_addres { get; } = "";
        public string mac_adress { get; set; } = "";
        public string nic_gateway { get; set; } = "";
        public string total_ram { get; set; } = "";
        public bool dhcp_enabled { get; set; } = false;
        public string ip_address { get; set; } = "";
        public string computer_manufacturer { get; set; } = "";
        public string computer_model { get; set; } = "";
        public string computer_serial_number { get; set; } = "";
        public string mb_serial_number { get; set; } = "";
        public string cpu_id { get; set; } = "";
        public string cpu_descripcio_nom { get; set; } = "";
        public string hdd_serial { get; set; } = "";
        public string comentaris { get; set; } = "";
        public string machine_id { get; set; } = "";
        public string machine_fingerprint { get; set; } = "";
        public string computer_name { get; set; } = "";
        public string computer_os { get; set; } = "";
        public string computer_os_ver { get; set; } = "";
        public string computer_os_build { get; set; } = "";
        public string computer_anydesk_id { get; set; } = "";
        public string computer_ubicacio { get; set; } = "";
        public bool computer_actiu { get; set; } = true;
        public List<Hdd> hdds { get; set; }

        public List<Software> softwares;
        public User usuari_equip { get; set; }
        public List<Monitor> monitors_equip { get; set; }
        public List<Printer> impressores_equip { get; set; }
        public List<certificat> certificats_equip { get; set; }

        public class Hdd
        {
            public string id { get; set; }
            public string marca { get; set; }
            public string model { get; set; }
            public string serie { get; set; }
            public string status { get; set; }
            public string media_type { get; set; }

            public List<Volume> volumes { get; set; }

            public class Volume
            {
                public string hdd_id { get; set; }
                public string label { get; set; }
                public double free { get; set; }
                public double used { get; set; }
                public double total { get; set; }

            }

        }

        public class Software
        {
            public int id { get; set; }
            public string equip_id { get; set; }
            public string nom { get; set; }
            public string versio { get; set; }
        }

        public class User
        {
            public string userDomainId { get; set; }
            public string domainName { get; set; }
            public string userName { get; set; }
            public string emailUsuari { get; set; }
            public string extensioVOIP { get; set; }
            public string otherTelephone { get; set; }
            public string phoneNumber { get; set; }
            public string ou { get; set; }
            public string lastLogon { get; set; }

        }

        public class Printer
        {
            public string nom { get; set; }

            public string ip { get; set; }

            public string status { get; set; }
        }

        public class certificat
        {
            public string nom { get; set; }
            public string private_key { get; set; }
            public DateTime valid { get; set; }
        }
        public class Monitor
        {

            public string id { get; set; }
            public string midaPantalla { get; set; }
            public string pixelsWidth { get; set; }
            public string pixelsHeight { get; set; }
            public string descripcio { get; set; }
            public string fabricant { get; set; }
            public string model { get; set; }
            public string numeroSerie { get; set; }
            public string tipusEntrada { get; set; }
            public string horizontalSize { get; set; }
            public string verticalSize { get; set; }
            public string active { get; set; }
            public string mida { get; set; }

            public Monitor(string id = "", string midaPantalla = "", string pixelsWidth = "", string pixelsHeight = "", string descripcio = "", string fabricant = "")
            {
                this.id = id;
                this.midaPantalla = midaPantalla;
                this.pixelsWidth = pixelsWidth;
                this.pixelsHeight = pixelsHeight;
                this.descripcio = descripcio;
                this.fabricant = fabricant;
            }

            public double getWidthResolution()
            {
                double result = 0;
                try
                {
                    result = (int)int.Parse(this.pixelsWidth) * (int.Parse(this.horizontalSize) / 2.54);
                    result = Math.Round(result);
                }
                catch (Exception)
                {
                    result = -1;
                }
                return result;
            }
            public double getHeightResolution()
            {
                double result = 0;
                try
                {
                    result = (int)int.Parse(this.pixelsHeight) * (int.Parse(this.verticalSize) / 2.54);
                    result = Math.Round(result);
                }
                catch (Exception)
                {
                    result = -1;
                }
                return result;
            }


        }

    }

    
}
