using Codelife.Data;
using Codelife.Models;
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
        private readonly IArticleRepository _articleRepository;
        private static string ModuleName = "AuthorService";

        public AuthorService(IAuthorRepository authorRepository , IArticleRepository articleRepository)
        {
            _authorRepository = authorRepository;
            _articleRepository = articleRepository;
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
                new Logger().LogError(ModuleName, "GetAuthorsById", "Error Getting Authors" + ex + "\n");
                throw;
            }
        }

        public async Task<Author> GetAuthorByEmail(string email)
        {
            try
            {
                var author = await _authorRepository.GetAuthorByEmail(email);
                return author;

            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "GetAuthorsByEmail", "Error Getting Authors" + email + " " + ex + "\n");
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

      

        public async Task<bool> IsEmailAvailable(string email)
        {
            try
            {
                bool isEmailAvailable = await _authorRepository.IsEmailAvailable(email);
                return isEmailAvailable;

            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "isEmailAvailable", "Error Getting isEmailAvailable" + ex + "\n");
                throw;
            }
        }

        public async Task<bool> UpdateAuthor(int authorId , Author updateAuthor)
        {
            try
            {
                var author = await _authorRepository.GetAuthorById(authorId);
                author.username = updateAuthor.username;
                author.profile = updateAuthor.profile;

                var status = await _authorRepository.UpdateAuthor(author);
                return status;
            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "UpdateAuthor", "Error Updating Author" + ex + "\n");
                throw;
            }
        }


    }
}