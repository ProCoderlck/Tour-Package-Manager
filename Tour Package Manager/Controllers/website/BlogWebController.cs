using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tour_Package_Manager.Models;

namespace Tour_Package_Manager.Controllers.website
{
    public class BlogWebController : Controller
    {
        // GET: BlogWeb
        public ActionResult Blogweblist()
        {
            return View();
        }

        public JsonResult ShowBlog()
        {
            List<BlogDD> Bloglist = new List<BlogDD>();
            try
            {
                DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spBlog",
                new SqlParameter("@opCode", 405));
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        BlogDD model = new BlogDD();
                        {
                            model.BlogAutoId = Convert.ToInt32(row["BlogAutoId"]);
                            model.BlogTitle = row["BlogTitle"].ToString();
                            model.UserAutoId = row["UserName"].ToString();
                            model.BlogCatagoryAutoId = row["CategoryName"].ToString();
                            model.Day = row["D"].ToString();
                            model.Month = row["M"].ToString();
                            model.Photo = row["Photo"].ToString();
                            model.Discription = row["Discription"].ToString();
                        }; Bloglist.Add(model);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(Bloglist);
        }

        public ActionResult BlogDetail()
        {
            return View();
        }
        public JsonResult ShowBlogdetail(string BlogAutoId)
        {
            BlogDD model = new BlogDD();
            try
            {
                DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spBlog",
                    new SqlParameter("@opCode", 406),
                    new SqlParameter("@BlogAutoId", BlogAutoId)
                    );
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.BlogTitle = ds.Tables[0].Rows[0]["BlogTitle"].ToString();
                    model.UserAutoId = ds.Tables[0].Rows[0]["UserName"].ToString();
                    model.BlogCatagoryAutoId = ds.Tables[0].Rows[0]["CategoryName"].ToString();
                    model.Discription = ds.Tables[0].Rows[0]["Discription"].ToString();
                    model.Day = ds.Tables[0].Rows[0]["D"].ToString();
                    model.Month = ds.Tables[0].Rows[0]["M"].ToString();
                    model.Photo = ds.Tables[0].Rows[0]["Photo"].ToString();
                    model.UserImage = ds.Tables[0].Rows[0]["UserImage"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(model);
        }

        public JsonResult blogcategory()
        {

            List<BlogCategoryDD> Bloglist = new List<BlogCategoryDD>();

            try
            {
                DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spBlog",
                     new SqlParameter("@opCode", 407)

                    );
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        BlogCategoryDD model = new BlogCategoryDD();
                        {
                            model.CategoryName = row["CategoryName"].ToString();
                        }; Bloglist.Add(model);
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return Json(Bloglist);
        }
        public JsonResult showblogtitle()
        {

            List<BlogDD> Bloglist = new List<BlogDD>();

            try
            {
                DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spBlog",
                     new SqlParameter("@opCode", 408)

                    );
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        BlogDD model = new BlogDD();
                        {
                            model.BlogTitle = row["BlogTitle"].ToString();
                            model.Date = row["Date"].ToString();
                            model.Photo = row["Photo"].ToString();
                        }; Bloglist.Add(model);
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return Json(Bloglist);
        }

    }
}