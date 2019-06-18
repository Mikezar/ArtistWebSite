using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Web;

namespace NikaArtist.Service.FileHandler
{
    public class ImageProcessor : IImageProcessor
    {
        private readonly string _thumbFolder;
        private readonly string _photoPath;
        private readonly TextAttributes _textAttributes;
        private static IDictionary<string, FontFamily> _fonts = new Dictionary<string, FontFamily>();

        private FontFamily GetFontFamily(string fileName)
        {
            if (_fonts.ContainsKey(fileName))
                return _fonts[fileName];

            string path = HttpContext.Current.Server.MapPath($"~/fonts/{fileName}.ttf");
            if (File.Exists(path))
            {
                var customFonts = new PrivateFontCollection();
                customFonts.AddFontFile(HttpContext.Current.Server.MapPath($"~/fonts/{fileName}.ttf"));

                _fonts.Add(fileName, customFonts.Families[0]);

                return customFonts.Families[0];
            }
            var defaultFont = FontFamily.Families.SingleOrDefault(x => x.Name == fileName);

            if (defaultFont != null)
                return defaultFont;
            else
                return FontFamily.Families.First();
        }

        public ImageProcessor(string path, string thumbFolder)
        {
            if (string.IsNullOrEmpty(thumbFolder)) throw new ArgumentNullException("thumbPath");
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException("path");

            _thumbFolder = thumbFolder;
            _photoPath = path;
        }

        public ImageProcessor(string path, string thumbFolder, TextAttributes textAttribute) : this(path, thumbFolder)
        {
            _textAttributes = textAttribute ?? textAttribute.Default;
        }

        public void CreateThumbnail(HttpPostedFileBase file, int maxWidth, int maxHeight, string fileName)
        {
            using (var image = Image.FromStream(file.InputStream))
            {
                var ratioX = (double)maxWidth / image.Width;
                var ratioY = (double)maxHeight / image.Height;
                var ratio = Math.Min(ratioX, ratioY);
                var newWidth = (int)(image.Width * ratio);
                var newHeight = (int)(image.Height * ratio);
                using (var newImage = new Bitmap(newWidth, newHeight))
                {
                    using (Graphics thumbGraph = Graphics.FromImage(newImage))
                    {
                        thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                        thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                        thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        thumbGraph.DrawImage(image, 0, 0, newWidth, newHeight);

                        string fileRelativePath = _thumbFolder + Path.GetFileName($"{fileName}s{Path.GetExtension(file.FileName).ToLower()}");

                        if (!Directory.Exists(HttpContext.Current.Server.MapPath(_thumbFolder)))
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(_thumbFolder));

                        newImage.Save(HttpContext.Current.Server.MapPath(fileRelativePath), newImage.RawFormat);
                    }
                }
            }
        }

        public byte[] Watermark(ImageAttributes attributes)
        {
            Random random = new Random();

            var watermarkFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var textFormat = new StringFormat()
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near
            };

            using (Image image = Image.FromFile(HttpContext.Current.Server.MapPath(_photoPath), true))
            {
                using (Graphics imageGraphics = Graphics.FromImage(image))
                {
                    imageGraphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                    if (attributes.IsWatermarkApplied)
                    {
                        using (Font watermarkFont = new Font(GetFontFamily(_textAttributes.WatermarkFont), _textAttributes.WatermarkFontSize, FontStyle.Regular, GraphicsUnit.Pixel))
                        {
                            SizeF actualSize = imageGraphics.MeasureString(_textAttributes.WatermarkText, watermarkFont);
                            imageGraphics.DrawString(_textAttributes.WatermarkText, watermarkFont,
                                new SolidBrush(Color.FromArgb(_textAttributes.Alpha, attributes.IsWatermarkBlack ? Color.Black : Color.White)),
                                (image.Width / 2) + random.Next(40), (image.Height / 2) + random.Next(40),
                                watermarkFormat);
                        }
                    }

                    RotateImage(image);

                    return ImageToByteArray(image);
                }
            }
        }

        public byte[] ImageToByteArray(Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        public void RotateImage(Image image)
        {
            if (Array.IndexOf(image.PropertyIdList, 274) > -1)
            {
                var orientation = (int)image.GetPropertyItem(274).Value[0];
                switch (orientation)
                {
                    case 1:
                        // No rotation required.
                        break;
                    case 2:
                        image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        break;
                    case 3:
                        image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case 4:
                        image.RotateFlip(RotateFlipType.Rotate180FlipX);
                        break;
                    case 5:
                        image.RotateFlip(RotateFlipType.Rotate90FlipX);
                        break;
                    case 6:
                        image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    case 7:
                        image.RotateFlip(RotateFlipType.Rotate270FlipX);
                        break;
                    case 8:
                        image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                }
                // This EXIF data is now invalid and should be removed.
                image.RemovePropertyItem(274);
            }
        }
    }
}
