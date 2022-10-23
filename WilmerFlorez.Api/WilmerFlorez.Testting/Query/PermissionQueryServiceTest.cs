using AutoMapper;
using MockQueryable.NSubstitute;
using Moq;
using WilmerFlorez.Domain.Configuration.Mappers;
using WilmerFlorez.Domain.Configuration.Output;
using WilmerFlorez.Entities;
using WilmerFlorez.Queries.Implementation;
using WilmerFlorez.Queries.Interfaces;
using WilmerFlorez.Testting.Factory;
using WilmerFlorez.Utilities.Interfaces.Kafka;
using WilmerFlorez.Utilities.Interfaces.Repository;

namespace WilmerFlorez.Testting.Query
{
    public class PermissionQueryServiceTest
    {
        private static IMapper? _mapper ;
        private Mock<IRepositoryAsync<Permission>> _permissionRepository;
        private Mock<IEventProducer> _eventProducer;

        private IPermissionQueryService _permissionQueryService;

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

            var permissionsCollection = PermissionFactoryTest.GetPermissions();
            var mocke = permissionsCollection.AsQueryable().BuildMock();
            _permissionRepository.Setup(x => x.GetAll()).Returns(mocke);

            _permissionQueryService = new PermissionQueryService
                (
                    _permissionRepository.Object,
                    _mapper,
                    _eventProducer.Object
                );

        }

        [Test]
        public async Task GetAllAsyncTest()
        {
            var result = await _permissionQueryService.GetAllAsync();
            Assert.NotNull(result);
            Assert.IsInstanceOf<List<PermissionOutput>>(result);
            Assert.That(result.Count(), Is.EqualTo(3));
            Assert.That(result.Any(c => c.Id == Guid.Parse("36426295-0AE0-4F0A-9E82-E02DCEC25B6D")),
                        Is.EqualTo(true));
        }
    }
}