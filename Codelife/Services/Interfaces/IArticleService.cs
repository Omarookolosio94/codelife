using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Codelife.Data;
using Codelife.Models;

namespace Codelife.Services.Interfaces
{
    public interface IArticleService
    {
        Task<List<Post>> GetAllArticle();
        Task<Post> GetArticleById(int postId);
        Task<List<Post>> GetArticleByAuthorId(int authorId);
        Task<bool> AddArticle(Post article);
        Task<bool> UpdateArticle(Post article , int authorId);
        Task<bool> DeleteArticle(int postId, int authorId);
        Task<bool> DeleteArticleByAuthorId(int authorId);
    }
}