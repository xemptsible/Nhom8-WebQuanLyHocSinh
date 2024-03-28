using QLHS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLHS.Patterns.Repository
{
    public interface IGradeRepository
    {
        IEnumerable<Khoi> GetAll();
        Khoi GetById(int id);
        Khoi GetByName(string name);
        void Add(Khoi khoi);
        void Update(Khoi khoi);
        void Delete(int id);
    }
}