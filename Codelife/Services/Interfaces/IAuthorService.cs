using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Codelife.Data;

namespace Codelife.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<List<Author>> GetAuthors();
        Task<Author> GetAuthorById(int authorId);
        Task<bool> AddAuthor(Author author);
        Task<bool> UpdateAuthor(Author author);
        Task<bool> DeleteAuthor(int authorId);
    }
}