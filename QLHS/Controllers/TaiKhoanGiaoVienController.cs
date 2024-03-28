using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLHS.Models;
using QLHS.Patterns.Abtract_Factory;
using QLHS.Patterns.Observer;
using QLHS.Patterns.Singleton;

namespace QLHS.Controllers
{
    public class TaiKhoanGiaoVienController : Controller
    {
        private Context context;

        public TaiKhoanGiaoVienController()
        {
            context = Context.Instance;
        }

        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TaiKhoanDangNhap(GiaoVien gv)
        {
            var db = context.DbContext;

            var check = db.Teachers.Where(s => s.TAIKHOAN == gv.TAIKHOAN && s.MATKHAU == gv.MATKHAU).FirstOrDefault();

            if (check == null)
            {
                ViewBag.Error = "Sai Tài Khoản hoặc Mật Khẩu";
                return View("DangNhap");
            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = true;
                Session["HoTenGV"] = check.HoTenGV;
                Session["MaGV"] = check.MaGV;
                Session["Status"] = check.TrangThai;

                var request_admin = db.Requests.ToList();
                int count_request_admin = request_admin.Count(y => !y.TrangThai);
                Session["NotificationCount"] = count_request_admin;

                IRequest request_teacher = new RequestObservable();
                int count_request_teacher = request_teacher.Notify(gv);
                Session["NotificationCount_GV"] = count_request_teacher;

                IRoleCheckerFactory factory = null;
                if (check.Role == "Admin")
                {
                    factory = new AdminRoleCheckerFactory();
                }
                else
                {
                    factory = new TeacherRoleCheckerFactory();
                }

                IRoleChecker roleChecker = factory.CreateRoleChecker();

                bool isAdmin = roleChecker.CheckRole(check);

                if (isAdmin)
                {
                    return RedirectToAction("ChiTiet", new { id = check.MaGV });
                }
                else
                {
                    return RedirectToAction("ChiTiet_GV", new { id = check.MaGV });
                }
            }
        }

        public ActionResult TrangChu()
        {
            int id_teacher = (int)Session["MaGV"];
            return RedirectToAction("ChiTiet", new { id = id_teacher });
        }

        public ActionResult ChiTiet(int id)
        {
            QLDEntities _db;

            _db = new QLDEntities();

            return View(_db.Teachers.Where(s => s.MaGV == id).FirstOrDefault());
        }

        public ActionResult TrangChu_GV()
        {
            int id_teacher = (int)Session["MaGV"];
            return RedirectToAction("ChiTiet_GV", new { id = id_teacher });
        }

        public ActionResult ChiTiet_GV(int id)
        {
            var db = context.DbContext;
            return View(db.Teachers.Where(s => s.MaGV == id).FirstOrDefault());
        }

        public ActionResult ChinhSua(int id)
        {
            QLDEntities _db;

            _db = new QLDEntities();

            return View(_db.Teachers.Where(s => s.MaGV == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult ChinhSua(int id, GiaoVien gv)
        {
            QLDEntities _db;

            _db = new QLDEntities();

            if (ModelState.IsValid)
            {
                _db.Entry(gv).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("TrangChu");
            }
            return View(gv);
        }

        public ActionResult ChinhSua_GV(int id)
        {
            QLDEntities _db;

            _db = new QLDEntities();

            return View(_db.Teachers.Where(s => s.MaGV == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult ChinhSua_GV(int id, GiaoVien gv)
        {
            QLDEntities _db;

            _db = new QLDEntities();

            if (ModelState.IsValid)
            {
                _db.Entry(gv).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("TrangChu_GV");
            }
            return View(gv);
        }
    }
}