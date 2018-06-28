﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using URF.Core.Abstractions.Trackable;
using URF.Core.EF.Tests.Models;
using URF.Core.Services.DomainModel;

namespace URF.Core.EF.Tests.Services
{
    public class CustomerDomainService: DomainService<CustomerDomainModel,Customer>,ICustomerDomainService
    {
        private readonly ITrackableRepository<Customer> _repository;
        private readonly IMapper _mapper;
        public CustomerDomainService(ITrackableRepository<Customer> repository) : base(repository)
        {
            _repository = repository;
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<CustomerDomainModel, Customer>();
                cfg.CreateMap<Customer, CustomerDomainModel>();
            });
            _mapper = config.CreateMapper();

        }

        public async Task<IEnumerable<CustomerDomainModel>> CustomersByCompany(string companyName)
        {
           var data= await Repository
                .Queryable()
                .Where(x => x.CompanyName.Contains(companyName))
                .ToListAsync();
            return _mapper.Map<List<Customer>,List<CustomerDomainModel>>(data);
        }
    }
}