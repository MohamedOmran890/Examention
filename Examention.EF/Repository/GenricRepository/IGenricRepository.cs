using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examention.EF.Repository.GenricRepository
{
    public interface IGenricRepository<T> where T : class
    {
        public Task<T> GetById(int Id);
        public Task<List<T>> GetList();
        public T Update(int Id, T NewObj);
        public Task<T> Create(T NewObj);
        public Task<int> DeleteById(int Id);
    }
}
