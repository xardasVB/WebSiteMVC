using BLL.Abstract;
using BLL.Concrete;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSiteMVC.Areas.Admin.Controllers
{
    public class HotelController : Controller
    {
        private readonly IHotelProvider provider;
        private readonly ICityProvider cityProvider;

        public HotelController(IHotelProvider provider, ICityProvider cityProvider)
        {
            this.provider = provider;
            this.cityProvider = cityProvider;
            ViewBag.MenuHotel = true;
        }

        //public HotelController()
        //{
        //    provider = new HotelProvider();
        //    cityProvider = new CityProvider();
        //    ViewBag.MenuHotel = true;
        //}

        // GET: Hotel
        public ActionResult Index()
        {
            List<HotelViewModel> model;
            model = provider
                .GetHotels()
                .OrderByDescending(c => c.Priority)
                .ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            HotelCreateViewModel model = new HotelCreateViewModel();
            InitializeCities(ref model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(HotelCreateViewModel model)
        {
            List<string> HotelNames = provider
                .GetHotels()
                .Select(c => c.Name)
                .ToList();

            if (HotelNames.Contains(model.Name))
                ModelState.AddModelError("", "This Hotel already exists");

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Fields are incorrect");
                InitializeCities(ref model);
                return View(model);
            }
            provider.CreateHotel(model);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            provider.DeleteHotel(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var hotel = provider.GetHotelById(id);
            var model = new HotelEditViewModel
            {
                Name = hotel.Name,
                Priority = hotel.Priority,
                CityId = hotel.CityId
            };
            InitializeCities(ref model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(HotelEditViewModel model, int id)
        {
            List<string> HotelNames = provider
                .GetHotels()
                .Select(c => c.Name)
                .ToList();
            HotelNames.Remove(provider.GetHotelById(id).Name);

            if (HotelNames.Contains(model.Name))
                ModelState.AddModelError("", "This Hotel already exists");

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Fields are incorrect");
                InitializeCities(ref model);
                return View(model);
            }
            provider.EditHotel(model, id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            return View(provider.GetHotelById(id));
        }

        private void InitializeCities(ref HotelCreateViewModel model)
        {
            model.Cities = cityProvider
            .GetCities()
            .Select(c => new SelectItemViewModel
            {
                Id = c.CountryId,
                Name = c.Name
            }).ToList();
        }

        private void InitializeCities(ref HotelEditViewModel model)
        {
            model.Cities = cityProvider
            .GetCities()
            .Select(c => new SelectItemViewModel
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }
    }
}