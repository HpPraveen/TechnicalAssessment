using Invoicing.Domain.DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Invoicing.Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly CosmosDbContext _context;
        internal DbSet<T> DbSet;

        public GenericRepository(CosmosDbContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }
        public List<T> Get(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "", Expression<Func<T, bool>>[]? filters = null)
        {
            IQueryable<T>? query = null;

            if (filter != null)
            {
                query = DbSet.Where(filter);
            }
            else if (filter == null)
            {
                query = DbSet;
            }

            if (filters != null)
            {
                query = filters.Aggregate(query, (current, expression) => current?.Where(expression));
            }

            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current?.Include(includeProperty));

            return orderBy != null ? orderBy(query ?? throw new InvalidOperationException()).ToList() : (query ?? throw new InvalidOperationException()).ToList();
        }

        public virtual object GetPaging(List<Expression<Func<T, bool>>>? filters = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "", int pageNumber = 0, int pageSize = 0)
        {
            IQueryable<T> query = DbSet;

            if (filters != null)
            {
                query = filters.Aggregate(query, (current, expression) => current.Where(expression));
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            var count = query.Count();

            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return new ResponseDto
            {
                Result = query.Skip((pageNumber - 1) * pageSize).Take(pageSize),
                TotalRecords = count
            };
        }

        public virtual T GetById(object id)
        {
            return DbSet.Find(id) ?? throw new InvalidOperationException();
        }

        public virtual void Insert(T entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Update(T entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void Delete(object id)
        {
            var entityToDelete = DbSet.Find(id);
            if (entityToDelete != null) Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Modified)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }
    }
}