using Codelife.Data;
using Codelife.Models;
using Codelife.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using BC = BCrypt.Net.BCrypt;

namespace Codelife.Controllers
{
    public class AccountController : Controller
    {
        private static string ModuleName = "AccountController";

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(Author author)
        {
            try
            {
                Author newAuthor = new Author
                {
                    email = author.email,
                    username = author.username,
                    password = BC.HashPassword(author.password),
                    profile = author.profile,
                    dateRegistered = DateTime.Today
            };

                var isEmailAvailableResult = new DAO.AccountController().IsEmailAvailable(author.email).Result;
                if (isEmailAvailableResult.IsSuccessStatusCode)
                {
                    bool response = isEmailAvailableResult.Content.ReadAsAsync<bool>().Result;

                    if (response == true)
                    {

                        var createAuthorResult = new DAO.AccountController().AddAuthor(newAuthor).Result;
                        if (createAuthorResult.IsSuccessStatusCode)
                        {
                            //GET USER DETAILS
                            var user = createAuthorResult.Content.ReadAsAsync<bool>().Result;

                            if (user)
                            {
                                return Json(new { success = true, message = "Registeration successful, We are honoured and excited having you as a member" });
                            }
                            else
                            {
                                return Json(new { success = false, message = "Registration Failed, Please Try Again" });
                            }
                        }
                        else
                        {
                            return Json(new { success = false, message = "Registration Failed, Please Try Again" });
                        }
                    }
                    else
                    {
                        return Json(new { success = false, message = "Registration Failed , Email already exists" });
                    }

                }
                else
                {
                    new Logger().LogError(ModuleName, "Register", "Error Registering Author - Email Registered Already or Server Error" + "Status Code: " + isEmailAvailableResult.StatusCode + "\n");
                    return Json(new { success = false, message = "Registration Failed , Please Try Again" });
                }

            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "Register", "Error Registering Author" + ex + "\n");
                return Json(new { success = false, message = "Registration failed, Please Try Again" });

            }
        }


        //Check if Email Exits
        [HttpPost]
        public JsonResult IsEmailAvailable(string email)
        {
            var isEmailAvailableResult = new DAO.AccountController().IsEmailAvailable(email).Result;
            bool response = false;

            if (isEmailAvailableResult.IsSuccessStatusCode)
            {
                response = isEmailAvailableResult.Content.ReadAsAsync<bool>().Result;
            }

            return Json(new { success = true, data = response });

        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Login author)
        {
            try
            {
                var response = new DAO.AccountController().GetAuthorByEmail(author.email).Result;

                if (response.IsSuccessStatusCode)
                {
                    var authorDetails = response.Content.ReadAsAsync<Author>().Result;

                    if (authorDetails != null)
                    {

                        if (BC.Verify(author.password, authorDetails.password))
                        {
                            Session["AUTHORID"] = authorDetails.authorId;
                            Session.Timeout = 25;
                            Session["AUTHOR"] = authorDetails;

                            return Json(new { success = true, message = "Login Successful, Welcome Back " + authorDetails.username });
                        }
                        else
                        {
                            return Json(new { success = false, message = "Login Failed , Invalid Email or Password" });
                        }
                    }
                    else
                    {
                        return Json(new { success = false, message = "Login Failed , Invalid Email or Password" });
                    }
                }
                else
                {
                    new Logger().LogError(ModuleName, "Register", "Error Login - Invalid Credentials or Server Error" + "Status Code: " + response.StatusCode + "\n");
                    return Json(new { success = false, message = "Login Failed , Please Try Again" });
                }

            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "Login", "Error Login " + ex + "\n");
                return Json(new { success = false, message = "Login failed, Please Try Again" });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult LoginUser(string email , string password)
        {
            try
            {
                var response = new DAO.AccountController().GetAuthorByEmail(email).Result;

                if (response.IsSuccessStatusCode)
                {
                    var authorDetails = response.Content.ReadAsAsync<Author>().Result;

                    if (authorDetails != null)
                    {

                        if (BC.Verify(password, authorDetails.password))
                        {
                            Session["AUTHORID"] = authorDetails.authorId;
                            Session.Timeout = 25;
                            Session["AUTHOR"] = authorDetails;

                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return RedirectToAction("Login");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Login");

                    }
                }
                else
                {
                    new Logger().LogError(ModuleName, "Register", "Error LoginUser - Invalid Credentials or Server Error" + "Status Code: " + response.StatusCode + "\n");
                    return RedirectToAction("Login");

                }

            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "LoginUser", "Error LoginUser " + ex + "\n");
                return RedirectToAction("Login");

            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session["AUTHORID"] = null;
            Session["AUTHOR"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}

