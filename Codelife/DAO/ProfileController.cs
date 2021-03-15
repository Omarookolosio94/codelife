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
    public class ProfileController: ApiController
    {
        private readonly CodelifeDbContext dbContext = new CodelifeDbContext();
        private readonly IAuthorService _authorService;
        
        HttpRequestMessage Request;
        HttpConfiguration configuration;

        string ModuleName = "ApiController";

        public ProfileController()
        {
            _authorService = new AuthorService(new AuthorRepository(dbContext) , new ArticleRepository(dbContext));
        }

        [HttpPost]
        [Route("GetAuthorProfile")]
        public async Task<HttpResponseMessage> GetAuthorProfile(int authorId)
        {
            try
            {
                Request = new HttpRequestMessage();
                configuration = new HttpConfiguration();
                Request.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = configuration;

                var result = await _authorService.GetAuthorById(authorId);
                return Request.CreateResponse<Author>(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unable to Get Author Details!");

            }

        }

        [HttpPost]
        [Route("GetAuthors")]
        public async Task<HttpResponseMessage> GetAuthors()
        {
            try
            {
                Request = new HttpRequestMessage();
                configuration = new HttpConfiguration();
                Request.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = configuration;

                var result = await _authorService.GetAuthors();
                return Request.CreateResponse<List<Author>>(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unable to Get List of Authors!");

            }

        }

        [HttpPost]
        [Route("UpdateAuthor")]
        public async Task<HttpResponseMessage> UpdateAuthor(int authorId,Author author)
        {
            try
            {
                Request = new HttpRequestMessage();
                configuration = new HttpConfiguration();
                Request.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = configuration;

                var result = await _authorService.UpdateAuthor(authorId , author);

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
                new Logger().LogError(ModuleName, "UpdateAuthor", "Error UpdateAuthor - Server Error" + "Status Code: " + ex+ "\n");
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unable to Update Author!");
            }
        }


        [HttpPost]
        [Route("DeleteAuthor")]
        public async Task<HttpResponseMessage> DeleteAuthor(int authorId)
        {
            try
            {
                Request = new HttpRequestMessage();
                configuration = new HttpConfiguration();
                Request.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = configuration;

                var result = await _authorService.DeleteAuthor(authorId);

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
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unable to Delete Author" + authorId);

            }

        }


    }

}