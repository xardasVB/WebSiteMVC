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
    public class HotelRepository : IHotelRepository
    {
        EFContext _ctx = new EFContext();

        public Hotel AddHotel(Hotel hotel)
        {
            _ctx.Hotels.Add(hotel);
            _ctx.SaveChanges();
            return hotel;
        }

        public List<Hotel> GetHotels()
        {
            return _ctx.Hotels
                .Include(c => c.City)
                .ToList();
        }

        public Hotel RemoveHotel(int id)
        {
            Hotel hotel = _ctx.Hotels.Include(h => h.City).FirstOrDefault(h => h.Id == id);
            _ctx.Hotels.Remove(hotel);
            _ctx.SaveChanges();
            return hotel;
        }

        public Hotel UpdateHotel(Hotel updatedHotel)
        {
            Hotel oldHotel = _ctx.Hotels.FirstOrDefault(c => c.Id == updatedHotel.Id);
            oldHotel.Priority = updatedHotel.Priority;
            oldHotel.Name = updatedHotel.Name;
            _ctx.SaveChanges();
            return updatedHotel;
        }
    }
}
