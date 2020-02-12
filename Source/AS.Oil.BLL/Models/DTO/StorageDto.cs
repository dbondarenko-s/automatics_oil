using System;

namespace AS.Oil.BLL.Models.DTO
{
    public class StorageDto
    {
        public long Id { get; set; }

        public int CategoryId { get; set; }

        public double MaxVolume { get; set; }

        public double MinVolume { get; set; }

        public double Volume { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreateDateTime { get; set; }

        public virtual CategoryDto Category { get; set; }
    }
}
