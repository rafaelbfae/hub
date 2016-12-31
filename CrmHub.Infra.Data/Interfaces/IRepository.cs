using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmHub.Infra.Data.Interface
{
    /// <summary>
    ///     The interface that defines the <see cref="Repository{Entity}" />.
    /// </summary>
    /// <typeparam name="TEntity">Repository's entity type.</typeparam>
    public interface IRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        ///     Returns an <see cref="IQueryable{T}" /> of entities.
        /// </summary>
        /// <returns><see cref="IQueryable{T}" /> of entities.</returns>
        IQueryable<TEntity> AsQueryable();

        /// <summary>
        ///     Removes an entity.
        /// </summary>
        /// <param name="entityToDelete">Entity to be removed.</param>
        void Delete(TEntity entityToDelete);

        /// <summary>
        ///     Returns an entity identified by provided parameters.
        /// </summary>
        /// <param name="entityIdentifiers">Entity's identifiers.</param>
        /// <returns>Entity found.</returns>
        TEntity Find(params object[] entityIdentifiers);

        /// <summary>
        ///     Asynchronously returns an entity identified by provided parameters.
        /// </summary>
        /// <param name="entityIdentifiers">Entity's identifiers.</param>
        /// <returns>Asynchronous operation with return value equals to entity found.</returns>
        Task<TEntity> FindAsync(params object[] entityIdentifiers);

        /// <summary>
        ///     Inserts an entity.
        /// </summary>
        /// <param name="entity">Entity to be inserted.</param>
        void Insert(TEntity entity);

        /// <summary>
        ///     Updates an entity.
        /// </summary>
        /// <param name="entityToUpdate">Entity to be updated.</param>
        void Update(TEntity entityToUpdate);
    }
}
