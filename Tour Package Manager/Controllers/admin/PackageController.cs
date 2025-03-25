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
    public class PackageController : Controller
    {
        // GET: Package
        public ActionResult Add_Package()
        {
            return View();
        }
        public ActionResult Package_List()
        {
            return View();
        }
        public JsonResult PackageInsertUpdate(int PackageAutoId, string PackageName, string ShortDiscription, string Discription, string Price,
           int DurationAutoId, int CatagoryAutoId, int LocationAutoId, string PackageImage, string PackagePdf, string PackageStart, string PackageEnd,
           int StatusAutoId)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Message"] = "";
            dic["PakageId"] = "";
            try
            {

                DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spPackageMaster",
                new SqlParameter("@opCode", PackageAutoId <= 0 ? 101 : 201),
                new SqlParameter("@PackageAutoId", PackageAutoId.ToString()),
                new SqlParameter("@PackageName", PackageName),
                new SqlParameter("@ShortDiscription", ShortDiscription),
                new SqlParameter("@Discription", Discription.ToString()),
                new SqlParameter("@Price", Price),
                new SqlParameter("@DurationAutoId", DurationAutoId.ToString()),
                new SqlParameter("@CatagoryAutoId", CatagoryAutoId.ToString()),
                new SqlParameter("@LocationAutoId", LocationAutoId.ToString()),
                new SqlParameter("@PackageImage", Utility.SaveBase64AsImage(PackageImage, 1)),
                new SqlParameter("@PackagePdf", Utility.SaveBase64AsImage(PackagePdf, 2)),
                new SqlParameter("@PackageStart", PackageStart.ToString()),
                new SqlParameter("@PackageEnd", PackageEnd.ToString()),
                new SqlParameter("@StatusAutoId", StatusAutoId.ToString()),
                new SqlParameter("@CreatedBy", Session["ValidateUserID"].ToString())
                 );
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dic["Message"] = ds.Tables[0].Rows[0][0].ToString();
                    dic["PakageId"] = ds.Tables[0].Rows[0][1].ToString();
                }

            }
            catch (Exception ex)
            {
                dic["Message"] = ex.Message;
            }

            return Json(dic); 
        }
        public JsonResult ShowPackage(int? DurationAutoId,int ?CategoryAutoId,int ?LocationAutoId)
          {
            List<PackageDD> PackageList = new List<PackageDD>();
            try
            {
                DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spPackageMaster",
                     new SqlParameter("@opCode", "401"),
                     new SqlParameter("@DurationAutoId", DurationAutoId),
                     new SqlParameter("@CatagoryAutoId", CategoryAutoId),
                     new SqlParameter("@LocationAutoId", LocationAutoId)
                    );

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        PackageDD model = new PackageDD();
                        {
                            model.PackageAutoId = Convert.ToInt32(row["PackageAutoId"]);
                            model.PackageName = row["PackageName"].ToString();
                            model.Price = Convert.ToDouble(row["Price"].ToString());
                            model.DurationAutoId = row["DurationName"].ToString();
                            model.CatagoryAutoId = row["CategoryName"].ToString();
                            model.LocationAutoId = row["LocationName"].ToString();
                            model.StatusAutoId = row["StatusName"].ToString();
                            model.PackageImage = row["PackageImage"].ToString();

                        }; PackageList.Add(model);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(PackageList);
        }
        public JsonResult EditPackage(string PackageAutoId)
        {
            PackageDD model = new PackageDD();
            try
            {
                DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spPackageMaster",
                    new SqlParameter("@opCode", 402),
                    new SqlParameter("@PackageAutoId", PackageAutoId)
                    );
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.PackageName = ds.Tables[0].Rows[0]["PackageName"].ToString();
                    model.ShortDiscription = ds.Tables[0].Rows[0]["ShortDiscription"].ToString();
                    model.Discription = ds.Tables[0].Rows[0]["Discription"].ToString();
                    model.Price = Convert.ToDouble(ds.Tables[0].Rows[0]["Price"]);
                    model.DurationAutoId = ds.Tables[0].Rows[0]["DurationAutoId"].ToString();
                    model.CatagoryAutoId = ds.Tables[0].Rows[0]["CatagoryAutoId"].ToString();
                    model.LocationAutoId = ds.Tables[0].Rows[0]["LocationAutoId"].ToString();
                    model.PackageImage = ds.Tables[0].Rows[0]["PackageImage"].ToString();
                    model.PackagePdf = ds.Tables[0].Rows[0]["PackagePdf"].ToString();
                    model.PackageStart = ds.Tables[0].Rows[0]["PackageStart"].ToString();
                    model.PackageEnd = ds.Tables[0].Rows[0]["PackageEnd"].ToString();
                    model.StatusAutoId = ds.Tables[0].Rows[0]["StatusAutoId"].ToString();

                }
            }
            catch (Exception ex)
            {

            }
            return Json(model);
        }
        public JsonResult Packagedelete(string PackageAutoId)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Message"] = "";
            try
            {

                DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spPackageMaster",
                     new SqlParameter("@opCode", 301),
                  new SqlParameter("@PackageAutoId", PackageAutoId.ToString())
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
        public JsonResult getDurationList()
        {
            List<DurationForDD> Durationlists = new List<DurationForDD>();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter("@OpCode", 403),

                };
                DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spPackageMaster", parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Durationlists.Add(new DurationForDD
                        {
                            DurationAutoId = Convert.ToInt32(row["DurationAutoId"].ToString()),
                            DurationName = row["DurationName"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ex = new Exception(ex.Message);
            }
            return Json(Durationlists);
        }
        public JsonResult getCategoryList()
        {
            List<CategoryForDD> Categorylists = new List<CategoryForDD>();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter("@OpCode", 404),

                };
                DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spPackageMaster", parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Categorylists.Add(new CategoryForDD
                        {
                            CategoryAutoId = Convert.ToInt32(row["CategoryAutoId"].ToString()),
                            CategoryName = row["CategoryName"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ex = new Exception(ex.Message);
            }
            return Json(Categorylists);
        }
        public JsonResult getLocationList()
        {
            List<LocationForDD> Locationlists = new List<LocationForDD>();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter("@OpCode", 405),

                };
                DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spPackageMaster", parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Locationlists.Add(new LocationForDD
                        {
                            LocationAutoId = Convert.ToInt32(row["LocationAutoId"].ToString()),
                            LocationName = row["LocationName"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ex = new Exception(ex.Message);
            }
            return Json(Locationlists);
        } 

        //Here start days master method

        public JsonResult InsertUpdateDays(int DaysAutoId, string DaysName, string PackageAutoId)
          {
            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {
                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spPackageMaster",
                        new SqlParameter("@opCode", DaysAutoId <= 0 ? 102 : 202),
                        new SqlParameter("@DaysAutoId", DaysAutoId.ToString()),
                        new SqlParameter("@DaysName", DaysName),
                        new SqlParameter("@PackageAutoId", PackageAutoId),
                        new SqlParameter("@CreatedBy", Session["ValidateUserID"].ToString())

                         );
                        ResponseDataObj.setResponseData(ds.Tables[0].Rows[0]["MessageCode"].ToString(), 
                            ds.Tables[0].Rows[0]["Message"].ToString(),
                            ds.Tables[0].Rows[0]["DaysAutoId"].ToString());

                    }
                    catch (Exception ex)
                    {
                        ResponseDataObj.setResponseData(401, "An error occurred. Try again.", null);
                    }
                }
                else
                {
                    ResponseDataObj.setResponseData(401, "Unauthorized Access.", null);
                }
            }
            else
            {
                ResponseDataObj.setResponseData(401, "Unauthorized Access.", null);
            }
            return Json(ResponseDataObj);
        }
       
        public JsonResult ShowDays(int? PackageAutoId)
        {

            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {
                        List<DaysDD> daysList = new List<DaysDD>();

                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spPackageMaster",
                             new SqlParameter("@opCode", "410"),
                             new SqlParameter("@PackageAutoId", PackageAutoId.ToString())

                            );
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {

                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                DaysDD model = new DaysDD();
                                {
                                    model.DaysAutoId = Convert.ToInt32(row["DaysAutoId"]);
                                    model.DaysName = row["DaysName"].ToString();
                                    model.TaskCount = row["TaskCount"].ToString();
                                }; daysList.Add(model);
                            }
                        }
                        ResponseDataObj.setResponseData(200, "Success", daysList);

                    }
                    catch (Exception ex)
                    {
                        ResponseDataObj.setResponseData(401, "An error occurred. Try again.", null);

                    }
                }
                else
                {
                    ResponseDataObj.setResponseData(401, "Unauthorized Access.", null);

                }
            }
            else
            {
                ResponseDataObj.setResponseData(401, "Unauthorized Access.", null);

            }
            return Json(ResponseDataObj);
        }
        public JsonResult Daydelete(string DaysAutoId)
        {
            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {

                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spPackageMaster",
                        new SqlParameter("@opCode", 302),
                          new SqlParameter("@DaysAutoId", DaysAutoId.ToString())
                          );

                        ResponseDataObj.setResponseData(ds.Tables[0].Rows[0]["MessageCode"].ToString(), ds.Tables[0].Rows[0]["Message"].ToString(), null);

                    }
                    catch (Exception ex)
                    {
                        ResponseDataObj.setResponseData(401, "An error occurred. Try again.", null);
                    }
                }
                else
                {
                    ResponseDataObj.setResponseData(401, "Unauthorized Access.", null);
                }
            }
            else
            {
                ResponseDataObj.setResponseData(401, "Unauthorized Access.", null);

            }
            return Json(ResponseDataObj);
        }
        public JsonResult EditdayData(string DaysAutoId)
        {
            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {
                        DaysDD model = new DaysDD();

                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spPackageMaster",
                            new SqlParameter("@opCode", 411),
                            new SqlParameter("@DaysAutoId", DaysAutoId)
                            );
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            model.DaysName = ds.Tables[0].Rows[0]["DaysName"].ToString();
                            model.PackageAutoId = ds.Tables[0].Rows[0]["PackageAutoId"].ToString();

                        }
                        ResponseDataObj.setResponseData(200, "Success", model);

                    }
                    catch (Exception ex)
                    {
                        ResponseDataObj.setResponseData(401, "An error occurred. Try again.", null);

                    }
                }
                else
                {
                    ResponseDataObj.setResponseData(401, "Unauthorized Access.", null);
                }
            }
            else
            {
                ResponseDataObj.setResponseData(401, "Unauthorized Access.", null);

            }
            return Json(ResponseDataObj);
        }
         
        public JsonResult InsertUpdateTask(int TaskAutoId, string TaskName, string DaysAutoId)
        {
            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {
                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spPackageMaster",
                        new SqlParameter("@opCode", TaskAutoId <= 0 ? 103 : 203),
                        new SqlParameter("@TaskAutoId", TaskAutoId.ToString()),
                        new SqlParameter("@TaskName", TaskName),
                        new SqlParameter("@DaysAutoId", DaysAutoId),
                        new SqlParameter("@CreatedBy", Session["ValidateUserID"].ToString())

                         );
                        ResponseDataObj.setResponseData(ds.Tables[0].Rows[0]["MessageCode"].ToString(), ds.Tables[0].Rows[0]["Message"].ToString(), null);

                    }
                    catch (Exception ex)
                    {
                        ResponseDataObj.setResponseData(401, "An error occurred. Try again.", null);
                    }
                }
                else
                {
                    ResponseDataObj.setResponseData(401, "Unauthorized Access.", null);
                }
            }
            else
            {
                ResponseDataObj.setResponseData(401, "Unauthorized Access.", null);
            }
            return Json(ResponseDataObj);
        }

        public JsonResult ShowTask(int DaysAutoId)
        {

            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {
                        List<TaskDD> TaskList = new List<TaskDD>();

                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spPackageMaster",
                             new SqlParameter("@opCode", "412"),
                             new SqlParameter("@DaysAutoId", DaysAutoId.ToString())

                            );
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {

                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                TaskDD model = new TaskDD();
                                {
                                    model.TaskAutoId = Convert.ToInt32(row["TaskAutoId"]);
                                    model.TaskName = row["TaskName"].ToString();
                                }; TaskList.Add(model);
                            }
                        }
                        ResponseDataObj.setResponseData(200, "Success", TaskList);

                    }
                    catch (Exception ex)
                    {
                        ResponseDataObj.setResponseData(401, "An error occurred. Try again.", null);

                    }
                }
                else
                {
                    ResponseDataObj.setResponseData(401, "Unauthorized Access.", null);

                }
            }
            else
            {
                ResponseDataObj.setResponseData(401, "Unauthorized Access.", null);

            }
            return Json(ResponseDataObj);
        }
        public JsonResult Taskdelete(string TaskAutoId)
        {
            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {

                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spPackageMaster",
                        new SqlParameter("@opCode", 303),
                          new SqlParameter("@TaskAutoId", TaskAutoId.ToString())
                          );

                        ResponseDataObj.setResponseData(ds.Tables[0].Rows[0]["MessageCode"].ToString(), ds.Tables[0].Rows[0]["Message"].ToString(), null);

                    }
                    catch (Exception ex)
                    {
                        ResponseDataObj.setResponseData(401, "An error occurred. Try again.", null);
                    }
                }
                else
                {
                    ResponseDataObj.setResponseData(401, "Unauthorized Access.", null);
                }
            }
            else
            {
                ResponseDataObj.setResponseData(401, "Unauthorized Access.", null);

            }
            return Json(ResponseDataObj);
        }
        public JsonResult EditTaskData(string TaskAutoId)
        {
            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {
                        TaskDD model = new TaskDD();

                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spPackageMaster",
                            new SqlParameter("@opCode", 413),
                            new SqlParameter("@TaskAutoId", TaskAutoId)
                            );
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            model.TaskName = ds.Tables[0].Rows[0]["TaskName"].ToString();
                            model.DaysAutoId = ds.Tables[0].Rows[0]["DaysAutoId"].ToString();

                        }
                        ResponseDataObj.setResponseData(200, "Success", model);

                    }
                    catch (Exception ex)
                    {
                        ResponseDataObj.setResponseData(401, "An error occurred. Try again.", null);

                    }
                }
                else
                {
                    ResponseDataObj.setResponseData(401, "Unauthorized Access.", null);
                }
            }
            else
            {
                ResponseDataObj.setResponseData(401, "Unauthorized Access.", null);

            }
            return Json(ResponseDataObj);
        }

    }
}