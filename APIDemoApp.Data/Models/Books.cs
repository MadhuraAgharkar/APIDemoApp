using System;
using System.Collections.Generic;

namespace APIDemoApp.Data.Models
{
    public partial class Books
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public int CountryId { get; set; }
        public int LanguageId { get; set; }
        public string Isbn { get; set; }
        public int CurrentEdition { get; set; }

        public Authors Author { get; set; }
        public Countries Country { get; set; }
        public Genres Genre { get; set; }
        public Languages Language { get; set; }
    }
}
