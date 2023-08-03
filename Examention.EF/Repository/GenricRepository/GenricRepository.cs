using Examention.Data.ApplicationContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examention.EF.Repository.GenricRepository
{
    public class GenricRepository<T> : IGenricRepository<T> where T : class
    {
        private readonly Context _Context;

        public GenricRepository(Context Context)
        {
            _Context = Context;
        }
        public async Task<T> Create(T NewObj)
        {
             await _Context.Set<T>().AddAsync(NewObj);
           var correct= await _Context.SaveChangesAsync();
            if (correct >= 1)
                return NewObj;
            return null;
        }

        public async Task<int> DeleteById(int Id)
        {
            var Obj = await GetById(Id);
            if (Obj == null)
                return 0;
            try
            {
                 _Context.Set<T>().Remove(Obj);
             var affectRows= await _Context.SaveChangesAsync();
                return affectRows;

            }
            catch(Exception)
            {
                return 0;

            }
        }

        public async Task<T> GetById(int Id)
        {
            if (Id <= 0)
            {
                throw new ArgumentException("Invaild ID");
            }
            try {
               
                return await _Context.Set<T>().FindAsync(Id);

                }
            catch(Exception )
            {
                Console.WriteLine("Error occured");
                throw;
            }
        }

        public async Task<List<T>> GetList()
        {
            return await _Context.Set<T>().AsNoTracking().ToListAsync();
        }

        public T Update(int Id, T newObj)
        {
            var oldObject = GetById(Id);
            if (oldObject == null)
                return null;
            try
            {
                _Context.Attach(newObj);
                _Context.Set<T>().Update(newObj);
                _Context.SaveChanges();
                return newObj;
            }
            catch(Exception ex)
            {
                return null;
            }

        }

    }
}
