using QLHS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLHS.Patterns.Strategy
{
    public class StudentSearch
    {
        public class IdStudentSearch : ISearchStrategy
        {
            public List<HocSinh> SearchStrategy(string searchString, QLDEntities db)
            {
                var result = db.Students.Where(p => p.MaHS.ToString().Contains(searchString)).ToList();
                return result;
            }
        }

        public class NameStudentSearch : ISearchStrategy
        {
            public List<HocSinh> SearchStrategy(string searchString, QLDEntities db)
            {
                var result = db.Students.Where(p => p.HoTen.Contains(searchString)).ToList();
                return result;
            }
        }

        public class ClassStudentSearch: ISearchStrategy
        {
            public List<HocSinh> SearchStrategy(string searchString, QLDEntities db)
            {
                var student = db.Students.ToList();

                var result = student.Where(p => {
                    var lop = db.Classes.FirstOrDefault(l => l.MaLop == p.Lop);
                    return lop != null && lop.TenLop.Contains(searchString);
                }).ToList(); 
                
                return result;
            }
        }

        public class CourseStudentSearch : ISearchStrategy
        {
            public List<HocSinh> SearchStrategy(string searchString, QLDEntities db)
            {
                var student = db.Students.ToList();

                var result = student.Where(p => {
                    var khoa = db.Courses.FirstOrDefault(l => l.MaKhoa == p.Khoa);
                    return khoa != null && khoa.TenKhoa.Contains(searchString);
                }).ToList();

                return result;
            }
        }

        public class PhoneStudentSearch : ISearchStrategy
        {
            public List<HocSinh> SearchStrategy(string searchString, QLDEntities db)
            {
                var result = db.Students.Where(p => p.SDT.Contains(searchString)).ToList();
                return result;
            }
        }
    }
}