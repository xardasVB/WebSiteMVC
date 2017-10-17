﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(maximumLength: 255)]
        [EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(maximumLength: 200)]
        public string Password { get; set; }

        [Required, StringLength(maximumLength: 150)]
        public string PasswordSalt { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
