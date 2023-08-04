using Examention.Data.Models;
using Examention.EF.Repository.GenricRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examention.EF.Repository.QuestionRepository
{
    public interface IQuestionRepository:IGenricRepository<Question>
    {
        Task<IEnumerable<Choice>> ChoiceByQuetionId(int Id);
        Task<IEnumerable<Choice>> GetAll(int examId);

    }
}
