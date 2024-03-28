using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using QLHS.Models;

namespace QLHS.Controllers
{
    public class LopController : Controller
    {
        QLDEntities database = new QLDEntities();

        public ActionResult DanhSachLop(string SearchString = "")
        {
            var classes = database.Classes.ToList();

            if (!string.IsNullOrEmpty(SearchString))
            {
                classes = classes.Where(hs => hs.TenLop.ToUpper().Contains(SearchString.ToUpper())).ToList();
            }
            foreach (var _class in classes)
            {
                var grade = database.Grades.FirstOrDefault(l => l.MaKhoi == _class.Khoi);
                if (grade != null)
                {
                    _class.TenKhoi = grade.TenKhoi;
                }

                var teacher = database.Teachers.FirstOrDefault(l => l.MaGV == _class.GVCN);
                if (teacher != null)
                {
                    _class.TenGVCN = teacher.HoTenGV;
                }

                // Tính tổng học sinh hiện tại của lớp
                int total_student = database.Students.Count(hs => hs.Lop == _class.MaLop);
                _class.SLHT = total_student;
            }

            return View(classes);
        }

        public ActionResult DanhSachLopDiem()
        {
            int? id_subject = Session["MaMonHoc"] as int?;
            if (id_subject.HasValue)
            {
                var classes = database.Classes.ToList();
                foreach (var _class in classes)
                {
                    var grade = database.Grades.FirstOrDefault(l => l.MaKhoi == _class.Khoi);
                    if (grade != null)
                    {
                        _class.TenKhoi = grade.TenKhoi;
                    }
                }

                foreach (var _class in classes)
                {
                    var teacher = database.Teachers.FirstOrDefault(l => l.MaGV == _class.GVCN);
                    if (teacher != null)
                    {
                        _class.TenGVCN = teacher.HoTenGV;
                    }
                }

                return View(classes);
            }
            else
            {
                return RedirectToAction("DanhSachMonHoc", "MonHoc");
            }
        }

        public ActionResult ThemLop()
        {
            List<Khoi> grades = database.Grades.ToList();

            ViewBag.selectGrade = new SelectList(grades, "MaKhoi", "TenKhoi", "");

            List<GiaoVien> teacher = database.Teachers.ToList();

            ViewBag.selectTeacher = new SelectList(teacher, "MaGV", "HoTenGV", "");

            Lop lp = new Lop();

            return View();
        }

        [HttpPost]
        public ActionResult ThemLop(Lop lp)
        {
            if (ModelState.IsValid)
            {
                var check_id = database.Classes.Where(s => s.MaLop == lp.MaLop).FirstOrDefault();

                if (check_id == null)
                {
                    Lop clonedClass = lp.Clone() as Lop;

                    database.Classes.Add(clonedClass);

                    database.SaveChanges();

                    return RedirectToAction("DanhSachLop");
                }
                else
                {
                    ViewBag.selectGrade = new SelectList(database.Grades.ToList(), "MaKhoi", "TenKhoi", "");

                    ViewBag.selectTeacher = new SelectList(database.Teachers.ToList(), "MaGV", "HoTenGV", "");

                    ViewBag.ErrorMessage = "Mã lớp tồn tại";

                    return View(lp);
                }
            }

            ViewBag.selectGrade = new SelectList(database.Grades.ToList(), "MaKhoi", "TenKhoi", "");

            ViewBag.selectTeacher = new SelectList(database.Teachers.ToList(), "MaGV", "HoTenGV", "");

            return View(lp);
        }

        public ActionResult ChinhSua(int id) 
        {
            List<Khoi> grades = database.Grades.ToList();

            List<GiaoVien> teacher = database.Teachers.ToList();

            ViewBag.selectGrade = new SelectList(grades, "MaKhoi", "TenKhoi", "");

            ViewBag.selectTeacher = new SelectList(teacher, "MaGV", "HoTenGV", "");

            return View(database.Classes.Where(s => s.MaLop == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult ChinhSua(int id, Lop lp) 
        {
            List<Khoi> grades = database.Grades.ToList();

            List<GiaoVien> teacher = database.Teachers.ToList();

            ViewBag.selectGrade = new SelectList(grades, "MaKhoi", "TenKhoi", "");

            ViewBag.selectTeacher = new SelectList(teacher, "MaGV", "HoTenGV", "");

            database.Entry(lp).State = System.Data.Entity.EntityState.Modified;

            database.SaveChanges();

            return RedirectToAction("DanhSachLop");
        }

        public ActionResult Xoa(int id)
        {
            return View(database.Classes.Where(s => s.MaLop == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Xoa(int id, Lop _class)
        {
            try
            {
                _class = database.Classes.Where(s => s.MaLop == id).FirstOrDefault();
                database.Classes.Remove(_class);
                database.SaveChanges();
                return RedirectToAction("DanhSachLop");
            }
            catch
            {
                return Content("Dữ liệu này đang sử dụng bởi 1 bảng khác, Lỗi !!!");
            }
        }
    }
}