using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AS.Oil.DAL.Entities
{
    [Table("category", Schema = "dbo")]
    public class Category
    {
        public Category()
        {
            Storages = new HashSet<Storage>();
        }

        [Key]
        [Column("id")]
        [Required]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        [MaxLength(1024)]
        public string Name { get; set; }

        public virtual ICollection<Storage> Storages { get; set; }
    }
}
