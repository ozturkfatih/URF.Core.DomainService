﻿using System.Threading;
using System.Threading.Tasks;
using TrackableEntities.Common.Core;

namespace URF.Core.Abstractions.Services.DomainModel
{
    public interface IDomainService<TDomainModel, TEntity>:IService<TEntity>
        where TEntity : class, ITrackable
        where TDomainModel : class
    {
        Task<TDomainModel> FindAsync(object[] keyValues, CancellationToken cancellationToken = default);
        Task<TDomainModel> FindAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default);
        void Insert(TDomainModel item);
        void Update(TDomainModel item);
        void Delete(TDomainModel item);
        void Attach(TDomainModel item);
        void Detach(TDomainModel item);
    }
}