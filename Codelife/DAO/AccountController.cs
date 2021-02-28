using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using Codelife.Data;
using Codelife.Services.Interfaces;
using Codelife.Services.Concretes;
using Codelife.Repositories.Concretes;

namespace Codelife.DAO
{
    public class AccountController: ApiController
    {
        private readonly CodelifeDbContext dbContext = new CodelifeDbContext();
        private readonly IAuthorService _authorService;
        
        //HttpRequestMessage Request;
        //HttpConfiguration configuration;

        string ModuleName = "ApiController";

        public AccountController()
        {
            _authorService = new AuthorService(new AuthorRepository(dbContext));
        }
    }

}