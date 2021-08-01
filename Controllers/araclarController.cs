using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Text;

namespace GuvenlikKoduUygulamasi.Controllers
{
    public class araclarController : Controller
    {
        [HttpGet]
        public ActionResult guvenlik_kodu()
        {
            Random rndRastgele = new Random();
            string strKod = fnKod_Olustur(4);
            byte[] bytKod = Encoding.UTF8.GetBytes(strKod);
            HttpContext.Session.Set("guvenlik_kodu", bytKod);
            FileContentResult fcrResim = null;
            using (MemoryStream msHafiza = new MemoryStream())
            using (Bitmap bmpResim = new Bitmap(100, 30))
            using (Graphics grResim = Graphics.FromImage((Image)bmpResim))
            {
                grResim.TextRenderingHint = TextRenderingHint.AntiAlias;
                grResim.SmoothingMode = SmoothingMode.AntiAlias;
                grResim.FillRectangle(Brushes.Transparent, new Rectangle(0, 0, bmpResim.Width, bmpResim.Height));
                grResim.DrawString(strKod, new Font("Arial", 20), Brushes.Black, 2, 3);
                bool blnKarmasa = true;
                if (blnKarmasa)
                {
                    int i, r, x, y, z;
                    Pen penKalem = new Pen(Color.Yellow);
                    for (i = 1; i < 10; i++)
                    {
                        penKalem.Color = Color.FromArgb((rndRastgele.Next(0, 255)), (rndRastgele.Next(0, 255)), (rndRastgele.Next(0, 255)));
                        r = rndRastgele.Next(0, 100);
                        x = rndRastgele.Next(0, 50);
                        y = rndRastgele.Next(0, 100);
                        z = rndRastgele.Next(0, 50);
                        grResim.DrawLine(penKalem, r, x, y, z);
                    }
                }
                bmpResim.Save(msHafiza, System.Drawing.Imaging.ImageFormat.Png);
                fcrResim = this.File(msHafiza.GetBuffer(), "image/png");
            }
            return fcrResim;
        }

        public string fnKod_Olustur(int parAdet)
        {
            string strKarakterler = "ABCDEFHJKLMNPRTXWYZ12346789";
            string strKod = string.Empty;
            Random rnd = new Random();
            for (int i = 0; i < parAdet; i++)
                strKod += strKarakterler[rnd.Next(0, strKarakterler.Length)].ToString();
            return strKod;
        }
    }
}
