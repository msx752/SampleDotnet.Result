namespace SampleDotnet.Result.Tests.MockApiServer.Models
{
    public class TestDatumDto
    {
        [JsonProperty("value1")]
        public string Value1 { get; set; }

        [JsonProperty("value2")]
        public long Value2 { get; set; }
    }
}