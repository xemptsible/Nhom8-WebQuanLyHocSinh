using QLHS.Models;
using System.Linq;
using System.Web.Mvc;

namespace QLHS.Controllers
{
    public class MonHocController : Controller
    {
        QLDEntities db = new QLDEntities();

        public ActionResult DanhSachMonHoc(string SearchString="")
        {
            if(SearchString != "")
            {
                var subject = db.Subjects.Where(x => x.TenMon.ToUpper().Contains(SearchString.ToUpper()));
                return View(subject.ToList());
            } 
            else
                return View(db.Subjects.ToList());
        }

        public ActionResult ThemMonHoc()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemMonHoc(MonHoc subject)
        {
            if (ModelState.IsValid)
            {
                var check_id = db.Subjects.Where(s => s.MaMon == subject.MaMon).FirstOrDefault();

                var check_name = db.Subjects.Where(s => s.TenMon == subject.TenMon).FirstOrDefault();

                if (check_id == null && check_name == null)
                {
                    MonHoc clonedSubject = subject.Clone() as MonHoc;
                    db.Subjects.Add(clonedSubject);
                    db.SaveChanges();
                    return RedirectToAction("DanhSachMonHoc");
                }
                else
                {
                    ViewBag.ErrorMessage = "Môn học tồn tại";
                    return View(subject);
                }
            }
            return View(subject);
        }

        public ActionResult ChiTiet(int id)
        {
            return View(db.Subjects.Where(s => s.MaMon == id).FirstOrDefault());
        }

        public ActionResult ChinhSua(int id)
        {
            return View(db.Subjects.Where(s => s.MaMon == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult ChinhSua(int id, MonHoc subject)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(subject).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("DanhSachMonHoc");
                }
                catch
                {
                    return Content("Dữ liệu này đang sử dụng bởi 1 bảng khác, Lỗi !!!");
                }
            }
            return View(subject);
        }

        public ActionResult Xoa(int id)
        {
            return View(db.Subjects.Where(s => s.MaMon == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Xoa(int id, MonHoc subject)
        {
            try
            {
                subject = db.Subjects.Where(s => s.MaMon == id).FirstOrDefault();
                db.Subjects.Remove(subject);
                db.SaveChanges();
                return RedirectToAction("DanhSachMonHoc");
            }
            catch
            {
                return Content("Dữ liệu này đang sử dụng bởi 1 bảng khác, Lỗi !!!");
            }
        }

    }
}