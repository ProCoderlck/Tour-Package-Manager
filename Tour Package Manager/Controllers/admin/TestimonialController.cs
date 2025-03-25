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
    public class TestimonialController : Controller
    {
        // GET: Testimonial
        public ActionResult Add_Testimonial()
        {
            return View();
        } 

        public ActionResult Testimonial_List()
        {
            return View();
        }
        public JsonResult TestimonialInsertUpdate(int TestimonialAutoId, string Name, string Photo, string Discription,
            string Profession)
        {

            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {
                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spTestimonial",
                        new SqlParameter("@opCode", TestimonialAutoId <= 0 ? 101 : 201),
                        new SqlParameter("@TestimonialAutoId", TestimonialAutoId.ToString()),
                        new SqlParameter("@Name", Name),
                        new SqlParameter("@Discription", Discription),
                        new SqlParameter("@Profession", Profession),
                        new SqlParameter("@Photo", Utility.SaveBase64AsImage(Photo, 7)),
                        new SqlParameter("@CreatedBy", Session["ValidateUserID"].ToString())
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
        public JsonResult ShowTestimonial()
        {

            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {
                        List<TestimonialDD> Testimoniallist = new List<TestimonialDD>();
                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spTestimonial",
                             new SqlParameter("@opCode", 401)
                            );
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {

                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                TestimonialDD model = new TestimonialDD();
                                {
                                    model.TestimonialAutoId = Convert.ToInt32(row["TestimonialAutoId"]);
                                    model.Name = row["Name"].ToString();
                                    model.Profession = row["Profession"].ToString();
                                    model.Discription = row["Discription"].ToString();
                                    model.Photo = row["Photo"].ToString();

                                }; Testimoniallist.Add(model);
                            }
                        }
                        ResponseDataObj.setResponseData(200, "Success", Testimoniallist);

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
        public JsonResult Testimonialdelete(string TestimonialAutoId)
        {
            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {

                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spTestimonial",
                        new SqlParameter("@opCode", 301),
                          new SqlParameter("@TestimonialAutoId", TestimonialAutoId.ToString())
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
        public JsonResult EditTestimonailData(string TestimonialAutoId)
        {
            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {
                        TestimonialDD model = new TestimonialDD();

                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spTestimonial",
                            new SqlParameter("@opCode", 402),
                            new SqlParameter("@TestimonialAutoId", TestimonialAutoId)
                            );
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                            model.Profession = ds.Tables[0].Rows[0]["Profession"].ToString();
                            model.Discription = ds.Tables[0].Rows[0]["Discription"].ToString();
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