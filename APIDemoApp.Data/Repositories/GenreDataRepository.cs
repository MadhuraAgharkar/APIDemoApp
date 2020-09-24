using APIDemoApp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using APIDemoApp.Data.Models;

namespace APIDemoApp.Data.Repositories
{
    public class GenreDataRepository : IGenreDataRepository
    {
        private readonly LibraryDBContext _context;
        public GenreDataRepository(LibraryDBContext context)
        {
            _context = context;
        }
        public async Task<Genres> ReadAsync(string genreName)
        {
            return await _context.Genres.Where(x => x.Name.ToLowerInvariant() == genreName.ToLowerInvariant()).FirstOrDefaultAsync();
        }
    }
}
