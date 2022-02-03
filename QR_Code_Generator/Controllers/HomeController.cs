using Microsoft.AspNetCore.Mvc;
using QR_Code_Generator.Models;
using QRCoder;
using System.Diagnostics;
using System.Drawing;

namespace QR_Code_Generator.Controllers
{
    public class HomeController : Controller
    {




        public IActionResult Index()
        {
            string code = "sumit";
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            using (Bitmap bitMap = qrCode.GetGraphic(20))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();
                    ViewBag.url = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                }
              ;

                return View();
            }

        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Qr(string str)
        {
            string code = str is null || str==""?"Found Nothing":str;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            using (Bitmap bitMap = qrCode.GetGraphic(20))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();
                    ViewBag.url = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                }
              ;

                return PartialView("QrCode");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}