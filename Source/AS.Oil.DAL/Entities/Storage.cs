using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AS.Oil.DAL.Entities
{
    [Table("storage", Schema = "dbo")]
    public class Storage
    {
        [Key]
        [Column("id")]
        [Required]
        public long Id { get; set; }

        [Column("category_id")]
        [Required]
        public int CategoryId { get; set; }

        [Column("max_volume")]
        [Required]
        public double MaxVolume { get; set; }

        [Column("min_volume")]
        [Required]
        public double MinVolume { get; set; }

        [Column("volume")]
        [Required]
        public double Volume { get; set; }

        [Column("name")]
        [Required]
        [MaxLength(1024)]
        public string Name { get; set; }

        [Column("is_deleted")]
        [Required]
        public bool IsDeleted { get; set; }

        [Column("create_dt")]
        [Required]
        public DateTime CreateDateTime { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
