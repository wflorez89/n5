using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WilmerFlorez.Domain.Entities
{
    [Table("Employees")]
    public class Employee : Entity<Guid>
    {
        public string Name { get; set; }
    }
}
