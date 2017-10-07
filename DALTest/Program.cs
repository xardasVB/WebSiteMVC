using DAL.Abstract;
using DAL.Concrete;
using BLL.Abstract;
using BLL.Concrete;
using BLL.Models;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ICountryRepository cor = new CountryRepository();
            ICityRepository cir = new CityRepository();
            IHotelRepository hr = new HotelRepository();

            ICountryProvider cop = new CountryProvider();
            ICityProvider cip = new CityProvider();
            IHotelProvider hp = new HotelProvider();

            foreach (var item in cip.GetCities())
            {
                Console.Write(item.Id + " | ");
                Console.Write(item.Name + " | ");
                Console.Write(item.Priority + " | ");
                Console.Write(item.DateCreate + " | ");
                Console.WriteLine();
            }
            
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            //hp.CreateHotel(new HotelCreateViewModel { Name = "Marlen", Priority = 5, CityId = 1 });
            //hr.AddHotel(new Hotel { Name = "Mir", CityId = 1, Priority = 15, DateCreate = DateTime.Now });

            foreach (var item in hr.GetHotels())
            {
                Console.Write(item.Id + " | ");
                Console.Write(item.Name + " | ");
                Console.Write(item.Priority + " | ");
                Console.Write(item.DateCreate + " | ");
                Console.Write(item.CityId + " | ");
                Console.Write(item.City.Name + " | ");
                Console.WriteLine();
            }

            //foreach (var item in cor.GetCountries())
            //{
            //    Console.Write(item.Id + " | ");
            //    Console.Write(item.Name + " | ");
            //    Console.Write(item.Priority + " | ");
            //    Console.Write(item.DateCreate + " | ");
            //    foreach (var it in item.Cities)
            //    {
            //        Console.Write(it.Name + " ");
            //    }
            //    Console.WriteLine();
            //}

            //foreach (var item in cir.GetCities())
            //{
            //    Console.Write(item.Id + " | ");
            //    Console.Write(item.Name + " | ");
            //    Console.Write(item.Priority + " | ");
            //    Console.Write(item.DateCreate + " | ");
            //    foreach (var it in item.Hotels)
            //    {
            //        Console.Write(it.Name + " ");
            //    }
            //    Console.WriteLine();
            //}
            
            //EFContext ctx = new EFContext();
            //foreach (var item in ctx.Countries.ToList())
            //{
            //    Console.WriteLine(item.Name);
            //}
        }
    }
}
