using Examention.Data.ApplicationContext;
using Examention.Data.Models;
using Examention.EF.Repository.GenricRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examention.EF.Repository.ExamRepository
{
    public class ExamRepository : GenricRepository<Exam>, IExamRepository
    {
        private readonly Context _Context;
        public ExamRepository(Context Context) : base(Context)
        {
            _Context = Context;
        }

        public async Task<IEnumerable<Exam>> GetExamByLevel(int LevelId)
        {
            return await _Context.Exams.Where(e => e.LevelId == LevelId).ToListAsync();
        }
    }
}
