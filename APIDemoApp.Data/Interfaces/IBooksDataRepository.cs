using APIDemoApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIDemoApp.Data.Interfaces
{
    public interface IBooksDataRepository
    {
        Task<Books> ReadAsync(int id);
        Task<Books> ReadByNameAsync(string bookName);
        Task<int> CreateAsync(Books books);
        Task DeleteAsync(int id);
        Task UpdateAsync(int bookId,int bookEdition);
    }
}
