using Newtonsoft.Json;

namespace Application.DTOs;

public class KeyToExchange
{
    [JsonProperty("base_code")]
    public string BaseCode { get; set; }
    [JsonProperty("target_code")]
    public string TargetCode { get; set; }
    [JsonProperty("conversion_rate")]
    public double ConversionRate { get; set; }
    [JsonProperty("conversion_result")]
    public double ConversionResult { get; set; }
}