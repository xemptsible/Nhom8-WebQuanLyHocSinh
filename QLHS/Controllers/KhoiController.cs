using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using QLHS.Controllers;
using QLHS.Models;
using QLHS.Patterns.Repository;

namespace QLHS.Controllers
{
    public class KhoiController : Controller
    {
        private readonly IGradeRepository _gradeRepository;

        public KhoiController(GradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        QLDEntities db = new QLDEntities();
        public ActionResult DanhSachKhoi(string SearchString = "")
        {
            IEnumerable<Khoi> grades;

            if (!string.IsNullOrEmpty(SearchString))
            {
                grades = _gradeRepository.GetAll().Where(k => k.TenKhoi.ToUpper().Contains(SearchString.ToUpper()));
            }
            else
            {
                grades = _gradeRepository.GetAll();
            }

            return View(grades.ToList());
        }

        public ActionResult ThemKhoi()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemKhoi(Khoi khoi)
        {
            if (ModelState.IsValid)
            {
                var existingKhoi = _gradeRepository.GetByName(khoi.TenKhoi);
                if (existingKhoi == null)
                {
                    _gradeRepository.Add(khoi);
                    return RedirectToAction("DanhSachKhoi");
                }
                else
                {
                    ViewBag.ErrorMessage = "Khối tồn tại";
                    return View(khoi);
                }
            }
            return View(khoi);
        }

        public ActionResult ChinhSua(int id)
        {
            var grade = _gradeRepository.GetById(id);
            return View(grade);
        }

        [HttpPost]
        public ActionResult ChinhSua(int id, Khoi khoi)
        {
            if (ModelState.IsValid)
            {
                _gradeRepository.Update(khoi); // Không cần gán lại MaKhoi = id ở đây
                return RedirectToAction("DanhSachKhoi");
            }
            return View(khoi);
        }

        public ActionResult Xoa(int id)
        {
            var khoi = _gradeRepository.GetById(id);
            return View(khoi);
        }
        [HttpPost]
        public ActionResult Xoa(int id, Khoi khoi)
        {
            try
            {
                khoi = _gradeRepository.GetById(id);
                _gradeRepository.Delete(id);
                return RedirectToAction("DanhSachKhoi");
            }
            catch
            {
                return Content("Dữ liệu này đang sử dụng bởi 1 bảng khác, Lỗi !!!");
            }
        }
    }
}