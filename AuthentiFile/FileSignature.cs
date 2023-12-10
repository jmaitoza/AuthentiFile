using Newtonsoft.Json;

namespace AuthentiFile;

public class FileSignature
{
    
    [JsonProperty("description")] public string Description { get; set; }
    [JsonProperty("ext")] public List<string> Ext { get; set; }
    [JsonProperty("fileSig")] public List<byte> FileSig { get; set; }
}