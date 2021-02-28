using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Codelife.Data;
using System.Threading.Tasks;

namespace Codelife.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAuthors();
        Task<Author> GetAuthorById(int authorId);
        Task<bool> AddAuthor(Author author);
        Task<bool> UpdateAuthor(Author author);
        Task<bool> DeleteAuthor(int authorId);
    }
}