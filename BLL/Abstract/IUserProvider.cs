﻿using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IUserProvider
    {
        StatusAccountViewModel Register(RegisterViewModel model);
    }
}
