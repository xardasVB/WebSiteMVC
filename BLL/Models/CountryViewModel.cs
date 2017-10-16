using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class CountryViewModel
    {
        [Display(Name = "Coutry Id")]
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Creation Date")]
        public DateTime DateCreate { get; set; }
        [Display(Name = "Priority")]
        public int Priority { get; set; }
    }

    public class SearchCountryViewModel
    {
        public string Name { get; set; }
        public string Priority { get; set; }
    }

    public class CountryItemViewModel
    {
        public List<CountryViewModel> Countries { get; set; }
        public int TotalPages { get; set; }
        [Display(Name = "Items per page")]
        [Range(1, short.MaxValue)]
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public SearchCountryViewModel Search { get; set; }
    }
}
