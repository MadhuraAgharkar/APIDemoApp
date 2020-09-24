using APIDemoApp.Data.Interfaces;
using APIDemoApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDemoApp.Data.Repositories
{
    public class AuthorDataRepository : IAuthorDataRepository
    {
        private readonly LibraryDBContext _context;
        public AuthorDataRepository(LibraryDBContext context)
        {
            _context = context;
        }
        public async Task<Authors> ReadAsync(string authorName)
        {
            return await _context.Authors.Where(x => x.Name.ToLowerInvariant() == authorName.ToLowerInvariant()).FirstOrDefaultAsync();
        }
    }
}
