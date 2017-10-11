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
    public class CityController : Controller
    {
        private readonly ICityProvider provider;
        private readonly ICountryProvider countryProvider;

        public CityController(ICityProvider provider, ICountryProvider countryProvider)
        {
            this.provider = provider;
            this.countryProvider = countryProvider;
            ViewBag.MenuCity = true;
        }

        //public CityController()
        //{
        //    provider = new CityProvider();
        //    countryProvider = new CountryProvider();
        //    ViewBag.MenuCity = true;
        //}

        // GET: City
        public ActionResult Index()
        {
            List<CityViewModel> model;
            model = provider
                .GetCities()
                .OrderByDescending(c => c.Priority)
                .ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            CityCreateViewModel model = new CityCreateViewModel();
            InitializeCountries(ref model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CityCreateViewModel model)
        {
            List<string> cityNames = provider
                .GetCities()
                .Select(c => c.Name)
                .ToList();

            if (cityNames.Contains(model.Name))
                ModelState.AddModelError("", "This City already exists");

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Fields are incorrect");
                InitializeCountries(ref model);
                return View(model);
            }
            provider.CreateCity(model);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            provider.DeleteCity(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var city = provider.GetCityById(id);
            var model = new CityEditViewModel
            {
                Name = city.Name,
                Priority = city.Priority,
                CountryId = city.CountryId
            };
            InitializeCountries(ref model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CityEditViewModel model, int id)
        {
            List<string> cityNames = provider
                .GetCities()
                .Select(c => c.Name)
                .ToList();
            cityNames.Remove(provider.GetCityById(id).Name);

            if (cityNames.Contains(model.Name))
                ModelState.AddModelError("", "This City already exists");

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Fields are incorrect");
                InitializeCountries(ref model);
                return View(model);
            }
            provider.EditCity(model, id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            return View(provider.GetCityById(id));
        }

        private void InitializeCountries(ref CityCreateViewModel model)
        {
            model.Countries = countryProvider
            .GetCountries()
            .Select(c => new SelectItemViewModel
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }

        private void InitializeCountries(ref CityEditViewModel model)
        {
            model.Countries = countryProvider
            .GetCountries()
            .Select(c => new SelectItemViewModel
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }
    }
}