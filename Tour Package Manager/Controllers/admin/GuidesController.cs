using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tour_Package_Manager.Models;

namespace Tour_Package_Manager.Controllers.admin
{
    public class GuidesController : Controller
    {
        // GET: Guides
        public ActionResult Add_Guides()
        {
            return View();
        }
        public ActionResult Guides_List()
        {
            return View();
        }

        public JsonResult GuideInsertUpdate(int GuideAutoId, string GuideName, string Photo, string Designation)
        {
            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {
                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spGuide",
                        new SqlParameter("@opCode", GuideAutoId <= 0 ? 101 : 201),
                        new SqlParameter("@GuideAutoId", GuideAutoId.ToString()),
                        new SqlParameter("@GuideName", GuideName),
                        new SqlParameter("@Designation", Designation),
                        new SqlParameter("@Photo", Utility.SaveBase64AsImage(Photo, 8)),
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
        public JsonResult ShowGuide()
        {

            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {
                        List<GuideDD> Guidelist = new List<GuideDD>();
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
                        ResponseDataObj.setResponseData(200, "Success", Guidelist);

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
        public JsonResult Guidedelete(string GuideAutoId)
        {
            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {

                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spGuide",
                        new SqlParameter("@opCode", 301),
                          new SqlParameter("@GuideAutoId", GuideAutoId.ToString())
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
        public JsonResult EditGuideData(string GuideAutoId)
        {
            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {
                        GuideDD model = new GuideDD();

                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spGuide",
                            new SqlParameter("@opCode", 402),
                            new SqlParameter("@GuideAutoId", GuideAutoId)
                            );
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            model.GuideName = ds.Tables[0].Rows[0]["GuideName"].ToString();
                            model.Designation = ds.Tables[0].Rows[0]["Designation"].ToString();
                            model.Photo = ds.Tables[0].Rows[0]["Photo"].ToString();

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
