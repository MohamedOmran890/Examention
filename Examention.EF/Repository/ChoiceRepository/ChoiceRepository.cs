using Examention.Data.ApplicationContext;
using Examention.Data.Models;
using Examention.EF.Repository.GenricRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examention.EF.Repository.ChoiceRepository
{
    internal class ChoiceRepository : GenricRepository<Choice>, IChoiceRepository
    {
        private readonly Context _Context;
        public ChoiceRepository(Context Context) : base(Context)
        {
            _Context = Context;
        }

        public Task<IEnumerable<Choice>> ChoiceByQuetionId(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
