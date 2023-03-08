using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        //Fiinding OBjetcts
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        ValueTask<TEntity?> FindAsync(params object?[]? keyValues);



        //Add objects
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);


        //DELETE OBJECTS
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);


    }
}
