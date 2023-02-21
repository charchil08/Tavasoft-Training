using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.DataAccess.Repositories
{
    public interface IRepository<T> where T : class
    {
        //lambda expression -> x = x.id == id
        //_context.Product.Include("categories").toList()

        IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate = null, string? includeProperties = null);

        T GetT(Expression<Func<T, bool>>? predicate = null, string? includeProperties = null);

        void Add(T entity);

        void Delete(T entity);

        void DeleteRange(IEnumerable<T> entities);
    }
}