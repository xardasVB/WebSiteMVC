using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class HotelCreateViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Priority is required")]
        [Range(1, short.MaxValue)]
        [Display(Name = "Priority")]
        public int Priority { get; set; }
        [Required(ErrorMessage = "City is required")]
        [Display(Name = "City")]
        public int CityId { get; set; }
        public List<SelectItemViewModel> Cities { get; set; }
    }
}
