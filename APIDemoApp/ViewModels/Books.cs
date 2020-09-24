using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDemoApp.ViewModels
{
    public class Books
    {
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Country { get; set; }
        public string Genre { get; set; }
        public string Language { get; set; }
        public string Isbn { get; set; }
        public int CurrentEdition { get; set; }
    }
}
