using CrmHub.Infra.Data.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Infra.Data.Context
{
    /// <summary>
    ///     Basic implementation of a general purpose entity repository.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity that this repository handles.</typeparam>
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private IDataContext context;

        private DbSet<TEntity> dataSet;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Repository{TEntity}" /> class backed by the specified
        ///     <see cref="IDataContext" />.
        /// </summary>
        /// <param name="context">Context used to manipulate database mapped entities.</param>
        public Repository(CrmHubContext context)
        {
            this.context = context;
            this.dataSet = context.Set<TEntity>();
        }

        /// <summary>
        ///     Gets the <see cref="SqlServerDatabaseContext" /> used by this repository.
        /// </summary>
        protected IDataContext SqlServerDatabaseContext
        {
            get
            {
                return this.context;
            }
        }

        /// <inheritdoc />
        public virtual IQueryable<TEntity> AsQueryable()
        {
            return this.dataSet;
        }

        /// <inheritdoc />
        public virtual void Delete(TEntity entity)
        {
            if (this.context.Entry(entity).State == EntityState.Detached)
            {
                this.dataSet.Attach(entity);
            }

            this.dataSet.Remove(entity);
        }

        /// <inheritdoc />
        public virtual TEntity Find(params object[] entityIdentifiers)
        {
            var entity = this.dataSet.Find(entityIdentifiers);
            return entity;
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> FindAsync(params object[] entityIdentifiers)
        {
            return await this.dataSet.FindAsync(entityIdentifiers);
        }

        /// <inheritdoc />
        public virtual void Insert(TEntity entity)
        {
            this.dataSet.Add(entity);
        }

        /// <inheritdoc />
        public virtual void Update(TEntity entityToUpdate)
        {
            this.context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
