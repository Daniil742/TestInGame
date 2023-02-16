namespace TestInGame.Data.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublicationYear { get; set; }
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
        public virtual List<Author> Authors { get; set; } = new();
        public string EditorialOffice { get; set; }
    }
}
