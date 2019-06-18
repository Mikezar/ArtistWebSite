using System.Drawing;
using System.Web;

namespace NikaArtist.Service.FileHandler
{
    public interface IImageProcessor
    {
        void CreateThumbnail(HttpPostedFileBase file, int maxWidth, int maxHeight, string fileName);
        byte[] Watermark(ImageAttributes attributes);
        byte[] ImageToByteArray(Image image);
        void RotateImage(Image image);
    }
}
