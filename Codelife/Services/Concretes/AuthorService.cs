using Codelife.Data;
using Codelife.Repositories.Interfaces;
using Codelife.Services.Interfaces;
using Codelife.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Codelife.Services.Concretes
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private static string ModuleName = "AuthorService";

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<List<Author>> GetAuthors()
        {
            try
            {
                var authors = await _authorRepository.GetAllAuthors();
                return authors;

            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "GetAuthors", "Error Getting Authors" + ex + "\n");
                throw;
            }
        }

        public async Task<Author> GetAuthorById(int authorId)
        {
            try
            {
                var author = await _authorRepository.GetAuthorById(authorId);
                return author;
                
            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "GetAuthors", "Error Getting Authors" + ex + "\n");
                throw;
            }
        }

        public async Task<bool> AddAuthor(Author author)
        {
            try
            {
                var status = await _authorRepository.AddAuthor(author);

                return status;
            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "AddAuthor", "Error Adding Author" + ex + "\n");
                throw;
            }
        }

        public async Task<bool> UpdateAuthor(Author author)
        {
            try
            {
                var status = await _authorRepository.UpdateAuthor(author);
                return status;
            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "UpdateAuthor", "Error Updating Author" + ex + "\n");
                throw;
            }
        }

        public async Task<bool> DeleteAuthor(int authorId)
        {
            try
            {
                var status = await _authorRepository.DeleteAuthor(authorId);
                return status;
            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "DeleteAuthor", "Error Deleting Author" + authorId + " " + ex + "\n");
                throw;
            }
        }


    }
}