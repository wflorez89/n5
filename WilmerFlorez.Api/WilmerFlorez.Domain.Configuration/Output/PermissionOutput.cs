using System;

namespace WilmerFlorez.Domain.Configuration.Output
{
    public class PermissionOutput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid PermissionTypeId { get; set; }
    }
}
