using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace QLQuanCaPhe.DTO
{
    public class ImageEncryption
    {
        private static ImageEncryption instance;

        public static ImageEncryption Instance
        {
            get
            {
                if (instance == null)
                    instance = new ImageEncryption();
                return instance;
            }
        }
        private ImageEncryption() { }

        public byte[] ImageToByteArray(Image img)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public Image ByteArrayToImage(byte[] b)
        {
            MemoryStream m = new MemoryStream(b);
            return Image.FromStream(m);
        }
    }
}
