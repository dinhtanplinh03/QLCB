using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace QLCB.Helpers
{
    public class QRCodeGeneratorHelper
    {
        public static byte[] GenerateQRCode(string content)
        {
            // Tạo đối tượng generator
            using (var qrGenerator = new QRCodeGenerator())
            {
                // Tạo dữ liệu QR code từ nội dung
                using (var qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q))
                {
                    // Tạo ảnh bitmap QR code
                    using (var qrCode = new QRCode(qrCodeData))
                    using (var bitmap = qrCode.GetGraphic(20)) // kích thước block = 20
                    using (var ms = new MemoryStream())
                    {
                        // Lưu vào bộ nhớ
                        bitmap.Save(ms, ImageFormat.Png);
                        return ms.ToArray();
                    }
                }
            }
        }
    }
}
