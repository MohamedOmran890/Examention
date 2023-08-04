using Examention.Data.ApplicationContext;
using Examention.Data.Models;
using Examention.EF.Repository.GenricRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examention.EF.Repository.ExamStudentRepository
{
    public class ExamStudentRepository : GenricRepository<ExamStudent>, IExamStudentRepository
    {
        private Context _context;
        public ExamStudentRepository(Context Context) : base(Context)
        {
            _context = Context;
        }

        public async Task<IEnumerable<ExamStudent>> GetGradeForExam(int examId)
        {
            return await _context.ExamStudents.Include(s=>s.Student).ThenInclude(e=>e.User).Where(e => e.ExamId == examId).ToListAsync();
        }
        public async Task<IEnumerable<ExamStudent>> GetGradeForStudent(string id)
        {
            return await _context.ExamStudents.Include(s => s.Student).ThenInclude(u => u.User).Where(d => d.Student.UserId == id).ToListAsync();
        }

        public async Task<ExamStudent> GetStudentGradeInExam(string studentId,int examId)
        {
            return await _context.ExamStudents.Include(e =>e.Student).ThenInclude(u=>u.User).FirstOrDefaultAsync(e => e.ExamId == examId);
        }
    }
}
