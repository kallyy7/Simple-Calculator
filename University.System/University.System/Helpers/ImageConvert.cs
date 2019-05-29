namespace University_System.Helpers
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Windows.Media.Imaging;

    public static class ImageConvert
    {
        /// <summary>
        /// Convert from byte to bitmap
        /// </summary>
        /// <param name="converted image"></param>
        /// <returns></returns>
        public static BitmapImage GetImage(string imgFromByteToString)
        {
            byte[] bytes = Convert.FromBase64String(imgFromByteToString);
            MemoryStream mem = new MemoryStream(bytes);
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.CacheOption = BitmapCacheOption.OnLoad; 
            bmp.StreamSource = mem;
            bmp.EndInit();

            return bmp;
        }

        /// <summary>
        /// Rezise the image and convert it to byte
        /// </summary>
        /// <returns></returns>
        public static byte[] GetImageBytes(string photoPath)
        {
            Image uploaded = Image.FromFile(photoPath);
            // оригинални размери на изображението
            int originalWidth = uploaded.Width;
            int originalHeight = uploaded.Height;
            // преоразмеряване на изображението (процент от височината и широчината)
            float percentWidth = 100 / (float)originalWidth;
            float percentHeight = 100 / (float)originalHeight;
            float percent = percentHeight < percentWidth ? 
                percentHeight : 
                percentWidth;
            // нови размери на изображението
            int newWidth = (int)(originalWidth * percent);
            int newHeight = (int)(originalHeight * percent);

            // създаване на новото изображение
            Image newImage = new Bitmap(newWidth, newHeight);
            // рисуване на новото изображение
            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(uploaded, 0, 0, newWidth, newHeight);
            }
            // конвертиране в байтове
            ImageConverter converter = new ImageConverter();

            byte[] imageBytes = (byte[])converter
                .ConvertTo(newImage, typeof(byte[]));

            return imageBytes;
        }
    }
}
