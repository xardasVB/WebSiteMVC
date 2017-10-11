using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class HotelViewModel
    {
        [Display(Name = "Hotel Id")]
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Creation Date")]
        public DateTime DateCreate { get; set; }
        [Display(Name = "Priority")]
        public int Priority { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "City Id")]
        public int CityId { get; set; }
    }
}
