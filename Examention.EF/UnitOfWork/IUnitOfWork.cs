﻿using Examention.Data.Models;
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
    public interface IUnitOfWork:IDisposable
    {
        IExamRepository Exams { get; }
        IQuestionRepository Questions { get; }
        IExamStudentRepository ExamStudents { get; }
        IGenricRepository<Student> Students { get; }
        IGenricRepository<Doctor> Doctors { get; }
        int Save();
    }
}
