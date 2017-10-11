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
    public class CityRepository : ICityRepository
    {
        private readonly IEFContext _ctx;

        public CityRepository(IEFContext ctx)
        {
            _ctx = ctx;
        }

        public City AddCity(City city)
        {
            _ctx.Set<City>().Add(city);
            _ctx.SaveChanges();
            return city;
        }

        public List<City> GetCities()
        {
            return _ctx.Set<City>()
                .Include(c => c.Country)
                .Include(c => c.Hotels)
                .ToList();
        }

        public void Dispose()
        {
            if (_ctx != null)
                _ctx.Dispose();
        }

        public City RemoveCity(int id)
        {
            City city = _ctx.Set<City>().Include(c => c.Country).FirstOrDefault(h => h.Id == id);
            _ctx.Set<City>().Remove(city);
            _ctx.SaveChanges();
            return city;
        }

        public City UpdateCity(City updatedCity)
        {
            City oldCity = _ctx.Set<City>().FirstOrDefault(c => c.Id == updatedCity.Id);
            oldCity.CountryId = updatedCity.CountryId;
            oldCity.Priority = updatedCity.Priority;
            oldCity.Name = updatedCity.Name;
            _ctx.SaveChanges();
            return updatedCity;
        }
    }
}
