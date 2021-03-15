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
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private static string ModuleName = "ArticleService";

        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<List<Post>> GetAllArticle()
        {
            try
            {
                var articles = await _articleRepository.GetAllArticle();
                return articles;

            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "GetArticles", "Error Getting Articles" + ex + "\n");
                throw;
            }
        }

        public async Task<Post> GetArticleById(int postId)
        {
            try
            {
                var article = await _articleRepository.GetArticleById(postId);
                return article;
                
            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "GetArticleById", "Error Getting Article" + ex + "\n");
                throw;
            }
        }

        public async Task<List<Post>> GetArticleByAuthorId(int authorId)
        {
            try
            {
                var articleByAuthor = await _articleRepository.GetArticleByAuthorId(authorId);
                return articleByAuthor;

            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "GetArticleByAuthorId", "Error Getting ArticleByAuthor" + authorId + " " + ex + "\n");
                throw;
            }
        }

        public async Task<bool> AddArticle(Post article)
        {
            try
            {
                var status = await _articleRepository.AddArticle(article);

                return status;
            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "AddArticle", "Error Adding Article" + ex + "\n");
                throw;
            }
        }

    

        public async Task<bool> DeleteArticle(int postId , int authorId)
        {
            try
            {
                var status = await _articleRepository.DeleteArticle(postId, authorId);
                return status;
            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "DeleteArticle", "Error Deleting Article" + postId + " " + ex + "\n");
                throw;
            }
        }

        public async Task<bool> DeleteArticleByAuthorId(int authorId)
        {
            try
            {
                var status = await _articleRepository.DeleteArticleByAuthorId(authorId);
                return status;
            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "DeleteArticle", "Error Deleting Article" + authorId + " " + ex + "\n");
                throw;
            }
        }



        public async Task<bool> UpdateArticle(Post article, int authorId)
        {
            try
            {
                var articleCheck = await _articleRepository.GetArticleById(article.postId);
                var status = false;

                if(articleCheck.authorId == authorId)
                {
                    articleCheck.title = article.title;
                    articleCheck.tag = article.tag;
                    articleCheck.text = article.text;
                    articleCheck.updateDate = article.updateDate;
                    articleCheck.publicationStatus = article.publicationStatus;

                    status = await _articleRepository.UpdateArticle(articleCheck);
                }  

                return status;
            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "UpdateArticle", "Error Updating Article" + ex + "\n");
                throw;
            }
        }


    }
}