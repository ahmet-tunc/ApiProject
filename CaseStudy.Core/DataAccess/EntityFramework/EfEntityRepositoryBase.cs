using CaseStudy.Core.Entities;
using CaseStudy.Core.Utilities.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public async Task<IDataResult<TEntity>> Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addEntity = context.Entry(entity);
                addEntity.State = EntityState.Added;
                await context.SaveChangesAsync();
                return new SuccessDataResult<TEntity>(entity);
            }
        }

        public IResult Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deleteEntity = context.Entry(entity);
                deleteEntity.State = EntityState.Deleted;
                context.SaveChanges();
                return new SuccessResult();
            }
        }

        public IDataResult<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return new SuccessDataResult<TEntity>(context.Set<TEntity>().SingleOrDefault(filter));
            }
        }

        public IDataResult<List<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return new SuccessDataResult<List<TEntity>>(filter != null ? context.Set<TEntity>().Where(filter).ToList() : context.Set<TEntity>().ToList());
            }
        }

        public IDataResult<TEntity> Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updateEntity = context.Entry(entity);
                updateEntity.State = EntityState.Modified;
                context.SaveChanges();
                return new SuccessDataResult<TEntity>(entity);
            }
        }
    }
}
