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
        private readonly IEFContext _ctx;

        public CountryRepository(IEFContext ctx)
        {
            _ctx = ctx;
        }

        public Country AddCountry(Country country)
        {
            _ctx.Set<Country>().Add(country);
            _ctx.SaveChanges();
            return country;
        }

        public List<Country> GetCountries()
        {
            return _ctx.Set<Country>()
                .Include(c => c.Cities)
                .ToList();
        }

        public void Dispose()
        {
            if (_ctx != null)
                _ctx.Dispose();
        }

        public Country RemoveCountry(int id)
        {
            Country country = _ctx.Set<Country>().FirstOrDefault(h => h.Id == id);
            _ctx.Set<Country>().Remove(country);
            _ctx.SaveChanges();
            return country;
        }

        public Country UpdateCountry(Country updatedCountry)
        {
            Country oldCountry = _ctx.Set<Country>().FirstOrDefault(c => c.Id == updatedCountry.Id);
            oldCountry.Priority = updatedCountry.Priority;
            oldCountry.Name = updatedCountry.Name;
            _ctx.SaveChanges();
            return updatedCountry;
        }

        public int TotalCountries()
        {
            return this.GetCountries().Count;
        }
    }
}
