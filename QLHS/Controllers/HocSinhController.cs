using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using QLHS.Models;
using QLHS.Patterns.Strategy;

namespace QLHS.Controllers
{
    public class HocSinhController : Controller
    {
        QLDEntities db = new QLDEntities();

        public ActionResult DanhSachHocSinh(string SearchString = "")
        {
            var students = db.Students.ToList();

            foreach (var student in students)
            {
                var lop = db.Classes.FirstOrDefault(l => l.MaLop == student.Lop);
                if (lop != null)
                {
                    student.TenLop = lop.TenLop;
                }
            }

            foreach (var student in students)
            {
                var course = db.Courses.FirstOrDefault(l => l.MaKhoa == student.Khoa);
                if (course != null)
                {
                    student.TenKhoa = course.TenKhoa;
                }
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                ISearchStrategy NameStudent = new StudentSearch.NameStudentSearch();
                ISearchStrategy IdStudent = new StudentSearch.IdStudentSearch();
                ISearchStrategy ClassStudent = new StudentSearch.ClassStudentSearch();
                ISearchStrategy CourseStudent = new StudentSearch.CourseStudentSearch();
                ISearchStrategy PhoneStudent = new StudentSearch.PhoneStudentSearch();


                SearchContext searchContext = new SearchContext(NameStudent);
                var result = searchContext.Searching(SearchString, db);

                if (result.Count == 0)
                {
                    searchContext.SetSearchType(IdStudent);
                    result = searchContext.Searching(SearchString, db);

                    if (result.Count == 0)
                    {
                        searchContext.SetSearchType(ClassStudent);
                        result = searchContext.Searching(SearchString, db);

                        if (result.Count == 0)
                        {
                            searchContext.SetSearchType(CourseStudent);
                            result = searchContext.Searching(SearchString, db);

                            if (result.Count == 0)
                            {
                                searchContext.SetSearchType(PhoneStudent);
                                result = searchContext.Searching(SearchString, db);
                            }
                        }
                    }
                }

                students = result;
            }

            return View(students);
        }

        public ActionResult DanhSachHocSinh_GV(string SearchString = "")
        {
            int id_teacher = (int)Session["MaGV"];

            if (id_teacher != 0)
            {
                var lop = db.Classes.FirstOrDefault(l => l.GVCN == id_teacher);

                if (lop != null)
                {
                    var students = db.Students.Where(hs => hs.Lop == lop.MaLop).ToList();

                    if (!string.IsNullOrEmpty(SearchString))
                    {
                        students = students.Where(hs => hs.HoTen.ToUpper().Contains(SearchString.ToUpper())).ToList();
                    }

                    foreach (var student in students)
                    {
                        var _class = db.Classes.FirstOrDefault(l => l.MaLop == student.Lop);
                        if (_class != null)
                        {
                            student.TenLop = _class.TenLop;
                        }
                    }

                    foreach (var hocSinh in students)
                    {
                        var khoa = db.Courses.FirstOrDefault(k => k.MaKhoa == hocSinh.Khoa);
                        if (khoa != null)
                        {
                            hocSinh.TenKhoa = khoa.TenKhoa;
                        }
                    }

                    return View(students);
                }
            }
            return View(new List<HocSinh>());
        }

        public ActionResult DanhSachHocSinhLoc(string SearchString = "")
        {
            var students = db.Students.ToList();

            if (!string.IsNullOrEmpty(SearchString))
            {
                students = students.Where(hs => hs.HoTen.ToUpper().Contains(SearchString.ToUpper())).ToList();
            }
            foreach (var student in students)
            {
                var _class = db.Classes.FirstOrDefault(l => l.MaLop == student.Lop);
                if (_class != null)
                {
                    student.TenLop = _class.TenLop;
                }
            }

            foreach (var hocSinh in students)
            {
                var khoa = db.Courses.FirstOrDefault(l => l.MaKhoa == hocSinh.Khoa);
                if (khoa != null)
                {
                    hocSinh.TenKhoa = khoa.TenKhoa;
                }
            }

            return View(students);
        }

        public ActionResult DanhSachHocSinhLocDiem(string SearchString = "")
        {
            var students = db.Students.ToList();

            if (!string.IsNullOrEmpty(SearchString))
            {
                students = students.Where(hs => hs.HoTen.ToUpper().Contains(SearchString.ToUpper())).ToList();
            }
            foreach (var student in students)
            {
                var _class = db.Classes.FirstOrDefault(l => l.MaLop == student.Lop);
                if (_class != null)
                {
                    student.TenLop = _class.TenLop;
                }
            }

            foreach (var student in students)
            {
                var course = db.Courses.FirstOrDefault(l => l.MaKhoa == student.Khoa);
                if (course != null)
                {
                    student.TenKhoa = course.TenKhoa;
                }
            }

            return View(students);
        }

        public ActionResult ThemHocSinh()
        {
            List<Lop> _class = db.Classes.ToList();
            ViewBag.selectClass = new SelectList(_class, "MaLop", "TenLop", "");
            List<KhoaHoc> _course = db.Courses.ToList();
            ViewBag.selectCourse = new SelectList(_course, "MaKhoa", "TenKhoa", "");
            HocSinh student = new HocSinh();
            return View(student);
        }

        [HttpPost]
        public ActionResult ThemHocSinh(HocSinh student)
        {
            if (ModelState.IsValid)
            {
                var check_id = db.Students.Where(s => s.MaHS == student.MaHS).FirstOrDefault();

                if (check_id == null)
                {
                    HocSinh clonedStudent = student.Clone() as HocSinh;
                    db.Students.Add(clonedStudent);
                    db.SaveChanges();
                    return RedirectToAction("DanhSachHocSinh");
                }
                else
                {
                    ViewBag.selectClass = new SelectList(db.Classes.ToList(), "MaLop", "TenLop", "");
                    ViewBag.selectCourse = new SelectList(db.Courses.ToList(), "MaKhoa", "TenKhoa", "");
                    ViewBag.ErrorMessage = "Mã học sinh tồn tại";
                    return View(student);
                }
            }
            ViewBag.selectClass = new SelectList(db.Classes.ToList(), "MaLop", "TenLop", "");
            ViewBag.selectCourse = new SelectList(db.Courses.ToList(), "MaKhoa", "TenKhoa", "");
            return View(student);
        }

        public ActionResult ChiTiet(int id)
        {
            return View(db.Students.Where(s => s.MaHS == id).FirstOrDefault());
        }

        public ActionResult ChiTiet_GV(int id)
        {
            return View(db.Students.Where(s => s.MaHS == id).FirstOrDefault());
        }

        public ActionResult ChinhSua(int id)
        {
            List<Lop> _class = db.Classes.ToList();
            ViewBag.selectClass = new SelectList(_class, "MaLop", "TenLop", "");
            return View(db.Students.Where(s => s.MaHS == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult ChinhSua(HocSinh student)
        {
            if (ModelState.IsValid)
            {
                List<Lop> _class = db.Classes.ToList();
                ViewBag.selectClass = new SelectList(_class, "MaLop", "TenLop", "");
                db.Entry(student).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhSachHocSinh");
            }
            ViewBag.selectClass = new SelectList(db.Classes.ToList(), "MaLop", "TenLop", "");
            return View(student);
        }

        public ActionResult Xoa(int id)
        {
            return View(db.Students.Where(s => s.MaHS == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Xoa(int id, HocSinh student)
        {
            try
            {
                student = db.Students.Where(s => s.MaHS == id).FirstOrDefault();
                db.Students.Remove(student);
                db.SaveChanges();
                return RedirectToAction("DanhSachHocSinh");
            }
            catch
            {
                return Content("Dữ liệu này đang sử dụng bởi 1 bảng khác, Lỗi !!!");
            }
        }

        public ActionResult DanhSachHocSinhTheoLop(int id_class)
        {
            var students = db.Students.Where(hs => hs.Lop == id_class).ToList();

            foreach (var student in students)
            {
                var _class = db.Classes.FirstOrDefault(l => l.MaLop == student.Lop);
                if (_class != null)
                {
                    student.TenLop = _class.TenLop;
                }

                var course = db.Courses.FirstOrDefault(k => k.MaKhoa == student.Khoa);
                if (course != null)
                {
                    student.TenKhoa = course.TenKhoa;
                }
            }
            return View("DanhSachHocSinhLoc", students);
        }

        public ActionResult DanhSachHocSinhTheoLopDiem(int id_class)
        {
            if (Session["MaMonHoc"] != null)
            {
                int id_subject = (int)Session["MaMonHoc"];

                Session["MaLop"] = id_class;

                var students = db.Students.Where(hs => hs.Lop == id_class).ToList();

                foreach (var student in students)
                {
                    var _class = db.Classes.FirstOrDefault(l => l.MaLop == student.Lop);

                    if (_class != null)
                    {
                        student.TenLop = _class.TenLop;
                    }

                    var course = db.Courses.FirstOrDefault(k => k.MaKhoa == student.Khoa);

                    if (course != null)
                    {
                        student.TenKhoa = course.TenKhoa;
                    }
                }
                return View("DanhSachHocSinhLocDiem", students);
            }
            else
            {
                return RedirectToAction("DanhSachMonHoc", "MonHoc");
            }
        }

        public ActionResult ChonHocSinh(int id_student)
        {
            int id_subject = (int)Session["MaMonHoc"];

            Session["MaHocSinh"] = id_student;

            return RedirectToAction("NhapDiem", "BangDiem");
        }
    }
}