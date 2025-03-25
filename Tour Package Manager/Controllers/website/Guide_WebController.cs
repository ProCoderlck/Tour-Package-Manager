using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tour_Package_Manager.Models;

namespace Tour_Package_Manager.Controllers.website
{
    public class Guide_WebController : Controller
    {
        // GET: Guide_Web
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ShowGuideforWeb()
        {

            List<GuideDD> Guidelist = new List<GuideDD>();

            try
            {
                DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spGuide",
                     new SqlParameter("@opCode", 401)
                    );
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        GuideDD model = new GuideDD();
                        {
                            model.GuideAutoId = Convert.ToInt32(row["GuideAutoId"]);
                            model.GuideName = row["GuideName"].ToString();
                            model.Designation = row["Designation"].ToString();
                            model.Photo = row["Photo"].ToString();

                        }; Guidelist.Add(model);
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return Json(Guidelist);
        }
    }
}