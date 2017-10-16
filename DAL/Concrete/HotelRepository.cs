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
        private readonly IEFContext _ctx;

        public HotelRepository(IEFContext ctx)
        {
            _ctx = ctx;
        }

        public Hotel AddHotel(Hotel hotel)
        {
            _ctx.Set<Hotel>().Add(hotel);
            _ctx.SaveChanges();
            return hotel;
        }

        public List<Hotel> GetHotels()
        {
            return _ctx.Set<Hotel>()
                .Include(c => c.City)
                .ToList();
        }

        public void Dispose()
        {
            if (_ctx != null)
                _ctx.Dispose();
        }

        public Hotel RemoveHotel(int id)
        {
            Hotel hotel = _ctx.Set<Hotel>().Include(h => h.City).FirstOrDefault(h => h.Id == id);
            _ctx.Set<Hotel>().Remove(hotel);
            _ctx.SaveChanges();
            return hotel;
        }

        public Hotel UpdateHotel(Hotel updatedHotel)
        {
            Hotel oldHotel = _ctx.Set<Hotel>().FirstOrDefault(c => c.Id == updatedHotel.Id);
            oldHotel.Priority = updatedHotel.Priority;
            oldHotel.Name = updatedHotel.Name;
            oldHotel.CityId = updatedHotel.CityId;
            _ctx.SaveChanges();
            return updatedHotel;
        }

        public int TotalHotels()
        {
            return this.GetHotels().Count;
        }
    }
}
