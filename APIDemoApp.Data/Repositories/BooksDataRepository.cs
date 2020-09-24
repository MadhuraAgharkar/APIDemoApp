using APIDemoApp.Data.Interfaces;
using APIDemoApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace APIDemoApp.Data
{
    public class BooksDataRepository : IBooksDataRepository
    {
        private readonly LibraryDBContext _context;
        public BooksDataRepository(LibraryDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// create a book record
        /// </summary>
        /// <param name="books"></param>
        /// <returns></returns>
        public async Task<int> CreateAsync(Books books)
        {
            _context.Books.Add(books);
            await _context.SaveChangesAsync();
            return books.Id;
        }

        /// <summary>
        /// Read book record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Books> ReadAsync(int id)
        {
            return await _context.Books.Where(x => x.Id == id)
                                 .Include(a => a.Author)
                                 .Include(a => a.Country)
                                 .Include(a => a.Genre)
                                 .Include(a => a.Language)
                                 .FirstOrDefaultAsync();
        }

        public async Task<Books> ReadByNameAsync(string bookName)
        {
            return await _context.Books.Where(x => x.Name.ToLowerInvariant() == bookName.ToLowerInvariant())
                                 .FirstOrDefaultAsync();
        }

        /// <summary>
        /// delete book record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// update book edition
        /// </summary>
        /// <param name="bookid"></param>
        /// <param name="currentEdition"></param>
        /// <returns></returns>
        public async Task UpdateAsync(int bookId, int currentEdition)
        {
            var book = await _context.Books.Where(x => x.Id == bookId).FirstOrDefaultAsync();
            book.CurrentEdition = currentEdition;
            await _context.SaveChangesAsync();
        }
    }
}
