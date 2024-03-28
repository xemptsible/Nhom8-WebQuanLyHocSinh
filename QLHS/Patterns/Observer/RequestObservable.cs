using QLHS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLHS.Patterns.Observer
{
    public class RequestObservable : IRequest
    {
        public int Notify(GiaoVien giaoVien)
        {
            QLDEntities db = new QLDEntities();
            var request_teacher = db.Requests.Where(yc => yc.MaGV == giaoVien.MaGV).ToList();
            int count_request_teacher = request_teacher.Count(y => y.TrangThai);

            return count_request_teacher;
        }
    }
}