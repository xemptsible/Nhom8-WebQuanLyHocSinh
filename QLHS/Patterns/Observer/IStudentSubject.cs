﻿using QLHS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLHS.Patterns.Observer
{
    internal interface IStudentSubject
    {
        int Update(HocSinh hs);
    }
}