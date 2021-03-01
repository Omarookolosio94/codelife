﻿using System;
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

namespace Codelife.DAO
{
    [RoutePrefix("api")]
    public class AccountController: ApiController
    {
        private readonly CodelifeDbContext dbContext = new CodelifeDbContext();
        private readonly IAuthorService _authorService;
        
        HttpRequestMessage Request;
        HttpConfiguration configuration;

        string ModuleName = "ApiController";

        public AccountController()
        {
            _authorService = new AuthorService(new AuthorRepository(dbContext));
        }

        [HttpPost]
        [Route("AddAuthor")]
        public async Task<HttpResponseMessage> AddAuthor(Author author)
        {
            try
            {
                Request = new HttpRequestMessage();
                configuration = new HttpConfiguration();
                Request.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = configuration;

                var result = await _authorService.AddAuthor(author);

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
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unable to Create Author!");
                // (500, new ExecptionResponse { Message = ex.Message });
            }
        }
    }

}