using BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using DAL.Abstract;
using DAL.Concrete;
using DAL.Entity;

namespace BLL.Concrete
{
    public class CountryProvider : ICountryProvider
    {
        ICountryRepository repository = new CountryRepository();

        public CountryCreateViewModel CreateCountry(CountryCreateViewModel country)
        {
            repository.AddCountry(new Country
            {
                Name = country.Name,
                Priority = country.Priority,
                DateCreate = DateTime.Now
            });
            return country;
        }

        public CountryViewModel DeleteCountry(int id)
        {
            Country country = repository.RemoveCountry(id);
            return new CountryViewModel
            {
                Id = country.Id,
                Name = country.Name,
                Priority = country.Priority,
                DateCreate = country.DateCreate
            };
        }

        public CountryEditViewModel EditCountry(CountryEditViewModel country, int id)
        {
            repository.UpdateCountry(new Country
            {
                Id = id,
                Name = country.Name,
                Priority = country.Priority
            });
            return country;
        }

        public List<CountryViewModel> GetCountries()
        {
            return repository
                .GetCountries()
                .Select(c => new CountryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Priority = c.Priority,
                    DateCreate = c.DateCreate
                })
                .ToList();
        }

        public CountryViewModel GetCountryById(int id)
        {
            Country country = repository.GetCountries().FirstOrDefault(c => c.Id == id);
            return new CountryViewModel
            {
                Id = country.Id,
                Name = country.Name,
                Priority = country.Priority,
                DateCreate = country.DateCreate
            };
        }
    }
}
