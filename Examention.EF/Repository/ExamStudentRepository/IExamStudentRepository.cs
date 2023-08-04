using Examention.Data.Models;
using Examention.EF.Repository.GenricRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examention.EF.Repository.ExamStudentRepository
{
    public interface IExamStudentRepository:IGenricRepository<ExamStudent>
    {
        public Task<IEnumerable<ExamStudent>> GetGradeForExam(int examId);
        public Task<IEnumerable<ExamStudent>> GetGradeForStudent(string studentId);
        public Task<ExamStudent> GetStudentGradeInExam(string studentId, int examId);

    }
}
