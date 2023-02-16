using TestInGame.Data.Entities;

namespace TestInGame.Data
{
    public static class DbInitializer
    {
        public static void Initialize(InGameDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Genres.AddRange(new List<Genre>
            {
                new Genre
                {
                    Name = "Ужасы"
                },
                new Genre
                {
                    Name = "Драма"
                },
                new Genre
                {
                    Name = "Детектив"
                }
            });

            context.SaveChanges();
        }
    }
}
