using Codelife.Data;
using Codelife.Models;
using Codelife.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

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
                    password = author.password,
                    profile = author.profile,
                    dateRegistered = author.dateRegistered
            };

                var createAuthorResult = new DAO.AccountController().AddAuthor(newAuthor).Result;

                if (createAuthorResult.IsSuccessStatusCode)
                {
                    //GET USER DETAILS
                    var user = createAuthorResult.Content.ReadAsAsync<bool>().Result;

                    if (user)
                    {
                        return Json(new { success = true, message = "success", user = user });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Failed" + user });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Fail to create Author" + createAuthorResult.RequestMessage + " " + createAuthorResult.ReasonPhrase });
                }
            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "Register", "Error Registering Author" + ex + "\n");
                return Json(new { success = false, message = "Fail to create Author" + ex });

            }
        }


        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }


    }

}