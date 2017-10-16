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
    public class CityProvider : ICityProvider
    {
        ICityRepository repository;

        public CityProvider(ICityRepository repository)
        {
            this.repository = repository;
        }

        public CityCreateViewModel CreateCity(CityCreateViewModel city)
        {
            repository.AddCity(new City
            {
                Name = city.Name,
                Priority = city.Priority,
                CountryId = city.CountryId,
                DateCreate = DateTime.Now
            });
            return city;
        }

        public CityViewModel DeleteCity(int id)
        {
            City city = repository.RemoveCity(id);
            return new CityViewModel
            {
                Id = city.Id,
                Name = city.Name,
                Priority = city.Priority,
                DateCreate = city.DateCreate
            };
        }

        public CityEditViewModel EditCity(CityEditViewModel city, int id)
        {
            repository.UpdateCity(new City
            {
                Id = id,
                Name = city.Name,
                Priority = city.Priority,
                CountryId = city.CountryId
            });
            return city;
        }

        public CityItemViewModel GetCitiesByPage(int page, int pages)
        {
            int pageNo = page - 1;
            CityItemViewModel model = new CityItemViewModel();
            model.Pages = pages;
            model.Cities = repository
                .GetCities()
                .OrderByDescending(c => c.Priority)
                .Skip(pageNo * pages)
                .Take(pages)
                .Select(c => new CityViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Priority = c.Priority,
                    DateCreate = c.DateCreate,
                    Country = c.Country.Name
                }).ToList();
            model.TotalPages = (int)Math.Ceiling((double)repository.TotalCities() / pages);
            model.CurrentPage = page;

            return model;
        }

        public List<CityViewModel> GetCities()
        {
            return repository
                .GetCities()
                .Select(c => new CityViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Priority = c.Priority,
                    DateCreate = c.DateCreate,
                    Country = c.Country.Name,
                    CountryId = c.CountryId
                })
                .ToList();
        }

        public CityViewModel GetCityById(int id)
        {
            City city = repository.GetCities().FirstOrDefault(c => c.Id == id);
            return new CityViewModel
            {
                Id = city.Id,
                Name = city.Name,
                Priority = city.Priority,
                DateCreate = city.DateCreate,
                Country = city.Country.Name
            };
        }
    }
}
