using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AS.Oil.Presentation.WebApi.Models
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {
            Storages = new HashSet<StorageViewModel>();
        }

        [Required]
        [JsonProperty("Id")]
        public int? Id { get; set; }

        [Required]
        [JsonProperty("Name")]
        [MinLength(5)]
        [MaxLength(512)]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<StorageViewModel> Storages { get; set; }
    }
}
