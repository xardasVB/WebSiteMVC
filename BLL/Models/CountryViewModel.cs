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

    public class CountryItemViewModel
    {
        public List<CountryViewModel> Countries { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
