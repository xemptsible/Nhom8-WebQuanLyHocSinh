using QLHS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLHS.Patterns.Abtract_Factory
{
    public class AdminRoleChecker : IRoleChecker
    {
        public bool CheckRole(GiaoVien gv)
        {
            return gv.Role == "Admin";
        }
    }

    public class TeacherRoleChecker : IRoleChecker
    {
        public bool CheckRole(GiaoVien gv)
        {
            return gv.Role == "Giáo viên";
        }
    }
}