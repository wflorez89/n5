using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WilmerFlorez.Common.Kafka;
using WilmerFlorez.Domain.Configuration.Output;
using WilmerFlorez.Domain.Entities;
using WilmerFlorez.Queries.Interfaces;
using WilmerFlorez.Utilities.Interfaces.Kafka;
using WilmerFlorez.Utilities.Interfaces.Repositories;

namespace WilmerFlorez.Queries.Implementation
{
    public class PermissionQueryService : IPermissionQueryService
    {
        private readonly IRepositoryAsync<Permission> _permissionRepository;
        private readonly IMapper _mapper;
        private readonly IEventProducer _eventProducer;
        public PermissionQueryService(IRepositoryAsync<Permission> permissionRepository, 
            IMapper mapper, 
            IEventProducer eventProducer)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
            _eventProducer = eventProducer;
        }

        public async Task<List<PermissionOutput>> GetAllAsync()
        {
            var collection = await _permissionRepository.GetAll().ToListAsync();
            var result = _mapper.Map<List<PermissionOutput>>(collection);
            _eventProducer.Produce(new BaseEvent { NameOperation = EnumKafkaName.get.ToString() });
            return result;
        }
    }
}
