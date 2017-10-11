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
    public class CountryController : Controller
    {
        private readonly ICountryProvider provider;
        
        public CountryController(ICountryProvider countryProvider)
        {
            provider = countryProvider;
            ViewBag.MenuCountry = true;
        }

        //public CountryController()
        //{
        //    provider = new CountryProvider();
        //    ViewBag.MenuCountry = true;
        //}

        // GET: Country
        public ActionResult Index()
        {
            List<CountryViewModel> model;
            model = provider
                .GetCountries()
                .OrderByDescending(c => c.Priority)
                .ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CountryCreateViewModel model)
        {
            List<string> countryNames = provider
                .GetCountries()
                .Select(c => c.Name)
                .ToList();

            if (countryNames.Contains(model.Name))
                ModelState.AddModelError("", "This country already exists");

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Fields are incorrect");
                return View(model);
            }
            provider.CreateCountry(model);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            provider.DeleteCountry(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var country = provider.GetCountryById(id);
            return View(new CountryEditViewModel
                        {
                            Name = country.Name,
                            Priority = country.Priority
                        });
        }

        [HttpPost]
        public ActionResult Edit(CountryEditViewModel model, int id)
        {
            List<string> countryNames = provider
                .GetCountries()
                .Select(c => c.Name)
                .ToList();
            countryNames.Remove(provider.GetCountryById(id).Name);

            if (countryNames.Contains(model.Name))
                ModelState.AddModelError("", "This country already exists");

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Fields are incorrect");
                return View(model);
            }
            provider.EditCountry(model, id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            return View(provider.GetCountryById(id));
        }
    }
}