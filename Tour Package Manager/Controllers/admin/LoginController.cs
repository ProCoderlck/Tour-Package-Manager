using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Data.SqlClient;

using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Web.Helpers;

namespace Tour_Package_Manager.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        static string cs = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
        public ActionResult Login()
       {
            ViewBag.ErrorMessage = TempData["ErrorMessage"]?.ToString();
            return View();
        }

        [HttpPost]
        public ActionResult ValidateLogin(string Email, string Password)
        {
            if (ValidateUser(Email, Password))
            {

                // Successful login logic
                return RedirectToAction("Index", "Home");
            }
            if (Email == "" && Password == "")
            {
                TempData["ErrorMessage"] = "Please! enter TPM ID and password.";
                return RedirectToAction("Login", "Login");
            }

            else
            {
                // Store the error message temporarily
                TempData["ErrorMessage"] = "Invalid TPM ID or password.";
                return RedirectToAction("Login", "Login");
            }
        }
        private bool ValidateUser(string Email, string Password)
        {
            bool isValid = false;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                // Create a SQL command with the query and parameters
                SqlCommand command = new SqlCommand("spgetusers", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Email", Email);
                command.Parameters.AddWithValue("@UserPass", Password);

                try
                {
                    // Open the connection
                    connection.Open();
                    // Execute the query and get the result (1 if user exists, 0 otherwise)
                    //int result = (int)command.ExecuteScalar();
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        Session["ValidateUserID"] = Convert.ToInt32(dr["UserAutoId"].ToString());
                        Session["ValidateUserName"] = dr["UserName"].ToString();
                        Session["ValidateUserImage"] = dr["UserImage"].ToString();


                        isValid = true;
                    }
                    //If result is 1, the user is valid
                    //if (result == 1)
                    //{
                    //    isValid = true;
                    //}
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during connection or execution
                    ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
                }
            }

            return isValid;
        }

        public ActionResult Logout()
        {
            Session["ValidateUserID"] = null;
            return RedirectToAction("Login", "Login");
        }
    }
}