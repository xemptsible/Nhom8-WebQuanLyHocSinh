using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLHS.Patterns.Abtract_Factory
{
    public class AdminRoleCheckerFactory : IRoleCheckerFactory
    {
        public IRoleChecker CreateRoleChecker()
        {
            return new AdminRoleChecker();
        }
    }

    public class TeacherRoleCheckerFactory : IRoleCheckerFactory
    {
        public IRoleChecker CreateRoleChecker()
        {
            return new TeacherRoleChecker();
        }
    }
}