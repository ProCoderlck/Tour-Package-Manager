using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tour_Package_Manager.Models;
using System.Web.Services.Description;

namespace Tour_Package_Manager.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Add_User()
        {
            return View();
        }
        public ActionResult User_List()
        {
            return View();
        }
        public JsonResult UserInsertUpdate(int UserAutoId, string UserName, int MobileNo, string Email, string Password,
            int StatusAutoId, string UserImage)
        {

            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {

                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spUser",
                        new SqlParameter("@opCode", UserAutoId <= 0 ? 101 : 201),
                        new SqlParameter("@UserAutoId", UserAutoId.ToString()),
                        new SqlParameter("@UserName", UserName),
                        new SqlParameter("@MobileNo", MobileNo.ToString()),
                        new SqlParameter("@Email", Email),
                        new SqlParameter("@Password", Password),
                        new SqlParameter("@StatusAutoId", StatusAutoId.ToString()),
                        new SqlParameter("@UserImage", Utility.SaveBase64AsImage(UserImage, 5)),
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


        public JsonResult ShowUser()
        {

            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {
                        List<UserDD> Userlist = new List<UserDD>();
                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spUser",
                             new SqlParameter("@opCode", 401)
                            );
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {

                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                UserDD model = new UserDD();
                                {
                                    model.UserAutoId = Convert.ToInt32(row["UserAutoId"]);
                                    model.UserName = row["UserName"].ToString();
                                    model.MobileNo = Convert.ToInt32(row["MobileNo"].ToString());
                                    model.Email = row["Email"].ToString();
                                    model.StatusAutoId = row["StatusName"].ToString();
                                    model.UserImage = row["UserImage"].ToString();

                                }; Userlist.Add(model);
                            }
                        }
                        ResponseDataObj.setResponseData(200, "Success", Userlist);

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
        public JsonResult Userdelete(string UserAutoId)
        {
            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {

                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spUser",
                        new SqlParameter("@opCode", 301),
                          new SqlParameter("@UserAutoId", UserAutoId.ToString())
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
        public JsonResult EditUserData(string UserAutoId)
        {
            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {
                        UserDD model = new UserDD();

                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spUser",
                            new SqlParameter("@opCode", 402),
                            new SqlParameter("@UserAutoId", UserAutoId)
                            );
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                            model.MobileNo = Convert.ToInt32(ds.Tables[0].Rows[0]["MobileNo"].ToString());
                            model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                            model.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                            model.StatusAutoId = ds.Tables[0].Rows[0]["StatusAutoId"].ToString();
                            model.UserImage = ds.Tables[0].Rows[0]["UserImage"].ToString();

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


        public JsonResult getStatusList(string StatusType)
        {
            List<StatusForDD> statuslists = new List<StatusForDD>();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter("@OpCode", 401),
                new SqlParameter("@StatusType","User")
                };
                DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spStatusMaster", parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        statuslists.Add(new StatusForDD
                        {
                            StatusAutoId = Convert.ToInt32(row["StatusAutoId"].ToString()),
                            StatusName = row["StatusName"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ex = new Exception(ex.Message);
            }
            return Json(statuslists);
        }
    }

}