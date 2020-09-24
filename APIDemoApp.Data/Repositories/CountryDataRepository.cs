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
    public class CountryDataRepository : ICountryDataRepository
    {
        private readonly LibraryDBContext _context;
        public CountryDataRepository(LibraryDBContext context)
        {
            _context = context;
        }

        public async Task<Countries> ReadAsync(string countryName)
        {
            return await _context.Countries.Where(x => x.Name.ToLowerInvariant() == countryName.ToLowerInvariant()).FirstOrDefaultAsync();
        }
    }
}
