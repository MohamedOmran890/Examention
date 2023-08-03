using Examention.Data.Models;
using Examention.EF.Repository.GenricRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examention.EF.Repository.ChoiceRepository
{
    public interface IChoiceRepository:IGenricRepository<Choice>
    {
        public Task<IEnumerable<Choice>> ChoiceByQuetionId(int Id);
    }
}
