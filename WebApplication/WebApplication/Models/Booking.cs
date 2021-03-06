using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplication.Models
{
    public class Booking
    {
        [Key] 
        [JsonPropertyName("id")] 
        public int Id { get; set; }

        [Required]
        [JsonPropertyName("dateFrom")]
        public DateTime DateFrom { get; set; }

        [Required]
        [JsonPropertyName("dateTo")]
        public DateTime DateTo { get; set; }

        [Required]
        [JsonPropertyName("BookedQuantity")]
        public int BookedQuantity { get; set; }
        
        [Required]
        [ForeignKey("Resource")]
        [JsonPropertyName("resourceId")]
        public int ResourceId { get; set; }
    }
}