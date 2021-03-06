using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication.Models
{
    public class Resource
    {
        [Key] 
        [JsonPropertyName(("id"))] 
        public int Id { get; set; }
        
        [Required] 
        [JsonPropertyName("name")] 
        public string Name { get; set; }

        [Required]
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonIgnore] public List<Booking> Bookings { get; set; }
    }
}