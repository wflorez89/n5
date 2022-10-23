using System;
using System.Collections.Generic;

namespace WilmerFlorez.Domain.Entities
{
    public class PermissionType : Entity<Guid>
    {
        public string NameType { get; set; }

        public ICollection<Permission> Permissions { get; set; }
    }
}
