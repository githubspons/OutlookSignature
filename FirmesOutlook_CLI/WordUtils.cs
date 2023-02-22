using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirmesOutlook_CLI
{
    static internal class WordUtils
    {


        // COMPROVEM SI LA FIRMA TE QR
        static public bool FirmaAmbQR(string alt_text,Microsoft.Office.Interop.Word.Document selection)
        {
            bool result = false;


            foreach (Microsoft.Office.Interop.Word.InlineShape s in selection.InlineShapes)
            {

                if (s.Type == Microsoft.Office.Interop.Word.WdInlineShapeType.wdInlineShapePicture)
                {
                    if (s.AlternativeText == alt_text)
                    {
                        result = true;
                    }
                }
            }

            return result;
        }


        static public void SubstitueixImatges(string image_file_path,string alt_text, Microsoft.Office.Interop.Word.Document selection)
        {

            List<Microsoft.Office.Interop.Word.Range> ranges = new List<Microsoft.Office.Interop.Word.Range>();
            float width = 0, height = 0;

            foreach (Microsoft.Office.Interop.Word.InlineShape s in selection.InlineShapes)
            {

                if (s.Type == Microsoft.Office.Interop.Word.WdInlineShapeType.wdInlineShapePicture)
                {
                    if (s.AlternativeText == alt_text)
                    {
                        ranges.Add(s.Range);
                        width = s.Width;
                        height = s.Height;

                        s.Delete();
                    }
                }
            }


            foreach (Microsoft.Office.Interop.Word.Range range in ranges)
            {

                var s = range.InlineShapes.AddPicture(image_file_path);
                s.Width = width;
                s.Height = height;
                s.AlternativeText = "QR";

            }


        }



        static public void SubstituirText(string etiqueta, string textNou, Microsoft.Office.Interop.Word.Selection selection)
        {
            try
            {
                selection.Find.Text = etiqueta;
                selection.Find.Forward = true;
                selection.Find.MatchWholeWord = true;
                selection.Find.Replacement.Text = textNou;
                selection.Find.Execute(etiqueta, false, true, false, false, false, true, false, false, textNou, 2);
            }
            catch (Exception)
            {
                Console.WriteLine("No s'ha canviat la etiqueta : " + etiqueta);
            }
        }


    }
}
