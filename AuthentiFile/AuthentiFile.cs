﻿using Newtonsoft.Json;

namespace AuthentiFile;

public class AuthentiFile
{
    // link ListOfFileSignatures.json to the correct build directory when building the project
    static string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ListOfFileSignatures.json");
    private static List<FileSignature> fileSigs;

    static AuthentiFile()
    {
        try
        {
            // deserialize JSON file into a list of FileSignature objects
            string jsonData = File.ReadAllText(jsonFilePath);
            fileSigs = JsonConvert.DeserializeObject<List<FileSignature>>(jsonData);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error initializing AuthentiFile: {ex.Message}");
            throw;
        }
    }
    
    
    static void ListMasqueradedFiles(string dirPath)
    {
        // ensure the base directory that dirPath is relative to is the root of the drive not the root of the project
        dirPath = Path.GetFullPath(dirPath);
        
        if (Directory.Exists(dirPath)) // ensure directory exists before listing files
        {
            int masqueradedFileCount = 0; 
            
            // get all the files in the selected directory
            foreach (var filePath in Directory.GetFiles(dirPath))
            {
                // get file extension listed in file name
                var fileExtInName = Path.GetExtension(filePath).ToLower();
                // fetches the first matching file signature based on the file extension in the name
                var fileSignature = fileSigs.FirstOrDefault(signature => signature.Ext.Contains(fileExtInName));
    
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

                        if (fileExtInName.Equals(".txt"))
                        {
                            // read entire file into a string
                            string text = File.ReadAllText(filePath);
                            
                            /* check if all chars in the file are plain text chars
                               (all plain text chars are between 32 and 126 in ASCII
                               9, 10, and 13 are \t, \n, and \r respectively which are also valid plain text)
                             */
                            bool isPlainText = text.All(c => (c >= 32 && c <= 126) || c == 9 || c == 10 || c == 13);

                            if (isPlainText)
                            {
                                Console.WriteLine($"File Name: {fileName}, File Path: {absoluteFilePath}\nFile contains valid plain text and may be a false positive. You should verify contents manually. Plain text content inside {fileName} below.\nFile contents: {text}");
                            }
                            else
                            {
                                Console.WriteLine($"File Name: {fileName}, File Path: {absoluteFilePath}");
                                masqueradedFileCount++;
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine($"File Name: {fileName}, File Path: {absoluteFilePath}");

                            masqueradedFileCount++;
                        }
                    }
                }
            } 
            // check counter to see if any masqueraded files were found
            if (masqueradedFileCount == 0)
            {
                Console.WriteLine($"No masqueraded files found in the directory {dirPath}.");
            }
        }
        
        else
        {
            // directory does not exist
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
            
            // check if user input is null
            else if (string.IsNullOrEmpty(userPath))
            {
                Console.WriteLine("Please enter a directory.");
            }
            
            // check if user has input a file instead of a directory
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
        // Fun ascii art of program name
        Console.WriteLine(@"
 ______           __    __                     __        ____         ___             
/\  _  \         /\ \__/\ \                   /\ \__  __/\  _`\   __ /\_ \            
\ \ \L\ \  __  __\ \ ,_\ \ \___      __    ___\ \ ,_\/\_\ \ \L\_\/\_\\//\ \      __   
 \ \  __ \/\ \/\ \\ \ \/\ \  _ `\  /'__`\/' _ `\ \ \/\/\ \ \  _\/\/\ \ \ \ \   /'__`\ 
  \ \ \/\ \ \ \_\ \\ \ \_\ \ \ \ \/\  __//\ \/\ \ \ \_\ \ \ \ \/  \ \ \ \_\ \_/\  __/ 
   \ \_\ \_\ \____/ \ \__\\ \_\ \_\ \____\ \_\ \_\ \__\\ \_\ \_\   \ \_\/\____\ \____\
    \/_/\/_/\/___/   \/__/ \/_/\/_/\/____/\/_/\/_/\/__/ \/_/\/_/    \/_/\/____/\/____/
");
        while (true)
        {
            ListMasqueradedFiles(GetUserDirectory());
        }
    }
}