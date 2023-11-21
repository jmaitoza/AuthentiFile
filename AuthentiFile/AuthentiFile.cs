using Newtonsoft.Json;

namespace AuthentiFile;

public class AuthentiFile
{
    // link fileSigs.json to the correct build directory when building the project
    static string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "fileSigs.json");
    // deserialize JSON file into a list of FileSignature objects
    private static List<FileSignature> fileSigs = JsonConvert.DeserializeObject<List<FileSignature>>(File.ReadAllText(jsonFilePath)) ?? throw new InvalidOperationException();
    
    
    static void ListMasqueradedFiles(string dirPath)
    {
        // ensure the base directory that dirPath is relative to is the root of the drive not the root of the project
        dirPath = Path.GetFullPath(dirPath);
        
        if (Directory.Exists(dirPath)) // ensure directory exists before listing files
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

        else
        {
            Console.WriteLine($"Cannot list files in {dirPath} because it does not exist.");
        }
        
    }

    private static string GetUserDirectory()
    {
        while (true)
        {
            Console.Write("Please enter the full path of the directory you would like to search or type 'quit' to quit the program: ");
            string userPath = Console.ReadLine();

            if (userPath?.ToLower() == "quit" || userPath?.ToLower() == "q")
            {
                Console.WriteLine("Closing program...");
                Environment.Exit(0);
            }
            
            // check if input is null
            else if (string.IsNullOrEmpty(userPath))
            {
                Console.WriteLine("Please enter a directory.");
            }
            
            // check if user input a file instead of a directory
            else if (File.Exists(userPath))
            {
                Console.WriteLine("Please enter a directory, not a file.");
            }
            
            else
            {
                // Treat as relative path and return the absolute path
                return Path.GetFullPath(userPath);
            }
        }
        
    }

    static void Main(string[] args)
    {
        while (true)
        {
            ListMasqueradedFiles(GetUserDirectory());
        }
    }
}