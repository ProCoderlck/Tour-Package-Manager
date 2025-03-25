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
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Add_Category()
        {
            return View();
        }
        public ActionResult Category_List()
        {
            return View();
        }

        public JsonResult CategoryInsertUpdate(int CategoryAutoId, string CategoryName, int StatusAutoId, string CategoryImage)
        {
            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {
                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spCategory",
                        new SqlParameter("@opCode", CategoryAutoId <= 0 ? 101 : 201),
                        new SqlParameter("@CategoryAutoId", CategoryAutoId.ToString()),
                        new SqlParameter("@CategoryName", CategoryName),
                        new SqlParameter("@CategoryImage", Utility.SaveBase64AsImage(CategoryImage, 3)),
                        new SqlParameter("@StatusAutoId", StatusAutoId.ToString()),
                        new SqlParameter("@createby", Session["ValidateUserID"].ToString())
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


        public JsonResult ShowCategory()
        {
            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {
                        List<CategoryDD> Categorylist = new List<CategoryDD>();

                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spCategory",
                             new SqlParameter("@opCode", 401)
                            );
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {

                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                CategoryDD model = new CategoryDD();
                                {
                                    model.CategoryAutoId = Convert.ToInt32(row["CategoryAutoId"]);
                                    model.CategoryName = row["CategoryName"].ToString();
                                    model.StatusAutoId = row["StatusName"].ToString();
                                    model.CategoryImage = row["CategoryImage"].ToString();
                                }; Categorylist.Add(model);
                            }
                        }
                        ResponseDataObj.setResponseData(200, "Success", Categorylist);

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
        public JsonResult Categorydelete(string CategoryAutoId)
        {
            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {

                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spCategory",
                             new SqlParameter("@opCode", 301),
                          new SqlParameter("@CategoryAutoId", CategoryAutoId.ToString())
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
        public JsonResult EditCategoryData(string CategoryAutoId)
        {
            ResponseData ResponseDataObj = new ResponseData();
            if (Session["ValidateUserID"] != null)
            {
                if (Session["ValidateUserID"].ToString() == "22")
                {
                    try
                    {
                        CategoryDD model = new CategoryDD();

                        DataSet ds = Common.ExecuteProcedureWithResultSets("Web_spCategory",
                            new SqlParameter("@opCode", 402),
                            new SqlParameter("@CategoryAutoId", CategoryAutoId)
                            );
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            model.CategoryName = ds.Tables[0].Rows[0]["CategoryName"].ToString();
                            model.StatusAutoId = ds.Tables[0].Rows[0]["StatusAutoId"].ToString();
                            model.CategoryImage = ds.Tables[0].Rows[0]["CategoryImage"].ToString();

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