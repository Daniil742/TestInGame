using TestInGame.Data.Entities;

namespace TestInGame.Data.Interfaces
{
    public interface IAuthorRepository : IDisposable
    {
        Task<List<Author>> GetAuthorsAsync();
        Task<Author> GetAuthorAsync(int id);
        Task InsertAuthorAsync(Author author);
        Task UpdateAuthorAsync(Author author);
        Task DeleteAuthorAsync(int id);
        Task SaveAsync();
    }
}
