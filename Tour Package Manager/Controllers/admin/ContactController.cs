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
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Contact_List()
        {
            return View();
        }
        public JsonResult ShowContact()
        {
            List<ContactDD> Contactlist = new List<ContactDD>();
            try
            {
                DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spContact",
                     new SqlParameter("@opCode", 401)
                    );
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ContactDD model = new ContactDD();
                        {
                            model.ContactAutoId = Convert.ToInt32(row["ContactAutoId"]);
                            model.Name = row["Name"].ToString();
                            model.Email = row["Email"].ToString();
                            model.Subject = row["Subject"].ToString();
                            model.status = row["status"].ToString();


                        }; Contactlist.Add(model);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(Contactlist);
        }

        public JsonResult ShowDetailsContact(string ContactAutoId)
        {
            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {    
                    try
                    {
                        ContactDD model = new ContactDD();

                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spContact",
                            new SqlParameter("@opCode", 402),
                            new SqlParameter("@ContactAutoId", ContactAutoId)
                            );
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                           
                            model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                            model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                            model.Subject = ds.Tables[0].Rows[0]["Subject"].ToString();
                            model.Message = ds.Tables[0].Rows[0]["Message"].ToString();
                            model.status = ds.Tables[0].Rows[0]["status"].ToString();
                           
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
       
        public JsonResult EditcontactstatusData(int? ContactAutoId,int ContactStatusAutoId)
        {

            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {

                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spContact",
                           new SqlParameter("@opCode", 202),
                            new SqlParameter("@ContactStatusAutoId", ContactStatusAutoId),
                            new SqlParameter("@ContactAutoId", ContactAutoId)

                            );


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
        public JsonResult Contactdelete(string ContactAutoId)
        {
            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {

                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spContact",
                        new SqlParameter("@opCode", 301),
                          new SqlParameter("@ContactAutoId", ContactAutoId.ToString())
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
    }
}