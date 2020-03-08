using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberIconGenerator
{
    public class Generator
    {
        public string OutputDirectory { get; set; }
        public string BackgroundPath { get; set; }
        public string Suffix { get; set; }
        public int Min { get; set; }
        public int Max { get; set; } = 99;
        public int Size { get; set; } = 128;
        public float FontSize { get; set; } = 21;

        public void Generate()
        {
            System.IO.Directory.CreateDirectory(OutputDirectory);

            int width = Size, height = Size;
            bool alpha = true;

            var image = Bitmap.FromFile(BackgroundPath);

            for (int i = Min; i <= Max; i++)
            {
                var destRect = new System.Drawing.Rectangle(0, 0, width, height);
                var pixelFormat = alpha ? System.Drawing.Imaging.PixelFormat.Format32bppArgb : System.Drawing.Imaging.PixelFormat.Format24bppRgb;
                var destImage = new Bitmap(width, height, pixelFormat);

                destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

                Font font = new Font("Segoe UI", FontSize, FontStyle.Regular);

                using (var graphics = Graphics.FromImage(destImage))
                {
                    //graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    using (var wrapMode = new ImageAttributes())
                    {
                        wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                        graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                    }

                    StringFormat format = new StringFormat();
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Center; 
                    graphics.DrawString(i.ToString(), font, Brushes.Black, width * 0.5f, height * 0.5f, format);
                }

                var fname = $"num{i}{Suffix}.png";
                destImage.Save(System.IO.Path.Combine(OutputDirectory, fname));
            }
        }
    }
}
