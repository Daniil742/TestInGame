using TestInGame.Data.Entities;

namespace TestInGame.Data.Interfaces
{
    public interface IBookRepository : IDisposable
    {
        Task<List<Book>> GetBooksAsync();
        Task<Book> GetBookAsync(int id);
        Task InsertBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
        Task SaveAsync();
    }
}
