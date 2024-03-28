using QLHS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLHS.Patterns.Strategy
{
    public interface ISearchStrategy
    {
        List<HocSinh> SearchStrategy(string searchString, QLDEntities db);
    }
}