﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf.Interfaces
{
    public interface Iclosable
    {
        Action Close { get; set; }
    }
}
