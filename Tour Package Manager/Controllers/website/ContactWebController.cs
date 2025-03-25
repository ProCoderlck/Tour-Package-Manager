using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tour_Package_Manager.Models;
using System.Web.Helpers;

namespace Tour_Package_Manager.Controllers.website
{
    public class ContactWebController : Controller
    {
        // GET: ContactWeb
        public ActionResult Contact()
        {
            return View();
        }
        public JsonResult SentMessage(string Name,string Email,string Subject, string ContactMessage)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Message"] = "";
            try
            {

                DataSet ds = Common.ExecuteProcedureWithResultSets("Website_spContact",
                new SqlParameter("@opCode",101),
                new SqlParameter("@ContactName", Name),
                new SqlParameter("@Email", Email),
                new SqlParameter("@ContactSubject", Subject),
                new SqlParameter("@ContactMessage", ContactMessage)
                 );
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dic["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();
                }

            }
            catch (Exception ex)
            {
                dic["Message"] = ex.Message;
            }

            return Json(dic);
        }

    }
}