using QLHS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace QLHS.Controllers
{
    public class ViPhamController : Controller
    {
        QLDEntities database = new QLDEntities();

        public ActionResult DanhSachViPham(string SearchString = "")
        {
            var Mistake = database.Mistakes.ToList();

            foreach (var mistake in Mistake)
            {
                var student = database.Students.FirstOrDefault(l => l.MaHS == mistake.MaHS);

                if (student != null)
                {
                    mistake.TenHS = student.HoTen;
                }
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                Mistake = Mistake.Where(hs => hs.TenHS.ToUpper().Contains(SearchString.ToUpper())).ToList();
            }

            foreach (var mistake in Mistake)
            {
                var teacher = database.Teachers.FirstOrDefault(l => l.MaGV == mistake.NguoiGhiNhan);

                if (teacher != null)
                {
                    mistake.TenNguoiGhiNhan = teacher.HoTenGV;
                }
            }

            return View(Mistake);
        }

        public ActionResult DanhSachViPham_GV(string SearchString = "")
        {
            int id_teacher = (int)Session["MaGV"];

            var Mistake = database.Mistakes.Where(vp => vp.NguoiGhiNhan == id_teacher).ToList();

            foreach (var mistake in Mistake)
            {
                var student = database.Students.FirstOrDefault(l => l.MaHS == mistake.MaHS);

                if (student != null)
                {
                    mistake.TenHS = student.HoTen;
                }
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                Mistake = Mistake.Where(hs => hs.TenHS.ToUpper().Contains(SearchString.ToUpper())).ToList();
            }

            foreach (var mistake in Mistake)
            {
                var teacher = database.Teachers.FirstOrDefault(l => l.MaGV == mistake.NguoiGhiNhan);

                if (teacher != null)
                {
                    mistake.TenNguoiGhiNhan = teacher.HoTenGV;
                }
            }

            return View(Mistake);
        }

        public ActionResult ThemViPham()
        {
            List<HocSinh> student = database.Students.ToList();

            List<GiaoVien> teacher = database.Teachers.ToList();

            ViewBag.selectStudent = new SelectList(student, "MaHS", "MaHS", "");

            ViewBag.selectTeacher = new SelectList(teacher, "MaGV", "HoTenGV", "");

            ViPham vp = new ViPham();

            vp.Ngay = DateTime.Now;

            return View(vp);
        }

        [HttpPost]
        public ActionResult ThemViPham(ViPham mistake)
        {
            if (ModelState.IsValid)
            {
                var check_id = database.Mistakes.Where(s => s.MaVP == mistake.MaVP).FirstOrDefault();

                if (check_id == null)
                {
                    database.Mistakes.Add(mistake);

                    database.SaveChanges();

                    return RedirectToAction("DanhSachViPham");
                }
                else
                {
                    ViewBag.selectStudent = new SelectList(database.Students.ToList(), "MaHS", "MaHS", "");

                    ViewBag.selectTeacher = new SelectList(database.Teachers.ToList(), "MaGV", "HoTenGV", "");

                    ViewBag.ErrorMessage = "Mã vi phạm tồn tại";

                    return View(mistake);
                }
            }

            ViewBag.selectStudent = new SelectList(database.Students.ToList(), "MaHS", "MaHS", "");

            ViewBag.selectTeacher = new SelectList(database.Teachers.ToList(), "MaGV", "HoTenGV", "");

            return View(mistake);
        }

        public ActionResult ThemViPham_GV()
        {
            List<HocSinh> student = database.Students.ToList();

            ViewBag.selectStudent = new SelectList(student, "MaHS", "MaHS", "");

            ViPham vp = new ViPham();

            vp.Ngay = DateTime.Now;

            return View(vp);
        }

        [HttpPost]
        public ActionResult ThemViPham_GV(ViPham mistake)
        {
            if (ModelState.IsValid)
            {
                int id_teacher = (int)Session["MaGV"];

                var check_id_mistake = database.Mistakes.Where(s => s.MaVP == mistake.MaVP).FirstOrDefault();

                if (check_id_mistake == null)
                {
                    mistake.NguoiGhiNhan = id_teacher;

                    database.Mistakes.Add(mistake);

                    database.SaveChanges();

                    return RedirectToAction("DanhSachViPham_GV");
                }
                else
                {
                    ViewBag.selectStudent = new SelectList(database.Students.ToList(), "MaHS", "MaHS", "");

                    ViewBag.ErrorMessage = "Mã vi phạm tồn tại";

                    return View(mistake);
                }
            }

            ViewBag.selectStudent = new SelectList(database.Students.ToList(), "MaHS", "MaHS", "");

            return View(mistake);
        }

        public ActionResult ChiTiet(int id)
        {
            return View(database.Mistakes.Where(s => s.MaVP == id).FirstOrDefault());
        }

        public ActionResult ChinhSua(int id)
        {
            List<HocSinh> student = database.Students.ToList();

            List<GiaoVien> teacher = database.Teachers.ToList();

            ViewBag.selectStudent = new SelectList(student, "MaHS", "MaHS", "");

            ViewBag.selectTeacher = new SelectList(teacher, "MaGV", "HoTenGV", "");

            return View(database.Mistakes.Where(s => s.MaVP == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult ChinhSua(int id, ViPham mistake)
        {
            if (ModelState.IsValid)
            {
                ViewBag.selectStudent = new SelectList(database.Students.ToList(), "MaHS", "MaHS", "");

                ViewBag.selectTeacher = new SelectList(database.Teachers.ToList(), "MaGV", "HoTenGV", "");

                database.Entry(mistake).State = System.Data.Entity.EntityState.Modified;

                database.SaveChanges();

                return RedirectToAction("DanhSachViPham");
            }

            ViewBag.selectStudent = new SelectList(database.Students.ToList(), "MaHS", "MaHS", "");

            ViewBag.selectTeacher = new SelectList(database.Teachers.ToList(), "MaGV", "HoTenGV", "");

            return View(mistake);
        }

        public ActionResult Xoa(int id)
        {
            return View(database.Mistakes.Where(s => s.MaVP == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Xoa(int id, ViPham mistake)
        {
            try
            {
                mistake = database.Mistakes.Where(s => s.MaVP == id).FirstOrDefault();

                database.Mistakes.Remove(mistake);

                database.SaveChanges();

                return RedirectToAction("DanhSachViPham");
            }
            catch
            {
                return Content("Dữ liệu này đang sử dụng bởi 1 bảng khác, Lỗi !!!");
            }
        }

        public JsonResult GetNameStudent(int id_student)
        {
            string name_student = database.Students.FirstOrDefault(hs => hs.MaHS == id_student)?.HoTen;
            return Json(name_student, JsonRequestBehavior.AllowGet);
        }
    }
}