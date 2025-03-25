using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Web;

namespace Tour_Package_Manager.Models
{
    public class Utility
    {

        public static string SaveBase64AsImage(string base64String, int imageType)
        {
            string imageURL = "";
            try
            {
                if (base64String.Contains(","))
                {
                    int i = base64String.IndexOf(",");
                    base64String = base64String.Substring(i + 1);
                }
                else if (base64String.Trim() == "" || base64String == null)
                {
                    return "";
                }
                else if(base64String.Contains("/uploads/img/"))
                {
                    return base64String;
                }
                byte[] imageBytes = Convert.FromBase64String(base64String);
                Random rand = new Random();
                Int32 unixTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                string imageFolder = "/uploads/img/";
                if (imageType == 1)
                {
                    imageFolder = "/uploads/img/PackageImage/";
                }
                else if (imageType == 2) 
                {
                    imageFolder = "/uploads/img/PackagePdf/";
                }
                 else if (imageType == 3) 
                {
                    imageFolder = "/uploads/img/CategoryImage/";
                }
                 else if (imageType == 4) 
                {
                    imageFolder = "/uploads/img/LocationImage/";
                }
                else if (imageType == 5)
                {
                    imageFolder = "/uploads/img/UserImage/";
                }
                else if (imageType == 6)
                {
                    imageFolder = "/uploads/img/BlogImage/";
                }
                else if (imageType == 7)
                {
                    imageFolder = "/uploads/img/Testimonial/";
                }
                else if (imageType == 8)
                {
                    imageFolder = "/uploads/img/Guides/";
                }

                string imageName = unixTimestamp + "_" + rand.Next(100000, 999999) + ".png";
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + imageFolder, imageName);
                try
                {
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        using (Image image = Image.FromStream(ms))
                        {
                            image.Save(filePath);
                        }
                    }
                    imageURL = imageFolder + imageName;
                }
                catch (Exception ex)
                {

                }
            }
            catch
            {

            }
            return imageURL;
        }
    }
}