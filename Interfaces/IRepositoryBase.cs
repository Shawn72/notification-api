using System;
using System.Linq;
using System.Linq.Expressions;

namespace NotificationApi.Interfaces
{
	public interface IRepositoryBase<T>
	{
		IQueryable<T> FindAll();
		IQueryable<T> FindAllAlerts();
		int TotalAlertPages();
		IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
		void Create(T entity);
		void Update(T entity);
		void Delete(T entity);
	}
}
