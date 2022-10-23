using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WilmerFlorez.Entities
{
    [Table("EmployeePermisions")]
    public class EmployeePermision : Entity<Guid>
    {
        public Guid PermissionId { get; set; }
        public Guid EmployeeId { get; set; }

        [ForeignKey("PermissionId")]
        public Permission Permission { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
    }
}
