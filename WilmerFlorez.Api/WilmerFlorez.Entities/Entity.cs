using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WilmerFlorez.Entities
{
    public abstract class Entity<TPrimaryKey> 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TPrimaryKey Id { get; set; }

        public object[] GetKeys()
        {
            return new object[] { Id };
        }
    }
}
