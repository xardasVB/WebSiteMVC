using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IHotelProvider
    {
        List<HotelViewModel> GetHotelsByPage(int page);
        List<HotelViewModel> GetHotels();
        HotelViewModel GetHotelById(int id);
        HotelViewModel DeleteHotel(int id);
        HotelCreateViewModel CreateHotel(HotelCreateViewModel hotel);
        HotelEditViewModel EditHotel(HotelEditViewModel hotel, int id);
    }
}
