using System;
using System.IO;

namespace Dishantha.Application.Helper
{
    public static class Base64ToImage
    {
        public static System.Drawing.Image ConvertToImage(string base64String)
        {
            if (base64String == null)
                return null;

            System.Drawing.Image image = null;
            string convert = base64String.Replace("data:image/png;base64,", String.Empty);
            byte[] imageBytes = Convert.FromBase64String(convert);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }
    }
}
