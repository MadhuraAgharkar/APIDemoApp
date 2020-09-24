using APIDemoApp.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIDemoApp.Business.Interfaces
{
    public interface IBooksBusinessContract
    {
        Task<Books> ReadAsync(int id);
        Task<int> CreateAsync(Books books);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(int bookId,int bookEdition);
    }
}
