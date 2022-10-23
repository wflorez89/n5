using WilmerFlorez.Domain.Entities;

namespace WilmerFlorez.Testting.Factory
{
    internal  static class PermissionFactoryTest
    {
        public static List<Permission> GetPermissions()
        {
            return new List<Permission>
            {
                new Permission
                {
                    Id = Guid.Parse("36426295-0AE0-4F0A-9E82-E02DCEC25B6D"),
                    Name = "Permission 1",
                    PermissionTypeId = Guid.Parse("CB926D7C-9AFB-4118-8841-C55DF6AD49AF"),
                },
                new Permission
                {
                    Id = Guid.Parse("F5B3782A-8178-4560-B96F-BA63561EF4DB"),
                    Name = "Permission 2",
                    PermissionTypeId = Guid.Parse("CB926D7C-9AFB-4118-8841-C55DF6AD49AF"),
                },
                 new Permission
                {
                    Id = Guid.Parse("48DDCFBE-DBAB-4080-BDFD-AEB9415968F2"),
                    Name = "Permission 3",
                    PermissionTypeId = Guid.Parse("CB926D7C-9AFB-4118-8841-C55DF6AD49AF"),
                }
            };
        }
    }
}
