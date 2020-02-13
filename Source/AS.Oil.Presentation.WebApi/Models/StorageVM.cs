using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AS.Oil.Presentation.WebApi.Models
{
    public class StorageVM
    {
        [Required]
        [JsonProperty("CategoryId")]
        public int? CategoryId { get; set; }

        [Required]
        [Range(0, 1000)]
        [JsonProperty("MaxVolume")]
        public double? MaxVolume { get; set; }

        [Required]
        [Range(0, 1000)]
        [JsonProperty("MinVolume")]
        public double? MinVolume { get; set; }

        [Required]
        [Range(0, 1000)]
        [JsonProperty("Volume")]
        public double? Volume { get; set; }

        [Required]
        [StringLength(1024)]
        [JsonProperty("Name")]
        public string Name { get; set; }
    }
}
