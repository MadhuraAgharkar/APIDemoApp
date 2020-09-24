using APIDemoApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIDemoApp.Data.Interfaces
{
    public interface IGenreDataRepository
    {
        Task<Genres> ReadAsync(string genreName);
    }
}
