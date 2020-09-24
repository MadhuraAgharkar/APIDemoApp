using APIDemoApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIDemoApp.Data.Interfaces
{
    public interface ICountryDataRepository
    {
        Task<Countries> ReadAsync(string countryName);
    }
}
