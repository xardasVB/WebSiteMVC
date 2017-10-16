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
        ICountryRepository repository;

        public CountryProvider(ICountryRepository repository)
        {
            this.repository = repository;
        }

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

        public CountryItemViewModel GetCountriesByPage(int page, int pages, SearchCountryViewModel search)
        {
            List<Country> query = repository.GetCountries();
            CountryItemViewModel model = new CountryItemViewModel();

            if (!string.IsNullOrEmpty(search.Name))
            {
                query = query.Where(c => c.Name.Contains(search.Name)).ToList();
            }

            if (!string.IsNullOrEmpty(search.Priority))
            {
                int priority;
                int.TryParse(search.Priority, out priority);
                query = query.Where(c => c.Priority == priority).ToList();
            }
            model.Countries = query
                .OrderByDescending(c => c.Priority)
                .Skip((page - 1) * pages)
                .Take(pages)
                .Select(c => new CountryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Priority = c.Priority,
                    DateCreate = c.DateCreate
                }).ToList();
            model.TotalPages = (int)Math.Ceiling((double)query.Count / pages);
            model.CurrentPage = page;
            model.Search = search;
            model.Pages = pages;

            return model;
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
