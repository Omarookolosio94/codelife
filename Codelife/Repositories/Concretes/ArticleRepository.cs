using Codelife.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Codelife.Data;
using System.Threading.Tasks;
using Codelife.Utility;

namespace Codelife.Repositories.Concretes
{
    public class ArticleRepository : BaseRepository<Post>, IArticleRepository
    {
        private static string ModuleName = "ArticleRepository";

        public ArticleRepository(CodelifeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Get all Authors
        public async Task<List<Post>> GetAllArticle()
        {
            try
            {
                return Query().Where(o => o.publicationStatus == true).OrderByDescending(o => o.postId).ToList();
            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "ArticleRepository", "Error Getting all Article" + ex + "/n");
                throw;
            }
        }

        public async Task<Post> GetArticleById(int postId)
        {
            try
            {
                return Query().Where(o => o.postId == postId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "ArticleRepository", "Error Getting Post" + postId + " " + ex + "/n");
                throw;
            }
        }

        public async Task<List<Post>> GetArticleByAuthorId(int authorId)
        {
            try
            {
                return Query().Where(o => o.authorId == authorId).OrderByDescending(o => o.postId).ToList();

            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "ArticleRepository", "Error Getting Post by Author" + authorId + " " + ex + "/n");
                throw;
            }
        }


        public async Task<bool> AddArticle(Post article)
        {
            try
            {
                await InsertAsync(article);
                await Commit();
                return true;
            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "ArticleRepository", "Error Adding Post" + ex + "/n");
                throw;
            }
        }

        public async Task<bool> DeleteArticle(int postId, int authorId)
        {
            try
            {
                var post = Query().Where(o => o.postId == postId && o.authorId == authorId);
                DeleteRange(post);
                await Commit();
                return true;

            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "ArticleRepository", "Error Deleting Post" + postId + "by author " + authorId + " " + ex + "/n");
                throw;
            }
        }

        public async Task<bool> DeleteArticleByAuthorId(int authorId)
        {
            try
            {
                var post = Query().Where(o => o.authorId == authorId).ToList();
                DeleteRange(post);
                await Commit();
                return true;
            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "ArticleRepository", "Error Deleting Post By AuthorId" + authorId + ex + "/n");
                throw;
            }
        }


        public async Task<bool> UpdateArticle(Post article)
        {
            try
            {
                Update(article);
                await Commit();
                return true;
            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "ArticleRepository", "Error Updating Post" + ex + "/n");
                throw;
            }
        }




    }
}