using Codelife.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Codelife.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var response = new DAO.ArticleController().GetAllArticle().Result;
            List<Post> articles = new List<Post>();

            if (response.IsSuccessStatusCode)
            {
                var articleDetails = response.Content.ReadAsAsync<List<Post>>().Result;

                if (articleDetails != null)
                {
                    articles = articleDetails;
                }
                else
                {
                    articles = null;
                }
            }
            else
            {
                articles = null;
            }

            return View(articles);
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}