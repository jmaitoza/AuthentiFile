using Newtonsoft.Json;

namespace AuthentiFile;

public class FileSignature
{
    // public string Description { get; set; }
    // public string Ext { get; set; }
    // public List<byte> FileSig { get; set; }
    
    [JsonProperty("description")] public string Description { get; set; }
    [JsonProperty("ext")] public List<string> Ext { get; set; }
    [JsonProperty("fileSig")] public List<byte> FileSig { get; set; }
}