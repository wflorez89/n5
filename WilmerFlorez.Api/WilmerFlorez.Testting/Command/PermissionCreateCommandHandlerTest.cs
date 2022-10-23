using AutoMapper;
using Moq;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WilmerFlorez.Commands.EventHandlers.CreatePermisssion;
using WilmerFlorez.Domain.Configuration.Commands.Permission;
using WilmerFlorez.Domain.Configuration.Mappers;
using WilmerFlorez.Domain.Configuration.Output;
using WilmerFlorez.Domain.Entities;
using WilmerFlorez.Queries.Implementation;
using WilmerFlorez.Testting.Factory;
using WilmerFlorez.Utilities.Interfaces.Kafka;
using WilmerFlorez.Utilities.Interfaces.Repositories;
using WilmerFlorez.Utilities.Interfaces.UnitOfWorks;

namespace WilmerFlorez.Testting.Command
{
    public class PermissionCreateCommandHandlerTest
    {
        private Mock<IRepositoryAsync<Permission>> _permissionRepository;
        private Mock<IUnitOfWorkAsync> _unitOfWork;
        private IMapper? _mapper;
        private Mock<IEventProducer> _eventProducer;
        private Mock<IElasticClient> _elasticClient;

        private PermissionCreateCommandHandler _permissionCreateCommandHandler;


        [SetUp]
        public void Setup()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new PermissionMapper());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            _permissionRepository = new Mock<IRepositoryAsync<Permission>>();
            _eventProducer = new Mock<IEventProducer>();
            _unitOfWork = new Mock<IUnitOfWorkAsync>();
            _elasticClient = new Mock<IElasticClient>();

            _permissionCreateCommandHandler = new PermissionCreateCommandHandler(
                        _permissionRepository.Object,
                        _unitOfWork.Object,
                        _mapper,
                        _eventProducer.Object,
                        _elasticClient.Object
                );

        }

        [Test]
        public async Task CreatePermissionTest()
        {
            var command = new PermissionCreateCommand {
                Name = "Permission 1",
                PermissionTypeId = Guid.Parse("CB926D7C-9AFB-4118-8841-C55DF6AD49AF")
            };
            var result = await _permissionCreateCommandHandler.Handle(command, new CancellationToken());
            Assert.NotNull(result);
            Assert.IsInstanceOf<PermissionOutput>(result);
            Assert.That(command.Name, Is.EqualTo(result.Name));
        }
    }
}
