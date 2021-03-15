using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Codelife.Data;
using Codelife.Models;

namespace Codelife.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<List<Author>> GetAuthors();
        Task<Author> GetAuthorById(int authorId);
        Task<Author> GetAuthorByEmail(string email);
        Task<bool> IsEmailAvailable(string email);
        Task<bool> AddAuthor(Author author);
        Task<bool> UpdateAuthor(int authorId, Author author);
        Task<bool> DeleteAuthor(int authorId);
    }
}