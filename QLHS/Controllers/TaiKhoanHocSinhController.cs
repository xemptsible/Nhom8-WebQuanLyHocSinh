using QLHS.Models;
using System.Linq;
using System.Web.Mvc;
using QLHS.Patterns.Singleton;
using QLHS.Patterns.Observer;

namespace QLHS.Controllers
{
    public class TaiKhoanHocSinhController : Controller
    {
        private Context context;

        public TaiKhoanHocSinhController()
        {
            context = Context.Instance;
        }

        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TaiKhoanDangNhap(HocSinh hs)
        {
            var db = context.DbContext;

            var check = db.Students.Where(s => s.MaHS == hs.MaHS && s.MATKHAU == hs.MATKHAU).FirstOrDefault();

            if (check == null)
            {
                ViewBag.Error = "Sai Tài Khoản hoặc Mật Khẩu";
                return View("Login");
            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["TenHS"] = check.HoTen;
                Session["MaHS"] = check.MaHS;

                IGrade grade = new GradeObservable();
                int count = grade.Update(hs);
                Session["NotificationCount_Student"] = count;

                return RedirectToAction("ChiTiet", new { id = check.MaHS });
            }
        }

        public ActionResult ChiTiet(int id)
        {
            var db = context.DbContext;
            return View(db.Students.Where(s => s.MaHS == id).FirstOrDefault());
        }

        public ActionResult TrangChu()
        {
            int maHS = (int)Session["MaHS"];
            return RedirectToAction("ChiTiet", new { id = maHS });
        }

        public ActionResult XemDiem()
        {
            var db = context.DbContext;

            if (Session["MaHS"] != null)
            {
                int maHS = (int)Session["MaHS"];

                var scores = db.Scores.Where(bd => bd.MaHS == maHS).ToList();

                var student = db.Students.FirstOrDefault(hs => hs.MaHS == maHS);

                var score_board = db.Scores.ToList();
                foreach (var sb in score_board)
                {
                    var subject = db.Subjects.FirstOrDefault(l => l.MaMon == sb.MaMon);
                    if (subject != null)
                    {
                        sb.TenMon = subject.TenMon;
                    }
                }

                if (scores != null)
                {
                    ViewBag.HocSinh = student;
                    return View(scores);
                }
            }
            return View();
        }

        public ActionResult XemViPham()
        {
            var db = context.DbContext;

            if (Session["MaHS"] != null)
            {
                int id_student = (int)Session["MaHS"];

                var mistake = db.Mistakes.Where(bd => bd.MaHS == id_student).ToList();

                var student = db.Students.FirstOrDefault(hs => hs.MaHS == id_student);

                var Vps = db.Mistakes.ToList();
                foreach (var VP in Vps)
                {
                    var gv = db.Teachers.FirstOrDefault(l => l.MaGV == VP.NguoiGhiNhan);
                    if (gv != null)
                    {
                        VP.TenNguoiGhiNhan = gv.HoTenGV;
                    }
                }

                if (mistake != null)
                {
                    ViewBag.HocSinh = student;
                    return View(mistake);
                }
            }
            return View();
        }
    }
}