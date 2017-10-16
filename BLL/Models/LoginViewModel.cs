﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class LoginViewModel
    {
        [Required, EmailAddress, Display(Name = "Email")]
        public string Email { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name="Remeber me")]
        public bool IsRemembered { get; set; }
    }
}