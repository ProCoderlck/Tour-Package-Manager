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
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Add_Blog()
        {
            return View();
        }
        public ActionResult Blog_List()
        {
            return View();
        }
       
        public JsonResult BlogInsertUpdate(int BlogAutoId, string BlogTitle, int UserAutoId, string Photo, string Date,
          int BlogCatagoryAutoId, string Discription)
        {

            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {

                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spBlog",
                        new SqlParameter("@opCode", BlogAutoId <= 0 ? 101 : 201),
                        new SqlParameter("@BlogAutoId", BlogAutoId.ToString()),
                        new SqlParameter("@BlogTitle", BlogTitle),
                        new SqlParameter("@UserAutoId", UserAutoId.ToString()),
                        new SqlParameter("@Date", Date),
                        new SqlParameter("@Discription", Discription),
                        new SqlParameter("@BlogCatagoryAutoId", BlogCatagoryAutoId.ToString()),
                        new SqlParameter("@Photo", Utility.SaveBase64AsImage(Photo, 6)),
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


        public JsonResult ShowBlog()
        {

            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {
                        List<BlogDD> Bloglist = new List<BlogDD>();
                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spBlog",
                             new SqlParameter("@opCode", 401)
                            );
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {

                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                BlogDD model = new BlogDD();
                                {
                                    model.BlogAutoId = Convert.ToInt32(row["BlogAutoId"]);
                                    model.BlogTitle = row["BlogTitle"].ToString();
                                    model.UserAutoId = row["UserName"].ToString();
                                    model.BlogCatagoryAutoId =row["CategoryName"].ToString();
                                    model.Discription = row["Discription"].ToString();
                                    model.Date = row["Date"].ToString();
                                    model.Photo = row["Photo"].ToString();

                                }; Bloglist.Add(model);
                            }
                        }
                        ResponseDataObj.setResponseData(200, "Success", Bloglist);

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
        public JsonResult Blogdelete(string BlogAutoId)
        {
            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {

                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spBlog",
                        new SqlParameter("@opCode", 301),
                          new SqlParameter("@BlogAutoId", BlogAutoId.ToString())
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
        public JsonResult EditBlogData(string BlogAutoId)
        {
            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {
                        BlogDD model = new BlogDD();

                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spBlog",
                            new SqlParameter("@opCode", 402),
                            new SqlParameter("@BlogAutoId", BlogAutoId)
                            );
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            model.BlogTitle = ds.Tables[0].Rows[0]["BlogTitle"].ToString();
                            model.UserAutoId = ds.Tables[0].Rows[0]["UserAutoId"].ToString();
                            model.BlogCatagoryAutoId =ds.Tables[0].Rows[0]["BlogCatagoryAutoId"].ToString();
                            model.Discription = ds.Tables[0].Rows[0]["Discription"].ToString();
                            model.Date = ds.Tables[0].Rows[0]["Date"].ToString();
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
        public JsonResult getBlogcategoryList()
        {
            List<BlogCategoryForDD> BlogCategorylists = new List<BlogCategoryForDD>();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter("@OpCode", 403),

                };
                DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spBlog", parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        BlogCategorylists.Add(new BlogCategoryForDD
                        {
                            BlogCategoryAutoId = Convert.ToInt32(row["BlogCategoryAutoId"].ToString()),
                            CategoryName = row["CategoryName"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ex = new Exception(ex.Message);
            }
            return Json(BlogCategorylists);
        }

        public JsonResult getUserList()
        {
            List<UserForDD> Userlists = new List<UserForDD>();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter("@OpCode", 404),

                };
                DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spBlog", parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Userlists.Add(new UserForDD
                        {
                            UserAutoId = Convert.ToInt32(row["UserAutoId"].ToString()),
                            UserName = row["UserName"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ex = new Exception(ex.Message);
            }
            return Json(Userlists);
        }

        public JsonResult BlogCategoryInsertUpdate(int BlogCategoryAutoId, string CategoryName, int StatusAotoId)
        {

            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    { 

                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spBlogCategory",
                        new SqlParameter("@opCode", BlogCategoryAutoId <= 0 ? 101 : 201),
                        new SqlParameter("@BlogCategoryAutoId", BlogCategoryAutoId.ToString()),
                        new SqlParameter("@CategoryName", CategoryName),
                        new SqlParameter("@StatusAutoId", StatusAotoId.ToString()),
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
        public JsonResult ShowBlogCategory()
        {

            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {
                        List<BlogCategoryDD> BlogCategorylist = new List<BlogCategoryDD>();
                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spBlogCategory",
                             new SqlParameter("@opCode", 401)
                            );
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {

                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                BlogCategoryDD model = new BlogCategoryDD();
                                {
                                    model.BlogCategoryAutoId = Convert.ToInt32(row["BlogCategoryAutoId"]);
                                    model.CategoryName = row["CategoryName"].ToString();
                                    model.Status = row["StatusName"].ToString();
                                
                                }; BlogCategorylist.Add(model);
                            }
                        }
                        ResponseDataObj.setResponseData(200, "Success", BlogCategorylist);

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


        public JsonResult BlogCategorydelete(int BlogCategoryAutoId)
        {
            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {

                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spBlogCategory ",
                        new SqlParameter("@opCode", 301),
                          new SqlParameter("@BlogCategoryAutoId", BlogCategoryAutoId.ToString())
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
        public JsonResult EditBlogCategoryData(string BlogCategoryAutoId)
        {
            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {
                        BlogCategoryDD model = new BlogCategoryDD();

                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spBlogCategory",
                            new SqlParameter("@opCode", 402),
                            new SqlParameter("@BlogCategoryAutoId", BlogCategoryAutoId)
                            );
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            model.CategoryName = ds.Tables[0].Rows[0]["CategoryName"].ToString();
                            model.Status = ds.Tables[0].Rows[0]["StatusAotoId"].ToString();

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