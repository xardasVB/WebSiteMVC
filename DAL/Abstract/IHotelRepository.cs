using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IHotelRepository
    {
        Hotel AddHotel(Hotel hotel);
        Hotel RemoveHotel(int id);
        Hotel UpdateHotel(Hotel updatedHotel);
        List<Hotel> GetHotels();
        int TotalHotels();
    }
}
