using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using Codelife.Data;
using Codelife.Services.Interfaces;
using Codelife.Services.Concretes;
using Codelife.Repositories.Concretes;
using System.Web.Http;
using System.Threading.Tasks;
using System.Net;
using Codelife.Models;
using Codelife.Utility;

namespace Codelife.DAO
{
    [RoutePrefix("api")]
    public class ArticleController : ApiController
    {
        private readonly CodelifeDbContext dbContext = new CodelifeDbContext();
        private readonly IArticleService _articleService;

        HttpRequestMessage Request;
        HttpConfiguration configuration;

        string ModuleName = "ApiController";

        public ArticleController()
        {
            _articleService = new ArticleService(new ArticleRepository(dbContext));
        }

        [HttpPost]
        [Route("AddArticle")]
        public async Task<HttpResponseMessage> AddArticle(Post article)
        {
            try
            {
                Request = new HttpRequestMessage();
                configuration = new HttpConfiguration();
                Request.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = configuration;

                var result = await _articleService.AddArticle(article);

                bool status = false;

                if (result)
                {
                    status = true;
                    return Request.CreateResponse(HttpStatusCode.OK, status);
                }
                return Request.CreateResponse(HttpStatusCode.OK, status);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unable to Post Article!");
            }
        }

        [HttpPost]
        [Route("GetAllArticle")]
        public async Task<HttpResponseMessage> GetAllArticle()
        {
            try
            {
                Request = new HttpRequestMessage();
                configuration = new HttpConfiguration();
                Request.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = configuration;

                var result = await _articleService.GetAllArticle();
                return Request.CreateResponse<List<Post>>(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unable to Get All Articles!");

            }

        }


        [HttpPost]
        [Route("GetArticleById")]
        public async Task<HttpResponseMessage> GetArticleById(int postId)
        {
            try
            {
                Request = new HttpRequestMessage();
                configuration = new HttpConfiguration();
                Request.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = configuration;

                var result = await _articleService.GetArticleById(postId);
                return Request.CreateResponse<Post>(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unable to Get Article" + postId);

            }

        }

        [HttpPost]
        [Route("GetArticleByAuthorId")]
        public async Task<HttpResponseMessage> GetArticleByAuthorId(int authorId)
        {
            try
            {
                Request = new HttpRequestMessage();
                configuration = new HttpConfiguration();
                Request.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = configuration;

                var result = await _articleService.GetArticleByAuthorId(authorId);
                return Request.CreateResponse<List<Post>>(HttpStatusCode.OK, result);


            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unable to Get All Articles!");

            }

        }


        [HttpPost]
        [Route("UpdateArticle")]
        public async Task<HttpResponseMessage> UpdateArticle(Post article , int authorId)
        {
            try
            {
                Request = new HttpRequestMessage();
                configuration = new HttpConfiguration();
                Request.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = configuration;

                var result = await _articleService.UpdateArticle(article, authorId);

                bool status = false;

                if (result)
                {
                    status = true;
                    return Request.CreateResponse(HttpStatusCode.OK, status);
                }
                return Request.CreateResponse(HttpStatusCode.OK, status);
            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "UpdateArticle", "Error UpdateArticle - Server Error" + "Status Code: " + ex + "\n");
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unable to Update Author!");
            }
        }

        [HttpPost]
        [Route("DeleteArticle")]
        public async Task<HttpResponseMessage> DeleteArticle(int postId , int authorId)
        {
            try
            {
                Request = new HttpRequestMessage();
                configuration = new HttpConfiguration();
                Request.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = configuration;

                var result = await _articleService.DeleteArticle(postId, authorId);

                bool status = false;

                if (result)
                {
                    status = true;
                    return Request.CreateResponse(HttpStatusCode.OK, status);
                }
                return Request.CreateResponse(HttpStatusCode.OK, status);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unable to Delete Article" + postId);

            }

        }

        [HttpPost]
        [Route("DeleteArticleByAuthorId")]
        public async Task<HttpResponseMessage> DeleteArticleByAuthorId(int authorId)
        {
            try
            {
                Request = new HttpRequestMessage();
                configuration = new HttpConfiguration();
                Request.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = configuration;

                var result = await _articleService.DeleteArticleByAuthorId(authorId);
            
                bool status = false;

                if (result)
                {
                    status = true;
                    return Request.CreateResponse(HttpStatusCode.OK, status);
                }
                return Request.CreateResponse(HttpStatusCode.OK, status);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unable to Delete Article By AuthorId" + authorId);

            }

        }


    }

}