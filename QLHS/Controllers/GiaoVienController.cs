using QLHS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLHS.Controllers
{
    public class GiaoVienController : Controller
    {
        QLDEntities db = new QLDEntities();

        public ActionResult DanhSachGiaoVien(string SearchString = "")
        {
            if (SearchString != "")
            {
                var teacher = db.Teachers.Where(x => x.HoTenGV.ToUpper().Contains(SearchString.ToUpper()));
                return View(teacher.ToList());
            }
            else
                return View(db.Teachers.ToList());
        }

        public ActionResult ThemGiaoVien()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemGiaoVien(GiaoVien teacher)
        {
            if(ModelState.IsValid)
            {
                var check_username = db.Teachers.Where(s => s.TAIKHOAN == teacher.TAIKHOAN).FirstOrDefault();

                if (check_username == null)
                {
                    GiaoVien clonedGiaoVien = teacher.Clone() as GiaoVien;
                    db.Teachers.Add(clonedGiaoVien);
                    db.SaveChanges();
                    return RedirectToAction("DanhSachGiaoVien");
                }
                else
                {
                    ViewBag.ErrorMessage = "Tài Khoản tồn tại";
                }

                var check_phone = db.Teachers.Where(s => s.SDT == teacher.SDT).FirstOrDefault();

                if (check_phone == null)
                {
                    db.Teachers.Add(teacher);
                    db.SaveChanges();
                    return RedirectToAction("DanhSachGiaoVien");
                }
                else
                {
                    ViewBag.ErrorMessage = "SĐT tồn tại";
                }
            }   
            return View(teacher);
        }

        public ActionResult ChiTiet(int id)
        {
            return View(db.Teachers.Where(s => s.MaGV == id).FirstOrDefault());
        }

        public ActionResult ChinhSua(int id)
        {
            return View(db.Teachers.Where(s => s.MaGV == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult ChinhSua(int id, GiaoVien teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhSachGiaoVien");
            }
            return View(teacher);
        }

        public ActionResult Xoa(int id)
        {
            return View(db.Teachers.Where(s => s.MaGV == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Xoa(int id, GiaoVien teacher)
        {
            try
            {
                teacher = db.Teachers.Where(s => s.MaGV == id).FirstOrDefault();
                db.Teachers.Remove(teacher);
                db.SaveChanges();
                return RedirectToAction("DanhSachGiaoVien");
            }
            catch
            {
                return Content("Dữ liệu này đang sử dụng bởi 1 bảng khác, Lỗi !!!");
            }
        }

        public ActionResult ChinhSuaTrangThai(int id, bool status)
        {
            var teacher = db.Teachers.Find(id);

            if (teacher == null)
            {
                return HttpNotFound();
            }

            teacher.TrangThai = status;
            db.SaveChanges();
            return RedirectToAction("DanhSachGiaoVien");
        }
    }
}