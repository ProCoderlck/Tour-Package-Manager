using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tour_Package_Manager.Models;

namespace Tour_Package_Manager.Controllers
{
    public class DurationController : Controller
    {
        // GET: Duration
        public ActionResult Add_Duration()
        {
            return View();
        }
        public ActionResult Duration_List()
        {
            return View();
        }
        public JsonResult DurationInsertUpdate(int DurationAutoId, string DurationName, int StatusAutoId)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Message"] = "";
            try
            {

                DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spDuration",
                new SqlParameter("@opCode", DurationAutoId <= 0 ? 101 : 201),
                new SqlParameter("@DurationAutoId", DurationAutoId.ToString()),
                new SqlParameter("@DurationName", DurationName),
                new SqlParameter("@StatusAutoId", StatusAutoId.ToString()),
                new SqlParameter("@createby", Session["ValidateUserID"].ToString())
                 );
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dic["Message"] = ds.Tables[0].Rows[0][0].ToString();
                }

            }
            catch (Exception ex)
            {
                dic["Message"] = ex.Message;
            }

            return Json(dic);
        }


        public JsonResult ShowDuration()
        {
            List<DurationDD> Durationlist = new List<DurationDD>();
            try
            {
                DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spDuration",
                     new SqlParameter("@opCode", 401)
                    );
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        DurationDD model = new DurationDD();
                        {
                            model.DurationAutoId = Convert.ToInt32(row["DurationAutoId"]);
                            model.DurationName = row["DurationName"].ToString();
                            model.StatusAutoId = row["StatusName"].ToString();

                        }; Durationlist.Add(model);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(Durationlist);
        }
        public JsonResult Durationdelete(string DurationAutoId)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Message"] = "";
            try
            {

                DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spDuration",
                     new SqlParameter("@opCode", 301),
                  new SqlParameter("@DurationAutoId", DurationAutoId.ToString())
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
        public JsonResult EditDurationData(string DurationAutoId)
        {
            DurationDD model = new DurationDD();

            try
            {

                DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spDuration",
                    new SqlParameter("@opCode", 402),
                    new SqlParameter("@DurationAutoId", DurationAutoId)
                    );
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.DurationName = ds.Tables[0].Rows[0]["DurationName"].ToString();

                    model.StatusAutoId = ds.Tables[0].Rows[0]["StatusAutoId"].ToString();

                }
            }
            catch (Exception ex)
            {

            }
            return Json(model);
        }
    }
}