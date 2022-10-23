using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WilmerFlorez.Common.Exceptions;
using WilmerFlorez.Common.Kafka;
using WilmerFlorez.Domain.Configuration.Commands.Permission;
using WilmerFlorez.Domain.Configuration.Output;
using WilmerFlorez.Entities;
using WilmerFlorez.Utilities.Interfaces.Kafka;
using WilmerFlorez.Utilities.Interfaces.Repository;

namespace WilmerFlorez.Commands.EventHandlers.UpdatePermisssion
{
    public class PermissionUpdateCommandHandler :
        IRequestHandler<PermissionUpdateCommand, PermissionOutput>
    {
        private readonly IRepositoryAsync<Permission> _permissionRepository;
        private readonly IMapper _mapper;
        private readonly IEventProducer _eventProducer;

        public PermissionUpdateCommandHandler(
           IRepositoryAsync<Permission> permissionRepository,
           IMapper mapper, 
           IEventProducer eventProducer)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
            _eventProducer = eventProducer;
        }

        public async Task<PermissionOutput> Handle(PermissionUpdateCommand notification, CancellationToken cancellationToken)
        {
            var entitie = await _permissionRepository.GetAll().FirstOrDefaultAsync(c => c.Id == notification.Id);
            if (entitie == null) throw new CustomException("permission does not exists");
            var permission = _mapper.Map(notification, entitie);
            await _permissionRepository.UpdateAsync(permission);
            await _permissionRepository.Commit();
            var result = _mapper.Map<PermissionOutput>(permission);
            _eventProducer.Produce(new BaseEvent { NameOperation = EnumKafkaName.modify.ToString() });
            return result;
        }
    }
}
