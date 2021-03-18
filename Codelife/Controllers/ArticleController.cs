using Codelife.Data;
using Codelife.Models;
using Codelife.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Codelife.Controllers
{
    public class ArticleController : Controller
    {
        private static string ModuleName = "ArticleController";

        [HttpGet]
        public ActionResult PostArticle()
        {
            var author = Session["AUTHORID"] != null ? (Session["AUTHORID"]) : null;
            if (author != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult ArticleById(int articleId)
        {
            try
            {
                var response = new DAO.ArticleController().GetArticleById(articleId).Result;
                Post article = new Post();

                if (response.IsSuccessStatusCode)
                {
                    article = response.Content.ReadAsAsync<Post>().Result;

                    if (article != null)
                    {

                        return View(article);
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
                new Logger().LogError(ModuleName, "ArticleById", "Error Getting Article By Id " + articleId + " " + ex + "\n");
                return RedirectToAction("NotFound", "Home");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult EditArticle(int articleId, int authorId)
        {
            try
            {
                Post article = new Post();

                var author = Session["AUTHORID"] != null ? (Session["AUTHORID"]) : null;
                if (author != null)
                {
                    var response = new DAO.ArticleController().GetArticleById(articleId).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        article = response.Content.ReadAsAsync<Post>().Result;

                        if (article != null)
                        {

                            if (article.authorId == authorId)
                            {
                                return View(article);
                            }
                            else
                            {
                                return RedirectToAction("Index", "Home");

                            }
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "EditArticle", "Error Getting Article By Id " + ex + "\n");
                return RedirectToAction("NotFound", "Home");
            }
        }



        [HttpPost]
        [AllowAnonymous]
        public ActionResult EditArticle(Post article)
        {
            try
            {
                if (article != null)
                {
                    var authorId = Session["AUTHORID"] != null ? (Session["AUTHORID"]) : null;

                    StringBuilder text = new StringBuilder();

                    // Encode text
                    text.Append(HttpUtility.HtmlEncode(article.text));
                    text.Replace("&lt;b&gt;", "<b>");
                    text.Replace("&lt;/b&gt;", "</b>");
                    text.Replace("&lt;br/&gt;", "<br/>");
                    text.Replace("&lt;em&gt;", "<em>");
                    text.Replace("&lt;/em&gt;", "</em>");
                    text.Replace("&lt;u&gt;", "<u>");
                    text.Replace("&lt;/u&gt;", "</u>");
                    text.Replace("&lt;h4&gt;", "<h4>");
                    text.Replace("&lt;/h4&gt;", "</h4>");
                    text.Replace("&lt;p&gt;", "<p>");
                    text.Replace("&lt;/p&gt;", "</p>");


                    article.text = text.ToString();

                    if (authorId != null)
                    {
                        if (authorId.ToString() == article.authorId.ToString())
                        {
                            article.updateDate = DateTime.Today;
                            var response = new DAO.ArticleController().UpdateArticle(article, Int32.Parse(authorId.ToString())).Result;

                            if (response.IsSuccessStatusCode)
                            {
                                var update = response.Content.ReadAsAsync<bool>().Result;

                                if (update == true)
                                {
                                    return Json(new { success = true, message = "Article Update Successful" });
                                }
                                else
                                {
                                    new Logger().LogError(ModuleName, "EditArticle", "Error Editing Article - false" + "\n");
                                    return Json(new { success = false, message = "Edit Article Failed, Please Try Again" });
                                }
                            }
                            else
                            {
                                new Logger().LogError(ModuleName, "EditArticle", "Error EditArticle - Unauthorized or Server Error" + "Status Code: " + response.StatusCode + "\n");
                                return Json(new { success = false, message = "Edit Article Failed , Please Try Again" });
                            }
                        }
                        else
                        {
                            return Json(new { success = false, message = "Edit Article Failed- Not Authorized , Please Try Again" + authorId.ToString() + article.authorId.ToString() });
                        }
                    }
                    else
                    {
                        return RedirectToAction("index", "Home");
                    }
                }
                else
                {
                    new Logger().LogError(ModuleName, "EditArticle", "Error Editing Article " + "\n");
                    return Json(new { success = false, message = "Edit Article Failed, Please Provide all necessary Details" });
                }

            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "EditArticle", "Error Editing Article " + ex + "\n");
                return Json(new { success = false, message = "Edit Article failed - Server Error, Please Try Again" });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult PostArticle(Post article)
        {
            try
            {
                article.createDate = DateTime.Today;
                article.updateDate = DateTime.Today;

                StringBuilder text = new StringBuilder();

                // Encode text
                text.Append(HttpUtility.HtmlEncode(article.text));
                text.Replace("&lt;b&gt;", "<b>");
                text.Replace("&lt;/b&gt;", "</b>");
                text.Replace("&lt;br/&gt;", "<br/>");
                text.Replace("&lt;em&gt;", "<em>");
                text.Replace("&lt;/em&gt;", "</em>");
                text.Replace("&lt;u&gt;", "<u>");
                text.Replace("&lt;/u&gt;", "</u>");
                text.Replace("&lt;h4&gt;", "<h4>");
                text.Replace("&lt;/h4&gt;", "</h4>");
                text.Replace("&lt;p&gt;", "<p>");
                text.Replace("&lt;/p&gt;", "</p>");


                article.text = text.ToString();

                var postArticleResult = new DAO.ArticleController().AddArticle(article).Result;

                if (postArticleResult.IsSuccessStatusCode)
                {
                    //GET ARTICLE 
                    var result = postArticleResult.Content.ReadAsAsync<bool>().Result;

                    if (result)
                    {
                        return Json(new { success = true, message = "Article Posted successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Posting Article failed, Please Try Again" });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Posting Article Failed, Please Try Again" });
                }
            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "PostArticle", "Error Posting Article " + ex + "\n");
                return Json(new { success = false, message = "Posting Article Failed, Please Try Again" });

            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult DeleteArticle(int articleId, int authorId)
        {
            try
            {

                var result = new DAO.ArticleController().DeleteArticle(articleId, authorId).Result;

                if (result.IsSuccessStatusCode)
                {
                    //GET ARTICLE 
                    var status = result.Content.ReadAsAsync<bool>().Result;

                    if (status)
                    {
                        return Json(new { success = true, message = "Article Deleted successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Deleting Article failed, Please Try Again" });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Deleting Article Failed, Please Try Again" });
                }
            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "DeleteArticle", "Error Deleting Article " + ex + "\n");
                return Json(new { success = false, message = "Deleting Article Failed, Please Try Again" });

            }
        }

    }
}