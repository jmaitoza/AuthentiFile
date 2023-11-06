namespace AuthentiFile;

public class FileSignature
{
    public string Extension { get; set; }
    public List<MagicNumber> MagicNumber{ get; set; }
}