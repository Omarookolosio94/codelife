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
    public class ProfileController : Controller
    {
        private static string ModuleName = "AccountController";

        [HttpGet]
        [AllowAnonymous]
        public ActionResult AuthorProfile(int authorId)
        {
            try
            {
                var response = new DAO.ProfileController().GetAuthorProfile(authorId).Result;
                Profile profile = new Profile();

                if (response.IsSuccessStatusCode)
                {
                    var authorDetails = response.Content.ReadAsAsync<Author>().Result;

                    if (authorDetails != null)
                    {
                        //get author articles
                        var articleResponse = new DAO.ArticleController().GetArticleByAuthorId(authorId).Result;

                        if (articleResponse.IsSuccessStatusCode)
                        {
                            var authorArticles = articleResponse.Content.ReadAsAsync<List<Post>>().Result;

                            if (authorArticles != null)
                            {
                                profile.Posts = authorArticles;
                            }
                            else
                            {
                                profile.Posts = null;
                            }

                        }
                        else
                        {
                            profile.Posts = null;
                        }

                        profile.authorId = authorDetails.authorId;
                        profile.email = authorDetails.email;
                        profile.username = authorDetails.username;
                        profile.dateRegistered = authorDetails.dateRegistered;
                        profile.profile = authorDetails.profile;

                        return View(profile);


                    }
                    else
                    {
                        return RedirectToAction("NotFound", "Home");

                    }
                }
                else
                {
                    return RedirectToAction("NotFound", "Home");

                }

            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "GetAuthorProfile", "Error GettingAuthorProfiile - Server Error" + "Status Code: " + ex + "\n");
                return RedirectToAction("NotFound", "Home");
            }

        }

        [HttpGet]
        public ActionResult Authors()
        {

            var response = new DAO.ProfileController().GetAuthors().Result;
            List<Author> authors = new List<Author>();

            if (response.IsSuccessStatusCode)
            {
                var authorDetails = response.Content.ReadAsAsync<List<Author>>().Result;

                if (authorDetails != null)
                {
                    authors = authorDetails;
                }
                else
                {
                    authors = null;
                }
            }
            else
            {
                authors = null;
            }

            return View(authors);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult UpdateAuthor(EditAuthor editAuthor)
        {
            try
            {
                if (editAuthor != null)
                {
                    var author = (Session["AUTHOR"] != null) ? ((Author)Session["AUTHOR"]) : null;

                    if (author != null)
                    {
                        author.username = editAuthor.username;
                        author.profile = editAuthor.profile;

                        var response = new DAO.ProfileController().UpdateAuthor(author.authorId, author).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            var update = response.Content.ReadAsAsync<bool>().Result;

                            if (update == true)
                            {

                                Session["AUTHORID"] = author.authorId;
                                Session.Timeout = 25;
                                Session["AUTHOR"] = author;
                                return Json(new { success = true, message = "Profile Update Successful" });
                            }
                            else
                            {
                                new Logger().LogError(ModuleName, "updateAuthorProfile", "Error Updating Author Profile Update false" + "\n");
                                return Json(new { success = false, message = "Profile Update Failed, Please Try Again" });
                            }
                        }
                        else
                        {
                            new Logger().LogError(ModuleName, "UpdateAuthorProfile", "Error UpdateAuthorProfiile - Invalid Credentials or Server Error" + "Status Code: " + response.StatusCode + "\n");
                            return Json(new { success = false, message = "Profile Update Failed  Status Error, Please Try Again" });
                        }

                    }
                    else
                    {
                        return Json(new { success = false, message = "Login To Edit Profile" });

                    }
                }
                else
                {
                    new Logger().LogError(ModuleName, "updateAuthorProfile", "Error Updating Author Profile " + "\n");
                    return Json(new { success = false, message = "Profile Update Failed , Please Provide all necessary Details" });
                }

            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "updateAuthorProfile", "Error Updating Author Profile " + ex + "\n");
                return Json(new { success = false, message = "Profile Update failed - Server Error, Please Try Again" });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult DeleteAuthor(int authorId)
        {
            try
            {

                var articleDeleteResult = new DAO.ArticleController().DeleteArticleByAuthorId(authorId).Result;

                if (articleDeleteResult.IsSuccessStatusCode)
                {
                    var result = new DAO.ProfileController().DeleteAuthor(authorId).Result;

                    if (result.IsSuccessStatusCode)
                    {
                        //GET ARTICLE 
                        var status = result.Content.ReadAsAsync<bool>().Result;

                        if (status)
                        {
                            Session["AUTHORID"] = null;
                            Session["AUTHOR"] = null;
                            return Json(new { success = true, message = "Your Profile has been Deleted and all records cleared," });

                        }
                        else
                        {
                            return Json(new { success = false, message = "Deleting Your Profile failed, Please Try Again" });
                        }
                    }
                    else
                    {
                        return Json(new { success = false, message = "Deleting Your Profile failed, Please Try Again" });

                    }

                }
                else
                {
                    return Json(new { success = false, message = "Deleting Your Profile Failed, Please Try Again" });
                }
            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "DeleteAuthor", "Error Deleting Author " + ex + "\n");
                return Json(new { success = false, message = "Deleting Your Profile Failed, Please Try Again" });

            }
        }
    }
}