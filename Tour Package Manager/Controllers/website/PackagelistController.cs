using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tour_Package_Manager.Models;
using System.Web.UI.WebControls;

namespace Tour_Package_Manager.Controllers.website
{
    public class PackagelistController : Controller
    {
        // GET: Packagelist
        public ActionResult Packagelist()
        {
            return View();
        }
        public ActionResult Package_details()
        {
            return View();
        }
        public JsonResult ShowPackage_detail(string PackageAutoId)
        {
            PackageDD model = new PackageDD();
            try
            {
                DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spPackageMaster",
                    new SqlParameter("@opCode", 406),
                    new SqlParameter("@PackageAutoId", PackageAutoId)
                    );
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.PackageName = ds.Tables[0].Rows[0]["PackageName"].ToString();
                    model.Price = Convert.ToDouble(ds.Tables[0].Rows[0]["Price"]);
                    model.PackageImage = ds.Tables[0].Rows[0]["PackageImage"].ToString();
                    model.Discription = ds.Tables[0].Rows[0]["Discription"].ToString();
                    model.ShortDiscription = ds.Tables[0].Rows[0]["ShortDiscription"].ToString();
                    model.DurationAutoId = ds.Tables[0].Rows[0]["DurationName"].ToString();
                    model.LocationAutoId = ds.Tables[0].Rows[0]["LocationName"].ToString();
                    model.CatagoryAutoId = ds.Tables[0].Rows[0]["CategoryName"].ToString();
                    model.PackageEnd = ds.Tables[0].Rows[0]["PackageEnd"].ToString();
                    model.PackageStart = ds.Tables[0].Rows[0]["PackageStart"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(model);
        }
        public JsonResult Packagecategory()
        {

            List<CategoryDD> Packagelist = new List<CategoryDD>();

            try
            {
                DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spPackageMaster",
                     new SqlParameter("@opCode", 407)

                    );
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        CategoryDD model = new CategoryDD();
                        {
                            model.CategoryName = row["CategoryName"].ToString();
                            model.CategoryAutoId = Convert.ToInt32(row["CategoryAutoId"].ToString());
                        }; Packagelist.Add(model);
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return Json(Packagelist);
        }
        public JsonResult showRecentPackage()
        {

            List<PackageDD> Packagelist = new List<PackageDD>();

            try
            {
                DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spPackageMaster",
                     new SqlParameter("@opCode", 408)

                    );
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        PackageDD model = new PackageDD();
                        {
                            model.PackageName = row["PackageName"].ToString();
                            model.PackageAutoId = Convert.ToInt32(row["PackageAutoId"].ToString());
                            model.PackageImage = row["PackageImage"].ToString();
                            model.PackageStart = row["PackageStart"].ToString();

                        }; Packagelist.Add(model);
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return Json(Packagelist);
        }


        public JsonResult ShowDaysforwebsite(int PackageAutoId)
        {
            List<DaysDD> daysList = new List<DaysDD>();

            try
            {

                DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spPackageMaster",
                     new SqlParameter("@opCode", "414"),
                     new SqlParameter("@PackageAutoId", PackageAutoId.ToString())

                    );
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        DaysDD model = new DaysDD();
                        {
                            model.DaysName = row["DaysName"].ToString();
                            model.DaysAutoId = Convert.ToInt32(row["DaysAutoId"].ToString());
                        }; daysList.Add(model);
                    }
                }

            }
            catch (Exception ex)
            {

            }


            return Json(daysList);
        }
        public JsonResult Taskforwebsite(int DaysAutoId)
         {
            List<TaskDD> List = new List<TaskDD>();

            try
            {

                DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spPackageMaster",
                     new SqlParameter("@opCode", "415"),
                     new SqlParameter("@DaysAutoId", DaysAutoId.ToString())

                    );
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        TaskDD model = new TaskDD();
                        {
                            model.TaskName = row["TaskName"].ToString();
                        }; List.Add(model);
                    }
                }

            }
            catch (Exception ex)
            {

            }


            return Json(List);
        }
        
    }
}