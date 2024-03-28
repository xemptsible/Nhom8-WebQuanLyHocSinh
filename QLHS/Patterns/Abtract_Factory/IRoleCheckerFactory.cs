using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLHS.Patterns.Abtract_Factory
{
    public interface IRoleCheckerFactory
    {
        IRoleChecker CreateRoleChecker();
    }
}