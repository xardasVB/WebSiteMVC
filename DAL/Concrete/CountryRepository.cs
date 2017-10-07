using DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entity;

namespace DAL.Concrete
{
    public class CountryRepository : ICountryRepository
    {
        EFContext _ctx = new EFContext();

        public Country AddCountry(Country country)
        {
            _ctx.Countries.Add(country);
            _ctx.SaveChanges();
            return country;
        }

        public List<Country> GetCountries()
        {
            return _ctx.Countries
                .Include(c => c.Cities)
                .ToList();
        }

        public Country RemoveCountry(int id)
        {
            Country country = _ctx.Countries.FirstOrDefault(h => h.Id == id);
            _ctx.Countries.Remove(country);
            _ctx.SaveChanges();
            return country;
        }

        public Country UpdateCountry(Country updatedCountry)
        {
            Country oldCountry = _ctx.Countries.FirstOrDefault(c => c.Id == updatedCountry.Id);
            oldCountry.Priority = updatedCountry.Priority;
            oldCountry.Name = updatedCountry.Name;
            _ctx.SaveChanges();
            return updatedCountry;
        }
    }
}
