using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PA.ApplicationCore.Domain
{
    public class Postes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty("postesId")]
        public int PostesId { get; set; }
        [StringLength(50)]
        [JsonProperty("ref")]
        public string? Ref { get; set; } // Name or Identifier for the workstation
        [DataType(DataType.Date)]
        [JsonProperty("derniereMaintenance")]
        public DateTime? derniereMaintenance { get; set; }
        [RegularExpression(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$")]
        [JsonProperty("ipAddress")]
        public string? IpAddress { get; set; }
        [JsonProperty("MacAddress")]
        public string? MacAddress { get; set; }
        [JsonProperty("CpuUsageMean")]
        public float? CpuUsageMean { get; set; }
        [JsonProperty("CpuUsageMedian")]
        public float? CpuUsageMedian { get; set; }
        [JsonProperty("CpuUsagePeak")]
        public float? CpuUsagePeak { get; set; }

        [JsonProperty("MemoryUsageMean")]
        public float? MemoryUsageMean { get; set; }
        [JsonProperty("MemoryUsageMedian")]
        public float? MemoryUsageMedian { get; set; }
        [JsonProperty("MemoryUsagePeak")]
        public float? MemoryUsagePeak { get; set; }

        // Foreign Key
        [JsonProperty("sallesId")]
        public int SallesId { get; set; }
        [JsonProperty("statusId")]
        public int StatusId { get; set; }
        [JsonProperty("typeId")]
        public int TypeId { get; set; }
    }
}
