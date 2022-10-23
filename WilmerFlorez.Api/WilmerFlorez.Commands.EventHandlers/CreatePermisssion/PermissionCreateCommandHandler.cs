using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nest;
using System;
using System.Threading;
using System.Threading.Tasks;
using WilmerFlorez.Common.Kafka;
using WilmerFlorez.Domain.Configuration.Commands.Permission;
using WilmerFlorez.Domain.Configuration.Output;
using WilmerFlorez.Domain.Entities;
using WilmerFlorez.Utilities.Interfaces.Kafka;
using WilmerFlorez.Utilities.Interfaces.Repositories;
using WilmerFlorez.Utilities.Interfaces.UnitOfWorks;

namespace WilmerFlorez.Commands.EventHandlers.CreatePermisssion
{
    public class PermissionCreateCommandHandler :
        IRequestHandler<PermissionCreateCommand, PermissionOutput>
    {

        private readonly IRepositoryAsync<Permission> _permissionRepository;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEventProducer _eventProducer;
        private readonly IElasticClient _elasticClient;

        public PermissionCreateCommandHandler(
           IRepositoryAsync<Permission> permissionRepository,
           IUnitOfWorkAsync unitOfWork,
           IMapper mapper,
           IEventProducer eventProducer,
           IElasticClient elasticClient)
        {
            _permissionRepository = permissionRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _eventProducer = eventProducer;
            _elasticClient = elasticClient;
        }

        public async Task<PermissionOutput> Handle(PermissionCreateCommand notification, CancellationToken cancellationToken)
        {
            var entitie = _mapper.Map<Permission>(notification);
            await _permissionRepository.InsertAsync(entitie);
            await _unitOfWork.SaveChangesAsync();
            var result = _mapper.Map<PermissionOutput>(entitie);
            _eventProducer.Produce(new BaseEvent { NameOperation = EnumKafkaName.request.ToString() });
            await _elasticClient.IndexDocumentAsync(notification);
            return result;
        }
    }
}
