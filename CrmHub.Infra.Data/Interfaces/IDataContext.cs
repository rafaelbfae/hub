using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace CrmHub.Infra.Data.Interface
{
    /// <summary>
    ///     Defines common operations for database mapped entities.
    /// </summary>
    public interface IDataContext : IUnitOfWork, IDisposable
    {
        /// <summary>
        ///     Gets the <see cref="DbEntityEntry" /> of the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <param name="entity">The entity for which the entry should be returned.</param>
        /// <returns>The <see cref="DbEntityEntry" /> of the entity.</returns>
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        ///     Gets the dataset that handles entities of the specified type.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <returns>Dataset that handles instances of the specified entity type.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Set",
            Justification = "This interface is consistent with the DbContext, which also violates this rule.")]
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
