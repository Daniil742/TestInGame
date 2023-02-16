using Microsoft.EntityFrameworkCore;
using TestInGame.Data.Entities;
using TestInGame.Data.Interfaces;

namespace TestInGame.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private bool _disposed = false;
        private readonly InGameDbContext _dbContext;

        public BookRepository(InGameDbContext dbContext) =>
            _dbContext = dbContext;

        public Task<List<Book>> GetBooksAsync() =>
            _dbContext.Books.ToListAsync();

        public async Task<Book> GetBookAsync(int id) =>
            await _dbContext.Books.FindAsync(new object[] { id });

        public async Task InsertBookAsync(Book book)
        {
            await _dbContext.Books.AddAsync(book);
            await _dbContext.Authors.AddRangeAsync(book.Authors);
        }

        public async Task UpdateBookAsync(Book book)
        {
            var bookFromDb = await _dbContext.Books.FindAsync(new object[] { book.Id });

            if (bookFromDb is null) return;

            bookFromDb.Title = book.Title;
            bookFromDb.PublicationYear = book.PublicationYear;
            bookFromDb.GenreId = book.GenreId;
            bookFromDb.Authors = book.Authors;
            bookFromDb.EditorialOffice = book.EditorialOffice;
        }

        public async Task DeleteBookAsync(int id)
        {
            var bookFromDb = await _dbContext.Books.FindAsync(new object[] { id });

            if (bookFromDb is null) return;

            _dbContext.Books.Remove(bookFromDb);
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

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
