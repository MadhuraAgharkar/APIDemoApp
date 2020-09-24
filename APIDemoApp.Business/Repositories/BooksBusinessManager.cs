using APIDemoApp.Business.Interfaces;
using APIDemoApp.Business.Models;
using APIDemoApp.Data.Interfaces;
using System;
using System.Threading.Tasks;
using DM = APIDemoApp.Data.Models;

namespace APIDemoApp.Business
{
    public class BooksBusinessManager : IBooksBusinessContract
    {
        private readonly IBooksDataRepository _booksDataRepository;
        private readonly IAuthorDataRepository _authorDataRepository;
        private readonly ICountryDataRepository _countryDataRepository;
        private readonly ILanguageDataRepository _languageDataRepository;
        private readonly IGenreDataRepository _genreDataRepository;
        public BooksBusinessManager(IBooksDataRepository booksDataRepository, IAuthorDataRepository authorDataRepository,
                                    ICountryDataRepository countryDataRepository, ILanguageDataRepository languageDataRepository,
                                    IGenreDataRepository genreDataRepository)
        {
            _booksDataRepository = booksDataRepository;
            _authorDataRepository = authorDataRepository;
            _countryDataRepository = countryDataRepository;
            _languageDataRepository = languageDataRepository;
            _genreDataRepository = genreDataRepository;
        }

        /// <summary>
        /// Create a book
        /// </summary>
        /// <param name="books"></param>
        /// <returns>bool</returns>
        public async Task<int> CreateAsync(Books books)
        {
            int bookCreateResponse = 0;
            try
            {
                DM.Books booksDM = new DM.Books() {
                    Name = books.Name,
                    CurrentEdition = books.CurrentEdition,
                    Isbn = books.Isbn 
                };

                var authorResponse = await _authorDataRepository.ReadAsync(books.Author);
                var countryResponse = await _countryDataRepository.ReadAsync(books.Country);
                var languageResponse = await _languageDataRepository.ReadAsync(books.Language);
                var genreResponse = await _genreDataRepository.ReadAsync(books.Genre);
                if (authorResponse != null)
                {
                    booksDM.AuthorId = authorResponse.Id;
                }
                else
                {
                    booksDM.Author = new DM.Authors()
                    {
                        Name = books.Author
                    };
                }
                if (countryResponse != null)
                {
                    booksDM.CountryId = countryResponse.Id;
                }
                else
                {
                    booksDM.Country = new DM.Countries()
                    {
                        Name = books.Country
                    };
                }
                if (languageResponse != null)
                {
                    booksDM.LanguageId = languageResponse.Id;
                }
                else
                {
                    booksDM.Language = new DM.Languages()
                    {
                        Name = books.Language
                    };
                }
                if (genreResponse != null)
                {
                    booksDM.GenreId = genreResponse.Id;
                }
                else
                {
                    booksDM.Genre = new DM.Genres()
                    {
                        Name = books.Genre
                    };
                }
                var bookReadResponse = await _booksDataRepository.ReadByNameAsync(books.Name);
                if(bookReadResponse == null)
                {
                    bookCreateResponse = await _booksDataRepository.CreateAsync(booksDM);
                }                
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return bookCreateResponse;
        }

        /// <summary>
        /// Delete a book 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            bool result = false;
            try
            {
                var readResponse = await ReadAsync(id);
                if (readResponse != null)
                {
                    await _booksDataRepository.DeleteAsync(id);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// Read a book record
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Books</returns>
        public async Task<Books> ReadAsync(int id)
        {
            Books books = null;
            try
            {
                var response = await _booksDataRepository.ReadAsync(id);
                if (response != null)
                {
                    books = new Books
                    {
                        Id = response.Id,
                        Name = response.Name,
                        Author = response.Author.Name,
                        Country = response.Country.Name,
                        Genre = response.Genre.Name,
                        CurrentEdition = response.CurrentEdition,
                        Language = response.Language.Name,
                        Isbn = response.Isbn
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return books;
        }

        /// <summary>
        /// update book edition
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="bookEdition"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(int bookId, int bookEdition)
        {
            bool result = false;
            try
            {
                var readResponse = await ReadAsync(bookId);
                if (readResponse != null)
                {
                    await _booksDataRepository.UpdateAsync(bookId, bookEdition);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
