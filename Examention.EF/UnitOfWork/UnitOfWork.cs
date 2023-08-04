using Examention.Data.ApplicationContext;
using Examention.Data.Models;
using Examention.EF.Repository.ExamRepository;
using Examention.EF.Repository.ExamStudentRepository;
using Examention.EF.Repository.GenricRepository;
using Examention.EF.Repository.QuestionRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examention.EF.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;
        public IExamRepository Exams { get; private set; }
        public IQuestionRepository Questions { get; private set; }
        public IExamStudentRepository ExamStudents { get; private set; }
        public IGenricRepository<Student>Students { get; private set; }
        public IGenricRepository<Doctor> Doctors { get; private set; }

        public UnitOfWork(Context context)
        {
            _context = context;
            Exams = new  ExamRepository(_context);
            Questions = new QuestionRepository(_context);
            ExamStudents = new ExamStudentRepository(_context);
            Students = new GenricRepository<Student>(_context);
            Doctors = new GenricRepository<Doctor>(_context);
        }
        public int Save()
        {
            return _context.SaveChanges();
            

        }
        public void Dispose()
        {
             _context.Dispose();
        }
    }
}
