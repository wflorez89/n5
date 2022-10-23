using Microsoft.EntityFrameworkCore;
using WilmerFlorez.Domain.Entities;

namespace WilmerFlorez.Database
{
    public class ContextDb : DbContext
    {
        public ContextDb(
            DbContextOptions options
        )
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<PermissionType> PermissionTypes { get; set; }
        public virtual DbSet<EmployeePermision> EmployeePermisions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Database schema
            builder.HasDefaultSchema("Wf");
        }
    }
}
