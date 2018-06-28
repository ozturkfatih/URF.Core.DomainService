﻿using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions.Services.DomainModel;
using URF.Core.Abstractions.Trackable;

namespace URF.Core.Services.DomainModel
{
    public abstract class DomainService<TDomainModel, TEntity> : Service<TEntity>,IDomainService<TDomainModel,TEntity> 
        where TEntity: class, ITrackable 
        where TDomainModel : class
    {
        private readonly ITrackableRepository<TEntity> _repository;
        private readonly IMapper _mapper;
        protected DomainService(ITrackableRepository<TEntity> repository) : base(repository)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<TDomainModel, TEntity>();
                cfg.CreateMap<TEntity, TDomainModel>();
            });
            _mapper = config.CreateMapper();
            _repository = repository;
        }

        public virtual async Task<TDomainModel> FindAsync(object[] keyValues, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.FindAsync(keyValues, cancellationToken);
            return _mapper.Map<TEntity, TDomainModel>(entity);
        }
        public virtual async Task<TDomainModel> FindAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.FindAsync(keyValue, cancellationToken);
            return _mapper.Map<TEntity, TDomainModel>(entity);
        }
        public virtual void Insert(TDomainModel item)
        {
            var entity = _mapper.Map<TDomainModel, TEntity>(item);
            _repository.Insert(entity);
        }
        public virtual void Update(TDomainModel item)
        {
            var entity = _mapper.Map<TDomainModel, TEntity>(item);
            _repository.Update(entity);
        }
        public virtual void Delete(TDomainModel item)
        {
            var entity = _mapper.Map<TDomainModel, TEntity>(item);
            _repository.Delete(entity);
        }
        public virtual void Attach(TDomainModel item)
        {
            var entity = _mapper.Map<TDomainModel, TEntity>(item);
            _repository.Attach(entity);
        }
        public virtual void Detach(TDomainModel item)
        {
            var entity = _mapper.Map<TDomainModel, TEntity>(item);
            _repository.Detach(entity);
        }
    }
}