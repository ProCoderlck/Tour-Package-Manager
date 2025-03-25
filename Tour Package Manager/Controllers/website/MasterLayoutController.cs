using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tour_Package_Manager.Models;
using System.Reflection;

namespace Tour_Package_Manager.Controllers.website
{
    public class MasterLayoutController : Controller
    {
        // GET: MasterLayout
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ShowHeaders()
        {
            List<LayoutMasterWebsite> Headerlist = new List<LayoutMasterWebsite>();
            try
            {
                DataSet ds = Common.ExecuteProcedureWithResultSets("LayoutMasterWeb",
                     new SqlParameter("@opCode", 401)
                    );
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        LayoutMasterWebsite model = new LayoutMasterWebsite();
                        {
                            model.Email =row["Email"].ToString();
                            model.PhoneNumber = row["PhoneNumber"].ToString();
                            model.Linkddin = row["Linkddin"].ToString();
                            model.Insatgram = row["Insatgram"].ToString();
                            model.Twitter = row["Twitter"].ToString();
                            model.Youtube = row["Youtube"].ToString();
                            model.Facebook = row["Facebook"].ToString();
                        }; Headerlist.Add(model);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(Headerlist);
        }
        public JsonResult ShowFooter()
        {
            LayoutMasterWebsite Headerlist = new LayoutMasterWebsite();
            try
            {
                DataSet ds = Common.ExecuteProcedureWithResultSets("LayoutMasterWeb",
                     new SqlParameter("@opCode", 401)
                    );
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Headerlist.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    Headerlist.PhoneNumber = ds.Tables[0].Rows[0]["PhoneNumber"].ToString();
                    Headerlist.Location = ds.Tables[0].Rows[0]["Location"].ToString();
                    Headerlist.Insatgram = ds.Tables[0].Rows[0]["Insatgram"].ToString();
                    Headerlist.Facebook = ds.Tables[0].Rows[0]["Facebook"].ToString();
                    Headerlist.Twitter = ds.Tables[0].Rows[0]["Twitter"].ToString();
                    Headerlist.Linkddin = ds.Tables[0].Rows[0]["Linkddin"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(Headerlist);
        }
        

    }
}