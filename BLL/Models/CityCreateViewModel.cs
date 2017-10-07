using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class CityCreateViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Priority is required")]
        [Range(1, short.MaxValue)]
        [Display(Name = "Priority")]
        public int Priority { get; set; }
        [Required(ErrorMessage = "Country is required")]
        [Display(Name = "Country")]
        public int CountryId { get; set; }
        public List<SelectItemViewModel> Countries { get; set; }
    }
}
