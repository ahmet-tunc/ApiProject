using CaseStudy.Core.Entities;
using CaseStudy.Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        IDataResult<List<T>> GetList(Expression<Func<T,bool>> filter = null);
        IDataResult<T> Get(Expression<Func<T,bool>> filter);
        Task<IDataResult<T>> Add(T entity);
        IDataResult<T> Update(T entity);
        IResult Delete(T entity);
    }
}
