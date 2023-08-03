using Examention.Data.Models;
using Examention.EF.Repository.ExamRepository;
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
        int Save();
    }
}
