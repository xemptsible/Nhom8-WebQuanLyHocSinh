using QLHS.Models;
using QLHS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLHS.Controllers
{
    public class YeuCauController : Controller
    {
        QLDEntities db = new QLDEntities();

        public ActionResult DanhSachYeuCau()
        {
            var request = db.Requests.ToList();

            int count = request.Count(y => !y.TrangThai);

            Session["NotificationCount"] = count;

            foreach (var _class in request)
            {
                var teacher = db.Teachers.FirstOrDefault(l => l.MaGV == _class.MaGV);

                if (teacher != null)
                {
                    _class.TenGVCN = teacher.HoTenGV;
                }
            }

            return View(request);
        }

        public ActionResult PhanHoi()
        {
            int id_teacher = (int)Session["MaGV"];

            if (id_teacher != 0)
            {
                var yeuCaus = db.Requests.Where(yc => yc.MaGV == id_teacher).ToList();
                int count = yeuCaus.Count(y => y.TrangThai);
                Session["NotificationCount_GV"] = count;
                return View(yeuCaus);
            }
            return View("ErrorView");
        }

        public ActionResult ThemYeuCau()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemYeuCau(YeuCau request)
        {
            if (ModelState.IsValid)
            {
                int id_teacher = (int)Session["MaGV"];

                if (id_teacher != 0)
                {
                    request.MaGV = id_teacher;

                    var check_id = db.Requests.Where(s => s.MaYC == request.MaYC).FirstOrDefault();

                    if (check_id == null)
                    {
                        YeuCau clonedRequest = request.Clone() as YeuCau;

                        db.Requests.Add(clonedRequest);

                        db.SaveChanges();

                        return RedirectToAction("DanhSachBangDiem_GV", "BangDiem");
                    }
                    else
                    {
                        ViewBag.Error = "Mã yêu cầu tồn tại";

                        return View(request);
                    }
                }
                return RedirectToAction("ErrorAction");
            }
            return View(request);
        }

        public ActionResult Xoa(int id)
        {
            return View(db.Requests.Where(s => s.MaYC == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Xoa(int id, YeuCau request)
        {
            try
            {
                request = db.Requests.Where(s => s.MaYC == id).FirstOrDefault();

                db.Requests.Remove(request);

                db.SaveChanges();

                return RedirectToAction("DanhSachYeuCau");
            }
            catch
            {
                return Content("Dữ liệu này đang sử dụng bởi 1 bảng khác, Lỗi !!!");
            }
        }

        public ActionResult Xoa_GV(int id)
        {
            return View(db.Requests.Where(s => s.MaYC == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Xoa_GV(int id, YeuCau request)
        {
            try
            {
                request = db.Requests.Where(s => s.MaYC == id).FirstOrDefault();

                db.Requests.Remove(request);

                db.SaveChanges();

                return RedirectToAction("PhanHoi");
            }
            catch
            {
                return Content("Dữ liệu này đang sử dụng bởi 1 bảng khác, Lỗi !!!");
            }
        }

        public ActionResult ChinhSuaTrangThaiYC(int id, bool status)
        {
            var request = db.Requests.Find(id);

            if (request == null)
            {
                return HttpNotFound();
            }

            request.TrangThai = status;

            db.SaveChanges();

            return RedirectToAction("DanhSachYeuCau");
        }
    }
}