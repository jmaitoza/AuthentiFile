# AuthentiFile

This repository was created as a requirement of CIS 442 (Fall 2023) at the University of UMass Dartmouth.  
Authored by Joshua Maitoza

## Table of Contents

- [AuthentiFile Requirements](#authentifile-requirements)
- [AuthentiFile Dependencies](#authentifile-dependencies)
- [Functionality](#functionality)
- [Installation](#installation)
- [Usage](#usage)
- [Tested File Types](#tested-file-types)
- [Known Issues](#known-issues)
- [Demo Video](#demo-video)
  

# AuthentiFile Requirements

This project is built using .NET 7.0. Ensure you have the corresponding framework installed to run the project if you plan to clone the project and run it from source.

# AuthentiFile Dependencies

This project depends on the following NuGet packages:

- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/13.0.3), version 13.0.3

Make sure these packages are installed before attempting to run the project.

## Functionality

AuthentiFile is a command-line tool written in C# that allows the user to scan a directory and output a list of all masqueraded files within that directory. The program will scan the directory (does not scan subdirectories) and compare the file extensions with the hexadecimal signature of the file to determine if the file is masqueraded. If the file is masqueraded, the program will output the name of the file and the directory it is located in to the console.

Example output:

```
File Name: example.jpg, File Path: C:\Users\example\example.jpg
```

## Installation
Download the latest release from the [releases](https://github.com/jmaitoza/AuthentiFile/releases/latest) page.  
Extract the contents of the zip file and run the AuthentiFile.exe file.  
When running the program do not delete any of the files in the same directory as the .exe file nor should you move the .exe file to a different directory. Doing so will cause the program to fail.

**Note**:  
If you are using Windows, you may get a warning from Windows Defender SmartScreen. This is because the application is not signed. To run the application, click "More Info" and then "Run Anyway"

If you are using Mac, you may get a warning that the application is from an unidentified developer. To run the application, right click the application and select "Open". You will then be prompted to confirm that you want to open the application. Click "Open" to run the application.

Unfortunately, I do not have access to a Linux machine to test the application on Linux. If you are using Linux and are able to test the application, please let me know if it works or not. You may be able to run the application using Proton on Linux. I have not tested this myself but it may work.

## Usage

To run the program, double click the AuthentiFile.exe file. A terminal window will open and prompt you to enter a directory to scan. Enter the directory you wish to scan and press enter. The program will then scan the directory and output the results to the console. The program will then prompt you to enter another directory to scan. To exit the program, enter "quit" when prompted for a directory.  

It is important to note that the program will warn the user if they input the file path of a file instead of a directory as AuthentiFile is only able to scan directories. Also AuthentiFile will only scan one directory at a time and does not scan subdirectories. Any directories within the directory being scanned will be ignored.

## Tested File Types

This program has been personally tested with the following file types:

- JPG
- PNG
- PDF
- PPTX
- DOCX
- GIF
- HEIC
- DLL
- MP3
- MP4  
- WAV

This program should work with any file type listed within the [file signatures](https://en.wikipedia.org/wiki/List_of_file_signatures) Wikipedia page so long as it is not offset from the beginning of the file, however, I have not tested every file type listed on that page just the most common ones.


### Known Issues
Does not correctly work with very specific file types such as:
- Older Microsoft Office documents
  - .doc, .xls, .ppt
- MacOS DMG files
  - .dmg

These files are automatically flagged as masqueraded by the program. I believe this is due to how older Microsoft Office documents are saved in some form of compatibility mode in newer versions of Office.

For DMG files, this is because the file signature for DMG files is offset by 512 bytes from the end of the file instead of the beginning of the file. This is not currently accounted for in the program as most files have their signature at the beginning of the file. If given more time, I would like to add support for files that have their signature offset from the beginning of the file.

## Demo Video
Click the image below to view the demo video  
[![https://youtu.be/BbXWhZlDWJE](https://img.youtube.com/vi/BbXWhZlDWJE/0.jpg)](https://youtu.be/BbXWhZlDWJE)

Video Link if embed does not work:
https://youtu.be/BbXWhZlDWJE
