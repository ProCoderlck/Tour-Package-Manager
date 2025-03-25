using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using Tour_Package_Manager.Models;

namespace Tour_Package_Manager.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        public JsonResult changePaswordWeb(string Password, string NewPassword)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Message"] = "";
            try
            {
                int UserAutoId = Convert.ToInt32(Session["ValidateUserID"]);
                if (UserAutoId > 0)
                {

                    DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spUser",
                        new SqlParameter("@opCode", 202),
                        new SqlParameter("@UserAutoId", UserAutoId),
                        new SqlParameter("@Password", Password),
                        new SqlParameter("@NewPassword", NewPassword)
                    );
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        dic["Message"] = ds.Tables[0].Rows[0]["Message"].ToString();

                    }
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