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
    public class HotelProvider : IHotelProvider
    {
        IHotelRepository repository;

        public HotelProvider(IHotelRepository repository)
        {
            this.repository = repository;
        }

        public HotelCreateViewModel CreateHotel(HotelCreateViewModel hotel)
        {
            repository.AddHotel(new Hotel
            {
                Name = hotel.Name,
                Priority = hotel.Priority,
                CityId = hotel.CityId,
                DateCreate = DateTime.Now
            });
            return hotel;
        }

        public HotelViewModel DeleteHotel(int id)
        {
            Hotel hotel = repository.RemoveHotel(id);
            return new HotelViewModel
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Priority = hotel.Priority,
                DateCreate = hotel.DateCreate
            };
        }

        public HotelEditViewModel EditHotel(HotelEditViewModel hotel, int id)
        {
            repository.UpdateHotel(new Hotel
            {
                Id = id,
                Name = hotel.Name,
                Priority = hotel.Priority,
                CityId = hotel.CityId
            });
            return hotel;
        }

        public HotelItemViewModel GetHotelsByPage(int page, int pages)
        {
            int pageNo = page - 1;
            HotelItemViewModel model = new HotelItemViewModel();
            model.Pages = pages;
            model.Hotels = repository
                .GetHotels()
                .OrderByDescending(c => c.Priority)
                .Skip(pageNo * pages)
                .Take(pages)
                .Select(c => new HotelViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Priority = c.Priority,
                    DateCreate = c.DateCreate,
                    City = c.City.Name
                }).ToList();
            model.TotalPages = (int)Math.Ceiling((double)repository.TotalHotels() / pages);
            model.CurrentPage = page;

            return model;
        }

        public List<HotelViewModel> GetHotels()
        {
            return repository
                .GetHotels()
                .Select(h => new HotelViewModel
                {
                    Id = h.Id,
                    Name = h.Name,
                    Priority = h.Priority,
                    DateCreate = h.DateCreate,
                    City = h.City.Name,
                    CityId = h.CityId
                })
                .ToList();
        }

        public HotelViewModel GetHotelById(int id)
        {
            Hotel hotel = repository.GetHotels().FirstOrDefault(c => c.Id == id);
            return new HotelViewModel
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Priority = hotel.Priority,
                DateCreate = hotel.DateCreate,
                City = hotel.City.Name
            };
        }
    }
}
