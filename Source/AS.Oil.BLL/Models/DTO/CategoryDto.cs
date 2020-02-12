using System.Collections.Generic;

namespace AS.Oil.BLL.Models.DTO
{
    public class CategoryDto
    {
        public CategoryDto()
        {
            Storages = new HashSet<StorageDto>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<StorageDto> Storages { get; set; }
    }
}
