using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface ICityProvider
    {
        CityItemViewModel GetCitiesByPage(int page, int pages);
        List<CityViewModel> GetCities();
        CityViewModel GetCityById(int id);
        CityViewModel DeleteCity(int id);
        CityCreateViewModel CreateCity(CityCreateViewModel city);
        CityEditViewModel EditCity(CityEditViewModel city, int id);
    }
}
