using QLHS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLHS.Patterns.Repository
{
    public class GradeRepository : IGradeRepository
    {
        private readonly QLDEntities _context;

        public GradeRepository(QLDEntities context)
        {
            _context = context;
        }

        public Khoi GetByName(string name)
        {
            return _context.Grades.FirstOrDefault(k => k.TenKhoi == name);
        }

        public Khoi GetById(int id)
        {
            return _context.Grades.Find(id);
        }

        public IEnumerable<Khoi> GetAll()
        {
            return _context.Grades.ToList();
        }

        public void Add(Khoi khoi)
        {
            _context.Grades.Add(khoi); // Thêm đối tượng Khoi vào DbContext
            _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
        }

        public void Update(Khoi khoi)
        {
            var existingKhoi = _context.Grades.Find(khoi.MaKhoi); // Tìm kiếm đối tượng Khoi trong cơ sở dữ liệu
            if (existingKhoi != null)
            {
                // Cập nhật thuộc tính của đối tượng Khoi từ khoi được truyền vào
                existingKhoi.TenKhoi = khoi.TenKhoi;
                // Và bất kỳ thuộc tính nào khác mà bạn muốn cập nhật

                _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
            }
        }

        public void Delete(int id)
        {
            var khoi = _context.Grades.Find(id);
            if (khoi != null)
            {
                _context.Grades.Remove(khoi);
                _context.SaveChanges();
            }
        }
    }
}