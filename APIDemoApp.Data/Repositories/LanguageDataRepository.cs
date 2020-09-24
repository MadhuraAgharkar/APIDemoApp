using APIDemoApp.Data.Interfaces;
using APIDemoApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace APIDemoApp.Data.Repositories
{
    public class LanguageDataRepository : ILanguageDataRepository
    {
        private readonly LibraryDBContext _context;
        public LanguageDataRepository(LibraryDBContext context)
        {
            _context = context;
        }

        public async Task<Languages> ReadAsync(string languageName)
        {
            return await _context.Languages.Where(x => x.Name.ToLowerInvariant() == languageName.ToLowerInvariant()).FirstOrDefaultAsync();
        }
    }
}
