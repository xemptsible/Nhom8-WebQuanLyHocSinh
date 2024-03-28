using QLHS.Models;
using QLHS.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace QLHS.Controllers
{
    public class BangDiemController : Controller
    {
        QLDEntities database = new QLDEntities();

        public ActionResult DanhSachBangDiem(string SearchString = "")
        {
            var scores = database.Scores.ToList();

            foreach (var sb in scores)
            {
                var student = database.Students.FirstOrDefault(l => l.MaHS == sb.MaHS);
                if (student != null)
                {
                    sb.TenHS = student.HoTen;
                }
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                scores = scores.Where(hs => hs.TenHS.ToUpper().Contains(SearchString.ToUpper())).ToList();
            }

            foreach (var sb in scores)
            {
                var subject = database.Subjects.FirstOrDefault(l => l.MaMon == sb.MaMon);
                if (subject != null)
                {
                    sb.TenMon = subject.TenMon;
                }
            }
            return View(scores);
        }

        public ActionResult DanhSachBangDiem_GV(string SearchString = "")
        {
            int id_teacher = (int)Session["MaGV"];

            if (id_teacher != 0)
            {
                var _class = database.Classes.FirstOrDefault(l => l.GVCN == id_teacher);

                if (_class != null)
                {
                    var scores = database.Scores.Where(bd => database.Students.Any(hs => hs.MaHS == bd.MaHS && hs.Lop == _class.MaLop)).ToList();

                    if (!string.IsNullOrEmpty(SearchString))
                    {
                        scores = scores.Where(bd => bd.TenHS.ToUpper().Contains(SearchString.ToUpper())).ToList();
                    }

                    foreach (var sb in scores)
                    {
                        var student = database.Students.FirstOrDefault(h => h.MaHS == sb.MaHS);
                        if (student != null)
                        {
                            sb.TenHS = student.HoTen;
                        }
                    }
                    foreach (var sb in scores)
                    {
                        var subject = database.Subjects.FirstOrDefault(m => m.MaMon == sb.MaMon);
                        if (subject != null)
                        {
                            sb.TenMon = subject.TenMon;
                        }
                    }

                    return View(scores);
                }
            }
            return View(new List<BangDiem>());
        }

        public ActionResult ChinhSua(int id)
        {
            var scores = database.Scores.ToList();

            foreach (var sb in scores)
            {
                var student = database.Students.FirstOrDefault(h => h.MaHS == sb.MaHS);
                if (student != null)
                {
                    sb.TenHS = student.HoTen;
                }

                var subject = database.Subjects.FirstOrDefault(m => m.MaMon == sb.MaMon);
                if (subject != null)
                {
                    sb.TenMon = subject.TenMon;
                }
            }
            return View(database.Scores.Where(s => s.MaBD == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult ChinhSua(int id, BangDiem bd)
        {
            if (ModelState.IsValid)
            {
                database.Entry(bd).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("DanhSachBangDiem");
            }
            return View(bd);
        }

        public ActionResult ChinhSua_GV(int id)
        {
            return View(database.Scores.Where(s => s.MaBD == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult ChinhSua_GV(int id, BangDiem bd)
        {
            if (ModelState.IsValid)
            {
                database.Entry(bd).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("DanhSachBangDiem_GV");
            }
            return View(bd);
        }

        public ActionResult Xoa(int id)
        {
            return View(database.Scores.Where(s => s.MaBD == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Xoa(int id, BangDiem bd)
        {
            try
            {
                bd = database.Scores.Where(s => s. MaBD == id).FirstOrDefault();
                database.Scores.Remove(bd);
                database.SaveChanges();
                return RedirectToAction("DanhSachBangDiem");
            }
            catch
            {
                return Content("Dữ liệu này đang sử dụng bởi 1 bảng khác, Lỗi !!!");
            }
        }

        public ActionResult DanhSachMonHoc(string SearchString = "")
        {
            if (SearchString != "")
            {
                var MH = database.Subjects.Where(x => x.TenMon.ToUpper().Contains(SearchString.ToUpper()));
                return View(MH.ToList());
            }
            else
                return View(database.Subjects.ToList());
        }

        public ActionResult ChonMonHoc(int id_subject)
        {
            Session["MaMonHoc"] = id_subject;

            return RedirectToAction("DanhSachLopDiem", "Lop");
        }

        public ActionResult NhapDiem()
        {
            int id_student = (int)Session["MaHocSinh"];
            int id_subject = (int)Session["MaMonHoc"];

            if (id_student == 0 || id_subject == 0)
            {
                return RedirectToAction("DanhSachBangDiem");
            }
            return View();
        }

        [HttpPost]
        public ActionResult LuuDiem(DiemViewModel s)
        {
            if (ModelState.IsValid)
            {
                using (var db = new QLDEntities())
                {
                    int id_student = (int)Session["MaHocSinh"];
                    int id_subject = (int)Session["MaMonHoc"];
                    int score = s.Diem;

                    var existingDiem = db.Scores.FirstOrDefault(d => d.MaHS == id_student && d.MaMon == id_subject);

                    if (existingDiem != null)
                    {
                        existingDiem.Diem = score;
                    }
                    else
                    {
                        var scoreEntity = new BangDiem
                        {
                            MaHS = id_student,
                            MaMon = id_subject,
                            Diem = score
                        };
                        db.Scores.Add(scoreEntity);
                    }
                    db.SaveChanges();
                }
                return RedirectToAction("DanhSachHocSinhTheoLopDiem", "HocSinh", new { id_class = (int)Session["MaLop"] });
            }
            return View("NhapDiem");
        }
    }
}