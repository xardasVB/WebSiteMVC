using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class CityViewModel
    {
        [Display(Name = "City Id")]
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Creation Date")]
        public DateTime DateCreate { get; set; }
        [Display(Name = "Priority")]
        public int Priority { get; set; }
        [Display(Name = "Country")]
        public string Country { get; set; }
        [Display(Name = "Country Id")]
        public int CountryId { get; set; }
    }

    public class CityItemViewModel
    {
        public List<CityViewModel> Cities { get; set; }
        public int TotalPages { get; set; }
        [Range(1, short.MaxValue)]
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
