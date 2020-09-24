using System;
using System.Collections.Generic;

namespace APIDemoApp.Data.Models
{
    public partial class Genres
    {
        public Genres()
        {
            Books = new HashSet<Books>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Books> Books { get; set; }
    }
}
