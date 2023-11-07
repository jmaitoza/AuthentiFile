using Newtonsoft.Json;
using System.Drawing;

namespace AuthentiFile;

public class AuthentiFile
{
    // deserialize JSON file into a list of FileSignature objects
    private static List<FileSignature> fileSigs = JsonConvert.DeserializeObject<List<FileSignature>>(File.ReadAllText("fileSigs.json")) ?? throw new InvalidOperationException();
    
    
    static void ListMasqueradedFiles(string dirPath)
    {
        // get all the files in the selected directory
        foreach (var filePath in Directory.GetFiles(dirPath))
        {
            // get file extension listed in file name
            var fileExtInName = Path.GetExtension(filePath).ToLower();
            // fetches the first matching file signature based on the file extension in the name
            var fileSignature = fileSigs.FirstOrDefault(fileSigs => fileSigs.Ext == fileExtInName);

            if (fileSignature != null)
            {
                // stream in files from given directory
                using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                var buffer = new byte[fileSignature.FileSig.Count];
                fileStream.Read(buffer, 0, buffer.Count());

                if (!buffer.SequenceEqual(fileSignature.FileSig)) //check if two buffers are equal
                {
                    // if not equal, print out the file's name and its file path
                    string fileName = Path.GetFileName(filePath);
                    string absoluteFilePath = Path.GetFullPath(filePath);
                    Console.WriteLine($"File Name: {fileName}, File Path: {absoluteFilePath}");
                }
            }
        }
    }

    static void GetUserDir()
    {
        var fileContent = string.Empty;
        var filePath = string.Empty;
    }

    static void Main(string[] args)
    {
        
        ListMasqueradedFiles("/Users/joshuamaitoza/RiderProjects/AuthentiFile/testFiles"); //macbook path
        // ListMasqueradedFiles("C:\\Users\\joshm\\RiderProjects\\AuthentiFile\\testFiles"); // windows pc path
    }

}