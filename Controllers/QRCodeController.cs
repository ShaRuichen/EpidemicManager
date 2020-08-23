using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;

using Microsoft.AspNetCore.Mvc;

using QRCoder;

namespace EpidemicManager.Controllers
{
    public class QRCodeController : Controller
    {
        public IActionResult Index(string path)
        {
            path ??= string.Empty;
            var context = Request.HttpContext;
            var connection = context.Connection;
            var localPort = connection.LocalPort;
            var port = localPort.ToString();
            var url = $"http://{ip}:{port}/{path}";
            using var generator = new QRCodeGenerator();
            using var qrCodeData = generator.CreateQrCode(url, QRCodeGenerator.ECCLevel.H);
            using var qrCode = new QRCode(qrCodeData);
            using var bitmap = qrCode.GetGraphic(10);
            using var stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Jpeg);
            var bytes = stream.GetBuffer();
            return new FileContentResult(bytes, "image/jpeg");
        }

        private readonly string ip;

        public QRCodeController()
        {
            var hostName = Dns.GetHostName();
            var hostEntry = Dns.GetHostEntry(hostName);
            var addresses = hostEntry.AddressList;
            foreach (var address in addresses)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    ip = address.ToString();
                }
            }
        }
    }
}
