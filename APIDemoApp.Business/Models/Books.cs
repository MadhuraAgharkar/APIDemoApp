using System;
using System.Collections.Generic;
using System.Text;

namespace APIDemoApp.Business.Models
{
    public class Books
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Country { get; set; }
        public string Genre { get; set; }
        public string Language { get; set; }
        public string Isbn { get; set; }
        public int CurrentEdition { get; set; }
    }
}
