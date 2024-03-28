using System.Linq;
using System.Web.Mvc;
using QLHS.Models;

namespace QLHS.Controllers
{
    public class KhoaHocController : Controller
    {
        QLDEntities db = new QLDEntities();

        public ActionResult DanhSachKhoaHoc(string SearchString = "")
        {
            if (SearchString != "")
            {
                var course = db.Courses.Where(x => x.TenKhoa.ToUpper().Contains(SearchString.ToUpper()));
                return View(course.ToList());
            }
            else
                return View(db.Courses.ToList());
        }

        public ActionResult ThemKhoaHoc()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemKhoaHoc(KhoaHoc course)
        {
            if (ModelState.IsValid)
            {
                var check_name = db.Courses.Where(s => s.TenKhoa == course.TenKhoa).FirstOrDefault();
                if (check_name == null)
                {
                    KhoaHoc clonedCourse = course.Clone() as KhoaHoc;
                    db.Courses.Add(clonedCourse);
                    db.SaveChanges();
                    return RedirectToAction("DanhSachKhoaHoc");
                }
                else
                {
                    ViewBag.ErrorMessage = "Khóa học tồn tại";
                    return View(course);
                }
            }
            return View(course);
        }

        public ActionResult ChinhSua(int id_subject)
        {
            return View(db.Courses.Where(s => s.MaKhoa == id_subject).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult ChinhSua(int id_subject, KhoaHoc course)
        {
            db.Entry(course).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DanhSachKhoaHoc");
        }

        public ActionResult Xoa(int id_subject)
        {
            return View(db.Courses.Where(s => s.MaKhoa == id_subject).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Xoa(int id_subject, KhoaHoc course)
        {
            try
            {
                course = db.Courses.Where(s => s.MaKhoa == id_subject).FirstOrDefault();
                db.Courses.Remove(course);
                db.SaveChanges();
                return RedirectToAction("DanhSachKhoaHoc");
            }
            catch
            {
                return Content("Dữ liệu này đang sử dụng bởi 1 bảng khác, Lỗi !!!");
            }
        }
    }
}