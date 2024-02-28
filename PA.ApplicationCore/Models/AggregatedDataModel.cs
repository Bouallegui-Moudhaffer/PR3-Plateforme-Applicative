using Newtonsoft.Json;

namespace PA.ApplicationCore.Models
{
    public class AggregatedDataModel
    {
        [JsonProperty("CpuUsageMean")]
        public float CpuUsageMean { get; set; }
        [JsonProperty("CpuUsageMedian")]
        public float CpuUsageMedian { get; set; }
        [JsonProperty("CpuUsagePeak")]
        public float CpuUsagePeak { get; set; }

        [JsonProperty("MemoryUsageMean")]
        public float MemoryUsageMean { get; set; }
        [JsonProperty("MemoryUsageMedian")]
        public float MemoryUsageMedian { get; set; }
        [JsonProperty("MemoryUsagePeak")]
        public float MemoryUsagePeak { get; set; }
    }

}
