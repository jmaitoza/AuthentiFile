namespace AuthentiFile;

public class FileSignature
{
    public string Description { get; set; }
    public string Ext { get; set; }
    public List<byte> FileSig { get; set; }
}