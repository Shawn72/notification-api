using Microsoft.EntityFrameworkCore;
using NotificationApi.Context;
using NotificationApi.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace NotificationApi.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DatabaseContext _context { get; set; }

        public RepositoryBase(DatabaseContext repoContext)
        {
            _context = repoContext;
        }

        public IQueryable<T> FindAll()
        {
            return this._context.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this._context.Set<T>()
                .Where(expression)
                .AsNoTracking();
        }

        public void Create(T entity)
        {
            this._context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this._context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this._context.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAllAlerts()
        {
            var allalerts = _context.CustomerAlert
             .FromSqlRaw("SELECT * FROM dbo.CustomerAlert")
             .ToList().AsQueryable();
            return (IQueryable<T>)allalerts;
        }

        public int TotalAlertPages()
        {
            int PageSize = 100;

            int totalRecords =  _context.CustomerAlert
           .FromSqlRaw("SELECT * FROM dbo.CustomerAlert").Count();
            return (int)Math.Ceiling((double)totalRecords / PageSize);
        }
    }
}
