using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface ICountryProvider
    {
        CountryItemViewModel GetCountriesByPage(int page, int pages);
        List<CountryViewModel> GetCountries();
        CountryViewModel GetCountryById(int id);
        CountryViewModel DeleteCountry(int id);
        CountryCreateViewModel CreateCountry(CountryCreateViewModel country);
        CountryEditViewModel EditCountry(CountryEditViewModel country, int id);
    }
}
