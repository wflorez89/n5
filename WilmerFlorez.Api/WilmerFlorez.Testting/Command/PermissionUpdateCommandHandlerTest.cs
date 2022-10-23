using AutoMapper;
using MockQueryable.NSubstitute;
using Moq;
using Nest;
using WilmerFlorez.Commands.EventHandlers.UpdatePermisssion;
using WilmerFlorez.Common.Exceptions;
using WilmerFlorez.Domain.Configuration.Commands.Permission;
using WilmerFlorez.Domain.Configuration.Mappers;
using WilmerFlorez.Domain.Configuration.Output;
using WilmerFlorez.Domain.Entities;
using WilmerFlorez.Testting.Factory;
using WilmerFlorez.Utilities.Interfaces.Kafka;
using WilmerFlorez.Utilities.Interfaces.Repositories;
using WilmerFlorez.Utilities.Interfaces.UnitOfWorks;

namespace WilmerFlorez.Testting.Command
{
    public class PermissionUpdateCommandHandlerTest
    {
        private Mock<IRepositoryAsync<Permission>> _permissionRepository;
        private Mock<IUnitOfWorkAsync> _unitOfWork;
        private IMapper? _mapper;
        private Mock<IEventProducer> _eventProducer;
        private Mock<IElasticClient> _elasticClient;

        private PermissionUpdateCommandHandler _permissionUpdateCommandHandler;


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

            var permissionsCollection = PermissionFactoryTest.GetPermissions();
            var mocke = permissionsCollection.AsQueryable().BuildMock();
            _permissionRepository.Setup(x => x.GetAll()).Returns(mocke);


            _permissionUpdateCommandHandler = new PermissionUpdateCommandHandler(
                        _permissionRepository.Object,
                        _mapper,
                        _eventProducer.Object
                );

        }

        [Test]
        public async Task UpdatePermissionTest()
        {
            var command = new PermissionUpdateCommand
            {
                Id = Guid.Parse("36426295-0AE0-4F0A-9E82-E02DCEC25B6D"),
                Name = "Permission 2",
                PermissionTypeId = Guid.Parse("CB926D7C-9AFB-4118-8841-C55DF6AD49AF")
            };
            var result = await _permissionUpdateCommandHandler.Handle(command, new CancellationToken());
            Assert.NotNull(result);
            Assert.IsInstanceOf<PermissionOutput>(result);
            Assert.That(command.Name, Is.EqualTo(result.Name));
        }

        [Test]
        public  void UpdatePermissionErrorTest()
        {

            var command = new PermissionUpdateCommand
            {
                Id = Guid.Parse("36426295-0AE0-4F0A-9E82-E02DCEC25B67"),
                Name = "Permission 2",
                PermissionTypeId = Guid.Parse("CB926D7C-9AFB-4118-8841-C55DF6AD49AF")
            };
            Assert.ThrowsAsync<CustomException>(async () => await _permissionUpdateCommandHandler.Handle(command, new CancellationToken()));
        }
    }
}
