using Examention.Data.Models;
using Examention.EF.Repository.GenricRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examention.EF.Repository.ExamRepository
{
    public interface IExamRepository:IGenricRepository<Exam>
    {
        Task<IEnumerable<Exam>> GetExamByLevel(int LevelId);
    }
}
