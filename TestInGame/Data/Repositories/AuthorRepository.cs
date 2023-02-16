using Microsoft.EntityFrameworkCore;
using TestInGame.Data.Entities;
using TestInGame.Data.Interfaces;

namespace TestInGame.Data.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private bool _disposed = false;
        private readonly InGameDbContext _dbContext;

        public AuthorRepository(InGameDbContext dbContext) =>
            _dbContext = dbContext;

        public Task<List<Author>> GetAuthorsAsync() =>
            _dbContext.Authors.ToListAsync();

        public async Task<Author> GetAuthorAsync(int id) =>
            await _dbContext.Authors.FindAsync(new object[] { id });

        public async Task InsertAuthorAsync(Author author)
        {
            await _dbContext.Authors.AddAsync(author);
            await _dbContext.Books.AddRangeAsync(author.Books);
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            var authorFromDb = await _dbContext.Authors.FindAsync(new object[] { author.Id });

            if (authorFromDb is null) return;

            authorFromDb.Name = author.Name;
            authorFromDb.BirthDay = author.BirthDay;
            authorFromDb.Books = author.Books;
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var authorFromDb = await _dbContext.Authors.FindAsync(new object[] { id });

            if (authorFromDb is null) return;

            _dbContext.Authors.Remove(authorFromDb);
        }

        public async Task SaveAsync() =>
            await _dbContext.SaveChangesAsync();

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            _disposed= true;
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
