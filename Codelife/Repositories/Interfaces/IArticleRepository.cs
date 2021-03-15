using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Codelife.Data;
using System.Threading.Tasks;

namespace Codelife.Repositories.Interfaces
{
    public interface IArticleRepository
    {
        Task<List<Post>> GetAllArticle();
        Task<Post> GetArticleById(int postId);
        Task<List<Post>> GetArticleByAuthorId(int authorId);
        Task<bool> AddArticle(Post article);
        Task<bool> UpdateArticle(Post article);
        Task<bool> DeleteArticle(int postId , int authorId);
        Task<bool> DeleteArticleByAuthorId(int authorId);
    }
}