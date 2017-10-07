using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface ICountryRepository
    {
        Country AddCountry(Country country);
        Country RemoveCountry(int id);
        Country UpdateCountry(Country updatedCountry);
        List<Country> GetCountries();
    }
}
