using System;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace Vallescar
{


    //Afegir referencia a DirectoryServices


    class UserInfo
    {
        public string sAMAccountName { get; set; }
        public string userDomainId { get; set; }
        public string domainName { get; set; }
        public string nom_usuari { get; set; } = "";
        public string nom_usuari_sense_empresa { get; set; } = "";
        public string empressa { get; set; } = "";
        public string departament { get; set; } = "";
        public string extensioVOIP { get; set; } = "";
        public string telefon { get; set; } = "";
        public string fax { get; set; } = "";
        public string mobil { get; set; } = "";
        public string email { get; set; } = "";
        public string ou { get; set; } = "";
        public string codi_postal { get; set; } = "";
        public string adreca { get; set; } = "";
        public string ciutat { get; set; } = "";
        public string estat { get; set; } = "";
        public string url { get; set; } = "";
        public string strL { get; set; } = "";
        public string facebook { get; set; } = "";
        public string ip_telefon { get; set; } = "";
        public string nom_arxiu_firma { get; set; } = "";
        public string lastLogon { get; set; } = "";

        public UserInfo()
        {

            informacio_usuari();

                this.userDomainId = System.Environment.UserDomainName + @"\\" + Environment.UserName;
            this.domainName = "vallescar.net";
            
            this.nom_usuari = System.DirectoryServices.AccountManagement.UserPrincipal.Current.DisplayName;

            if (this.nom_usuari.IndexOf('|') > -1)                  // GENERAR CAP NOM USUARI SENSE EMPRESA
            {
                try
                {
                    string[] temp = this.nom_usuari.Split('|');
                    this.nom_usuari_sense_empresa =  temp[0].Trim();
                }
                catch (Exception)
                { this.nom_usuari_sense_empresa = ""; }
            }

            this.email = System.DirectoryServices.AccountManagement.UserPrincipal.Current.EmailAddress;
            this.extensioVOIP = System.DirectoryServices.AccountManagement.UserPrincipal.Current.VoiceTelephoneNumber;
            this.ou = get_user_ou(Environment.UserName);
            this.lastLogon = System.DirectoryServices.AccountManagement.UserPrincipal.Current.LastLogon.ToString();
        }


        static public string get_user_ou(string usernmame)
        {
            string servidor_dns = "192.168.1.9:389";
            string dc = "dc=vallescar,dc=net";
            string usuari_administrador = "vallescar.net\\administrador";
            string contrasenya = "Cidon@*0met";

            //string a = FuncionsDomini.GetOU("echica");

            try
            {
                /* Retreiving a principal context */
                PrincipalContext domainContext = new PrincipalContext(ContextType.Domain, servidor_dns, dc, usuari_administrador, contrasenya);

                /* Retreive a user
                 */
                UserPrincipal user = UserPrincipal.FindByIdentity(domainContext, usernmame);

                /* Retreive the container
                 */

                // Obtenim el Path del usuari al Ldap i comprovem totes les entrades OU fins la primera

                DirectoryEntry deUser = user.GetUnderlyingObject() as DirectoryEntry;
                string[] userPath = deUser.Path.ToString().Split(',');
                string ou = "";

                foreach (string s in userPath)
                {
                    if(s.IndexOf("OU")>-1)
                    {
                        ou = s.Substring(3, s.Length - 3);
                    }
                }

                return ou;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string informacio_usuari()
        {
            string userName = Environment.UserName;

            try
            {
                DirectoryEntry myLdapConnection = createDirectoryEntry();
                DirectorySearcher search = new DirectorySearcher(myLdapConnection);
                search.Filter = "(&(objectClass=user)(anr=" + userName + "*))";


                // create an array of properties that we would like and  
                // add them to the search object  

                string[] requiredProperties = new string[] {"sAMAccountName","cn", "st", "Fullname", "company", "physicalDeliveryOfficeName", "homePhone", "streetAddress", "l", "WWWHomePage", "url", "pager", "telephoneNumber", "facsimileTelephoneNumber", "url", "pager", "postOfficeBox", "mail", "mobile", "title" };

                foreach (String property in requiredProperties)
                    search.PropertiesToLoad.Add(property);

                SearchResult result = search.FindOne();

                if (result != null)
                {
                    foreach (String property in requiredProperties)
                        foreach (Object myCollection in result.Properties[property])
                        {
                            switch (property)
                            {
                                case "sAMAccountName":
                                    this.sAMAccountName = myCollection.ToString();
                                    break;
                                case "FullName":
                                    this.nom_usuari = myCollection.ToString();
                                    break;
                                case "company":
                                    this.empressa = myCollection.ToString();
                                    break;
                                case "physicalDeliveryOfficeName":
                                    this.departament = myCollection.ToString();
                                    break;
                                case "homePhone":
                                    this.telefon = myCollection.ToString();
                                    break;
                                case "mobile":
                                    this.mobil = myCollection.ToString();
                                    break;
                                case "mail":
                                    this.email = myCollection.ToString();
                                    break;
                                case "postOfficeBox":
                                    this.codi_postal = myCollection.ToString();
                                    break;
                                case "streetAddress":
                                    this.adreca = myCollection.ToString();
                                    break;
                                case "l":
                                    this.ciutat = myCollection.ToString();
                                    break;
                                case "st":
                                    this.estat = myCollection.ToString();
                                    break;
                                case "WWWHomePage":
                                    this.url = myCollection.ToString();
                                    break;
                                case "facsimileTelephoneNumber":
                                    this.fax = myCollection.ToString();
                                    break;
                                case "url":
                                    this.strL = myCollection.ToString();
                                    break;
                                case "pager":
                                    this.facebook = myCollection.ToString();
                                    break;
                                case "telephoneNumber":
                                    this.ip_telefon = myCollection.ToString();
                                    break;
                                case "title":
                                    this.nom_arxiu_firma = myCollection.ToString();
                                    break;
                            }

                        }
                }

                else Console.WriteLine("User not found!");
            }

            catch (Exception e)
            {
                Console.WriteLine("Exception caught:\n\n" + e.ToString());
            }

            return "";

        }

        public string getTitle()
        {

            String userName = Environment.UserName;

            try
            {
                DirectoryEntry myLdapConnection = createDirectoryEntry();
                DirectorySearcher search = new DirectorySearcher(myLdapConnection);
                search.Filter = "(&(objectClass=user)(anr=" + userName + "*))";

                // create an array of properties that we would like and  
                // add them to the search object  

                string[] requiredProperties = new string[] { "cn", "Fullname","company", "physicalDeliveryOfficeName", "homePhone", "streetAddress","l", "wWWHomePage","url","pager", "telephoneNumber", "facsimileTelephoneNumber","url","pager", "postofficebox", "mail", "mobile","title" };

                foreach (String property in requiredProperties)
                    search.PropertiesToLoad.Add(property);

                SearchResult result = search.FindOne();

                if (result != null)
                {
                    foreach (String property in requiredProperties)
                        foreach (Object myCollection in result.Properties[property])
                        {
                            if (property == "title")
                                return  myCollection.ToString();
                        }
                }

                else Console.WriteLine("User not found!");
            }

            catch (Exception e)
            {
                Console.WriteLine("Exception caught:\n\n" + e.ToString());
            }

            return "";

        }


        static DirectoryEntry createDirectoryEntry()
        {
            // create and return new LDAP connection with desired settings  

            DirectoryEntry ldapConnection = new DirectoryEntry("LDAP://DC=vallescar,DC=net");
            ldapConnection.Path = "LDAP://DC=vallescar,DC=net";
            ldapConnection.AuthenticationType = AuthenticationTypes.Secure;
            return ldapConnection;

            // LDAP                
            /*
            DirectoryEntry entry = new DirectoryEntry("LDAP://DC=vallescar,DC=net");
            DirectorySearcher mySearcher = new DirectorySearcher(entry);
            mySearcher.Filter = "(&(objectClass=user)(anr=" + userName + "*))";
            SearchResultCollection result = mySearcher.FindAll();
            */
        }

    }
}
