//using IronBarCode;
using Microsoft.Office.Interop.Word;
using Spire.Barcode;
using System;
using System.Collections.Generic;
using System.IO;
using Vallescar;
using Image = System.Drawing.Image;
using Point = System.Drawing.Point;
using Word = Microsoft.Office.Interop.Word;

namespace FirmesOutlook_CLI
{
    class Program
    {


        // 0.6c
        /*
         * Modificada la comprovació per si s'ha d'actualitzar la firma, ara comparem lastwritedata dels
         * arxius origen i desti
         */

        static string versio = "0.9c";
        static string carpeta_log = @"FirmesOutlook_CLI.txt";
        static string carpeta_plantilles_firma = @"\\192.168.1.12\it\Compartit\Firmes_Outlook\";
        static string carpeta_local_plantilles = @"c:\inf";
        static bool document_visible = false;
        static string ERR_plantilla_firma = @"No s'ha pogut copiar l'arxiu de la plantilla";
        static bool actualitzar_firma = true;
        static bool usuari_cmd = false;
        static UserInfo usuari;
        static string md5_1 = "";
        static string md5_2 = "";
        static DateTime arxiu_servidor;
        static DateTime arxiu_local;
        static bool alternativa = false;
        static List<string> firmes;
        static int numero_firma;
        static string nom_plantilla_cmd = "";


        static void Main(string[] args)
        {


            LogVallescar.write_log("Iniciant configuració plantilla versio " + versio, carpeta_log);

            UserInfo usuari = new UserInfo();
            firmes = new List<string>();

            // COMPROVEM SI HI HA ARGS
            foreach (string a in args)
            {
                if (a.IndexOf("/plantilla=") > -1)
                {
                    string[] c = a.Split('=');
                    alternativa = true;
                    usuari.nom_arxiu_firma = c[1];
                }
            }



            // COMPROVEM SI S'HA INICIAT L'APLICACIÓ AMB UN NOM DE FIRMA ALTERNATIU
            if (usuari.nom_arxiu_firma != "")
            {


                // COMPROVEM SI TE MES D'UNA FIRMA I FEM UN LLISTAT
                if (usuari.nom_arxiu_firma.IndexOf(";") > -1)
                {
                    Console.WriteLine("2b");
                    string[] noms_firmes = usuari.nom_arxiu_firma.Split(';');

                    Console.WriteLine("2c");
                    foreach (string firma in noms_firmes)
                    {
                        firmes.Add(firma);
                    }
                }
                else
                {
                    firmes.Add(usuari.nom_arxiu_firma);
                }

                numero_firma = 1;



                // CONFIGUREM LES FIRMES
                foreach (string firma in firmes)
                {

                    actualitzar_firma = true;

                    // MIREM SI LA FIRMA EXISTEIX AL SERVIDOR
                    if (FuncionsArxiu.FileExists(carpeta_plantilles_firma + firma) == false)
                    {
                        LogVallescar.write_log("L'arxiu de firma " + firma + " no existeix al servidor", carpeta_log);
                        actualitzar_firma = false;
                    }


#if DEBUG
                    // ELIMINAR DOC PERQUE ES GENERI DE NOU LA FIRMA
                    if (File.Exists(carpeta_local_plantilles + @"\" + firma))
                        File.Delete(carpeta_local_plantilles + @"\" + firma);
#endif


                    if (actualitzar_firma == true)
                    {
                        if (File.Exists(carpeta_local_plantilles + @"\" + firma))
                        {
                            FileInfo fileInfoSource = new FileInfo(carpeta_plantilles_firma + firma);
                            FileInfo fileInfoDest = new FileInfo(carpeta_local_plantilles + @"\" + firma);

                            if (fileInfoSource.LastWriteTime == fileInfoDest.LastWriteTime)
                            {
                                LogVallescar.write_log("L'arxiu de firma " + firma + " no ha canviat, no cambiem la firma", carpeta_log);
                                LogVallescar.write_log("Data plantilla servidor/local " + fileInfoSource.LastWriteTime.ToString() + " " + fileInfoDest.LastWriteTime.ToString(), carpeta_log);
                                actualitzar_firma = false;
                            }
                            else
                            {
                                LogVallescar.write_log("Data plantilla servidor/local " + fileInfoSource.LastWriteTime.ToString() + " " + fileInfoDest.LastWriteTime.ToString(), carpeta_log);
                            }
                        }
                    }

#if DEBUG == false
                    if (actualitzar_firma == true)
                    {
#endif

                    LogVallescar.write_log("Configurant firma " + firma, carpeta_log);

                    configurar_firma(usuari, firma, numero_firma, document_visible);

#if DEBUG == false
                }
#endif


                    alternativa = true;
                    numero_firma++;

                }
            }
            else
            {
                LogVallescar.write_log("l'usuari no te una plantilla configurada al domini", carpeta_log);
            }

            System.Environment.Exit(0);                                                                                     // Console app
        }




        // FUNCIO PER MODIFCAR LA PLANTILLA I AFEGIR QR
        static private void configurar_firma(UserInfo usuari, string nom_plantilla, int numero_firma, bool visible = false)
        {

            LogVallescar.write_log("Iniciant configuració firma", carpeta_log);

            if (FuncionsArxiu.copiar_arxius_no_md5(carpeta_plantilles_firma + @"\" + nom_plantilla, @"c:\inf\" + nom_plantilla) != "")
            {
                LogVallescar.write_log(ERR_plantilla_firma, carpeta_log);
                return;
            }

            var wordApp = new Word.Application();
            wordApp.Visible = visible;
            Word.Document document = wordApp.Documents.Open(carpeta_local_plantilles + @"\" + nom_plantilla);

            Console.WriteLine("Obrint plantilla : " + carpeta_local_plantilles + @"\" + nom_plantilla);
            Console.WriteLine("Document name : " + document.Name);

            Word.Hyperlinks hyperlinks = document.Hyperlinks;

            try
            {
                foreach (Word.Hyperlink hyperlink in hyperlinks)
                {
                    if (hyperlink.Address == "mailto:aemail")
                    {
                        hyperlink.Address = "mailto://" + usuari.email;
                    }

                    if (hyperlink.Address == "http://aweb/")
                    {
                        hyperlink.Address = usuari.email;
                    }
                }
            }
            catch (Exception)
            {

                Console.WriteLine("La plantilla no te cap vincle");
            }




            Word.EmailOptions email_options = wordApp.EmailOptions;
            Word.EmailSignature email_signature = email_options.EmailSignature;
            Word.EmailSignatureEntries email_signature_entries = email_signature.EmailSignatureEntries;
            Word.Document signature = document;
            Word.Range signature_range = signature.Range();
            Word.Selection new_signature = wordApp.Selection;



            WordUtils.SubstituirText("anomsenseempresaupper", usuari.nom_usuari_sense_empresa.ToUpper(), new_signature);
            WordUtils.SubstituirText("anomsenseempresa", usuari.nom_usuari_sense_empresa, new_signature);
            WordUtils.SubstituirText("anomupper", usuari.nom_usuari.ToUpper(), new_signature);
            WordUtils.SubstituirText("anom", usuari.nom_usuari, new_signature);
            WordUtils.SubstituirText("adeptupper", usuari.departament.ToUpper(), new_signature);
            WordUtils.SubstituirText("adept", usuari.departament, new_signature);
            WordUtils.SubstituirText("acompanyupper", usuari.empressa.ToUpper(), new_signature);
            WordUtils.SubstituirText("acompany", usuari.empressa, new_signature);
            WordUtils.SubstituirText("aemail", usuari.email, new_signature);
            WordUtils.SubstituirText("%email", usuari.email, new_signature);
            WordUtils.SubstituirText("atelefon", usuari.telefon, new_signature);
            WordUtils.SubstituirText("amobil", usuari.mobil, new_signature);
            WordUtils.SubstituirText("afax", usuari.fax, new_signature);
            WordUtils.SubstituirText("aweb", usuari.url, new_signature);
            WordUtils.SubstituirText("aadreca1", usuari.adreca, new_signature);
            WordUtils.SubstituirText("acodipostal", usuari.codi_postal, new_signature);
            WordUtils.SubstituirText("aciutat", usuari.ciutat, new_signature);
            WordUtils.SubstituirText("aestat", usuari.estat, new_signature);
            WordUtils.SubstituirText("aiptel", usuari.ip_telefon, new_signature);




            // SI TENIM MÒVIL AFEGIM ELS CAMPS MOB: I |  (PEUGEOT)
            if (usuari.mobil != "")
            {
                WordUtils.SubstituirText("amobillabel", "Mob: " + usuari.mobil + " | ", new_signature);
            }
            else
            {
                WordUtils.SubstituirText("amobillabel", "", new_signature);
            }




            // GENEREM IMATGE QR
            if (WordUtils.FirmaAmbQR("qr", signature) == true)
            {
                string qr = GeneradorQR.LinkAQrAlt(usuari.sAMAccountName, "vch_transparent_marge_768_interior_solid_t.png");
                WordUtils.SubstitueixImatges(qr, "qr", signature);
            }



            if (usuari.nom_usuari != "")
            {

                // AFEGIM FIRMA
                email_signature_entries.Add(usuari.nom_usuari_sense_empresa + " (" + numero_firma.ToString() + ")", signature_range);

                // CONFIGUREM LA FIRMA PREDETERMINADA
                if (alternativa == false)
                {
                    email_signature.NewMessageSignature = usuari.nom_usuari_sense_empresa + " (" + numero_firma.ToString() + ")";
                    email_signature.ReplyMessageSignature = usuari.nom_usuari_sense_empresa + " (" + numero_firma.ToString() + ")";
                }
            }


            // MARQUEM EL DOCUMENT WORD COM A DESAT I SORTIM
            document.Saved = true;                                                                                          // Marquem el document com a desat i sortim de Word
            wordApp.Quit();


        }
    }
}
