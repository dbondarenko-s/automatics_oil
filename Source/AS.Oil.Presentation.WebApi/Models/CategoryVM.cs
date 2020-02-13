using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AS.Oil.Presentation.WebApi.Models
{
    public class CategoryVM
    {
        [Required]
        [JsonProperty("Name")]
        [MinLength(5)]
        [MaxLength(512)]
        public string Name { get; set; }
    }
}
