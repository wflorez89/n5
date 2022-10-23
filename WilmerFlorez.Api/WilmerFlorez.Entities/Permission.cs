using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WilmerFlorez.Entities
{
    [Table("Permissions")]
    public class Permission : Entity<Guid>
    {
        public string Name { get; set; }
        public Guid PermissionTypeId { get; set; }

        [ForeignKey("PermissionTypeId")]
        public PermissionType PermissionType { get; set; }
    }
}
