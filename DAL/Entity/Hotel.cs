using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(maximumLength: 256)]
        public string Name { get; set; }
        public DateTime DateCreate { get; set; }
        public int Priority { get; set; }

        [ForeignKey("City")]
        public int CityId { get; set; }

        virtual public City City { get; set; }
    }
}
