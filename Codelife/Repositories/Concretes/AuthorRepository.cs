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
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        private static string ModuleName = "AuthorRepository";

        public AuthorRepository(CodelifeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Get all Authors
        public async Task<List<Author>> GetAllAuthors()
        {
            try
            {
                return Query().OrderByDescending(o => o.authorId).ToList();
            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "AuthorRepository", "Error Getting all Authors" + ex + "/n");
                throw;
            }
        }

        public async Task<Author> GetAuthorById(int authorId)
        {
            try
            {
                return Query().Where(o => o.authorId == authorId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "AuthorRepository", "Error Getting author" + authorId + " " + ex + "/n");
                throw;
            }
        }


        public async Task<bool> AddAuthor(Author author)
        {
            try
            {
                await InsertAsync(author);
                await Commit();
                return true;
            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "AuthorRepository", "Error Adding User" + ex + "/n");
                throw;
            }
        }

        public async Task<bool> UpdateAuthor(Author author)
        {
            try
            {
                Update(author);
                await Commit();
                return true;
            }
            catch (Exception ex)
            {
                new Logger().LogError(ModuleName, "AuthorRepository", "Error Updating Author" + ex + "/n");
                throw;
            }
        }

        public async Task<bool> DeleteAuthor(int authorId){
            try
            {
                var author = Query().Where(o => o.authorId == authorId);
                DeleteRange(author);
                await Commit();
                return true;

            } catch(Exception ex)
            {
                new Logger().LogError(ModuleName, "AuthorRepository", "Error Deleting Author" + authorId + " " + ex + "/n");
                throw;
            }
        }


    }
}