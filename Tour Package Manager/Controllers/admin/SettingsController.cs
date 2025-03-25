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
    public class SettingsController : Controller
    {
        // GET: Settings
        public ActionResult Add_Settings()
        {
            return View();
        }

        public JsonResult SettingsUpdate(string Email, string PhoneNumber, string Location, string Insatgram, string Facebook,
        string Youtube, string Linkddin,string Twitter)
        {

            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {

                        DataSet ds = Common.ExecuteProcedureWithResultSets("LayoutMasterWeb",
                        new SqlParameter("@opCode", 201),
                        new SqlParameter("@Email", Email),
                        new SqlParameter("@PhoneNumber", PhoneNumber),
                        new SqlParameter("@Location", Location),
                        new SqlParameter("@Insatgram", Insatgram),
                        new SqlParameter("@Facebook", Facebook),
                        new SqlParameter("@Youtube", Youtube),
                        new SqlParameter("@Linkddin", Linkddin),
                        new SqlParameter("@Twitter", Twitter),
                        new SqlParameter("@createby", Session["ValidateUserID"].ToString())
                         );
                        ResponseDataObj.setResponseData(ds.Tables[0].Rows[0]["MessageCode"].ToString(), ds.Tables[0].Rows[0]["Message"].ToString(), null);

                    }
                    catch (Exception e)
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

        public JsonResult EditSettingsData()
        {
            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {
                        LayoutMasterWebsite model = new LayoutMasterWebsite();

                        DataSet ds = Common.ExecuteProcedureWithResultSets("LayoutMasterWeb",
                            new SqlParameter("@opCode", 402)
                            );
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                            model.PhoneNumber = ds.Tables[0].Rows[0]["PhoneNumber"].ToString();
                            model.Location = ds.Tables[0].Rows[0]["Location"].ToString();
                            model.Insatgram = ds.Tables[0].Rows[0]["Insatgram"].ToString();
                            model.Facebook = ds.Tables[0].Rows[0]["Facebook"].ToString();
                            model.Twitter = ds.Tables[0].Rows[0]["Twitter"].ToString();
                            model.Linkddin = ds.Tables[0].Rows[0]["Linkddin"].ToString();
                            model.Youtube = ds.Tables[0].Rows[0]["Youtube"].ToString();

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