using Examention.Data.ApplicationContext;
using Examention.Data.Models;
using Examention.EF.Repository.GenricRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examention.EF.Repository.QuestionRepository
{
    public class QuestionRepository : GenricRepository<Question>, IQuestionRepository
    {
        private readonly Context _Context;
        public QuestionRepository(Context Context) : base(Context)
        {
            _Context = Context;
        }

        public async Task<IEnumerable<Choice>> ChoiceByQuetionId(int Id)
        {
            return (IEnumerable<Choice>)await _Context.Questions.Where(q => q.Id == Id).Select(c=>c.Choices).ToListAsync();
        }
    }
}
