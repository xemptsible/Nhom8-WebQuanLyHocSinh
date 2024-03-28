using QLHS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLHS.Patterns.Observer
{
    public class GradeObservable : IGrade
    {
        public int Update(HocSinh hs)
        {
            QLDEntities db = new QLDEntities();
            var grades = db.Scores.Where(g => g.MaHS == hs.MaHS).ToList();
            int count = grades.Count();

            return count;
        }
    }
}