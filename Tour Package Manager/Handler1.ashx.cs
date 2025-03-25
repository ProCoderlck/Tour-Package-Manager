using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tour_Package_Manager
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Int32 unixTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            HttpPostedFile uploads = context.Request.Files.Get(0);
            string file = System.IO.Path.GetFileName(uploads.FileName);
            string filePath = context.Server.MapPath(".") + "\\Uploads\\img\\" + unixTimestamp + "_" + file;
            uploads.SaveAs(filePath);
            context.Response.Write(filePath);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}