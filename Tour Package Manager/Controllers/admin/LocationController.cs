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
    public class LocationController : Controller
    {
        // GET: Location
        public ActionResult Add_Location()
        {
            return View();
        }
        public ActionResult Location_List()
        {
            return View();
        }
        public JsonResult LocationInsertUpdate(int LocationAutoId, string LocationName, int StatusAutoId, string LocationImage)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Message"] = "";
            try
            {
                DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spLocation",
                new SqlParameter("@opCode", LocationAutoId <= 0 ? 101 : 201),
                new SqlParameter("@LocationAutoId", LocationAutoId.ToString()),
                new SqlParameter("@LocationName", LocationName),
                new SqlParameter("@StatusAutoId", StatusAutoId.ToString()),
                new SqlParameter("@LocationImage", Utility.SaveBase64AsImage(LocationImage, 4)),

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


        public JsonResult ShowLocation()
        {
            List<LocationDD> Locationlist = new List<LocationDD>();
            try
            {
                DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spLocation",
                     new SqlParameter("@opCode", 401)
                    );
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        LocationDD model = new LocationDD();
                        {
                            model.LocationAutoId = Convert.ToInt32(row["LocationAutoId"]);
                            model.LocationName = row["LocationName"].ToString();
                            model.LocationImage = row["LocationImage"].ToString();

                            model.StatusAutoId = row["StatusName"].ToString();

                        }; Locationlist.Add(model);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(Locationlist);
        }
        public JsonResult Locationdelete(string LocationAutoId)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Message"] = "";
            try
            {

                DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spLocation",
                     new SqlParameter("@opCode", 301),
                  new SqlParameter("@LocationAutoId", LocationAutoId.ToString())
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
        public JsonResult EditLocationData(string LocationAutoId)
        {
            LocationDD model = new LocationDD();
            try
            {
                DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spLocation",
                    new SqlParameter("@opCode", 402),
                    new SqlParameter("@LocationAutoId", LocationAutoId)
                    );
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.LocationName = ds.Tables[0].Rows[0]["LocationName"].ToString();
                    model.StatusAutoId = ds.Tables[0].Rows[0]["StatusAutoId"].ToString();
                    model.LocationImage = ds.Tables[0].Rows[0]["LocationImage"].ToString();

                }
            }
            catch (Exception ex)
            {

            }
            return Json(model);
        }
    }
}