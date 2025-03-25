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
    public class TestimonialWebController : Controller
    {
        // GET: TestimonialWeb
        public ActionResult TestimonailList()
        {
            return View();
        }
        public JsonResult ShowTestimonialweb()
        {
            List<TestimonialDD> Testimoniallist = new List<TestimonialDD>();
            try
            {
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
            }
            catch (Exception ex)
            {

            }
            return Json(Testimoniallist);
        }
    }
}