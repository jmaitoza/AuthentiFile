using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using AuthentiFile;
using Newtonsoft.Json;

class Program
{
    // Mapping file extensions to their corresponding magic numbers
    // This is getting replaced with JSON implementation
    // private static Dictionary<string, List<byte[]>> fileSigs = new Dictionary<string, List<byte[]>>
    // {
    //     { ".jpg", new List<byte[]> { new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 } } },
    //     { ".png", new List<byte[]> { new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } } },
    //     { ".docx", new List<byte[]> { new byte[] { 0x50, 0x4B, 0x03, 0x04 } } },
    //     { ".exe", new List<byte[]> { new byte[] { 0x4D, 0x5A } } },
    //     { ".zip", new List<byte[]> { new byte[] { 0x50, 0x4B, 0x03, 0x04 } } },
    //     { ".gif", new List<byte[]> { new byte[] { 0x47, 0x49, 0x46, 0x38 } } },
    //     { ".bmp", new List<byte[]> { new byte[] { 0x42, 0x4D } } },
    //     { ".msi", new List<byte[]> { new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 } } },
    //     {".pdf", new List<byte[]> {new byte[] {0x25, 0x50, 0x44, 0x46, 0x2D}}}
    //     
    // };
    
    // deserialize JSON file into a list of FileSignature objects
    private static List<FileSignature> fileSigs = JsonConvert.DeserializeObject<List<FileSignature>>(File.ReadAllText("fileSigs.json"));
    
    static void ListMasqueradedFiles(string dirPath)
    {
        // get all the files in the selected directory
        
        
        // foreach (var filePath in Directory.GetFiles(dirPath))
        // {
        //     // get the file extension in the name of the file (not the actual file extension)
        //     var fileExtInName = Path.GetExtension(filePath).ToLower();
        //
        //     // if the file extension is in the dictionary
        //     if (fileSigs.ContainsKey(fileExtInName))
        //     {
        //         var fileMagicNumbers = fileSigs[fileExtInName];
        //
        //
        //         using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        //
        //         foreach (var magicNumber in fileMagicNumbers)
        //         {
        //             var buffer = new byte[magicNumber.Length];
        //             fileStream.Read(buffer, 0, buffer.Length);
        //
        //             if (!buffer.SequenceEqual(magicNumber)) // check if two buffers are equal
        //             {
        //                 // if not equal, print out the files name and its file path
        //                 string fileName = Path.GetFileName(filePath);
        //                 string absoluteFilePath = Path.GetFullPath(filePath);
        //                 Console.WriteLine($"File name: {fileName}, File path: {absoluteFilePath})");
        //                 break;
        //             }
        //         }
        //     }
        // }
    }

    static void Main(string[] args)
    {
        ListMasqueradedFiles("/Users/joshuamaitoza/RiderProjects/Forensics/testFiles"); //macbook path
        // ListMasqueradedFiles("C:\\Users\\joshm\\RiderProjects\\AuthentiFile\\testFiles"); // windows pc path
    }

}