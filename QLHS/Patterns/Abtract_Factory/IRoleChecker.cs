using QLHS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLHS.Patterns.Abtract_Factory
{
    public interface IRoleChecker
    {
        bool CheckRole(GiaoVien gv);
    }
}