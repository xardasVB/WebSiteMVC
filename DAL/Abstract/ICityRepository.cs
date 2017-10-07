using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface ICityRepository
    {
        City AddCity(City city);
        City RemoveCity(int id);
        City UpdateCity(City updatedCity);
        List<City> GetCities();
    }
}
