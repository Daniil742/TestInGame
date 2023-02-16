﻿namespace TestInGame.Data.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public virtual List<Book> Books { get; set; } = new();
    }
}
