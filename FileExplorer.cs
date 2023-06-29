using System;
using System.IO;

public class FileExplorer
{
    private string currentDirectory;

    public FileExplorer()
    {
        currentDirectory = Environment.CurrentDirectory;
    }

    public void Run()
    {
        Console.WriteLine("Welcome to File Explorer!");

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Current Directory: " + currentDirectory);
            Console.WriteLine("Commands: 'ls' - List Files and Folders, 'cd <folder>' - Change Directory, 'back' - Go Back, 'exit' - Exit");

            Console.Write("Enter a command: ");
            string command = Console.ReadLine();

            if (command.StartsWith("ls"))
            {
                ListFilesAndFolders();
            }
            else if (command.StartsWith("cd"))
            {
                string folderName = command.Substring(3).Trim();

                if (folderName == "..")
                {
                    NavigateUp();
                }
                else
                {
                    NavigateToFolder(folderName);
                }
            }
            else if (command == "back")
            {
                NavigateBack();
            }
            else if (command == "exit")
            {
                Console.WriteLine("Thank you for using File Explorer. Goodbye!");
                return;
            }
            else
            {
                Console.WriteLine("Invalid command. Please try again.");
            }
        }
    }

    private void ListFilesAndFolders()
    {
        try
        {
            string[] directories = Directory.GetDirectories(currentDirectory);
            string[] files = Directory.GetFiles(currentDirectory);

            Console.WriteLine("Folders:");
            foreach (string directory in directories)
            {
                Console.WriteLine(Path.GetFileName(directory));
            }

            Console.WriteLine("Files:");
            foreach (string file in files)
            {
                Console.WriteLine(Path.GetFileName(file));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }

    private void NavigateToFolder(string folderName)
    {
        try
        {
            string newDirectory = Path.Combine(currentDirectory, folderName);

            if (Directory.Exists(newDirectory))
            {
                currentDirectory = newDirectory;
            }
            else
            {
                Console.WriteLine("Folder not found.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }

    private void NavigateUp()
    {
        try
        {
            DirectoryInfo parentDirectory = Directory.GetParent(currentDirectory);

            if (parentDirectory != null)
            {
                currentDirectory = parentDirectory.FullName;
            }
            else
            {
                Console.WriteLine("Already at the root directory.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }

    private void NavigateBack()
    {
        try
        {
            string previousDirectory = Environment.CurrentDirectory;

            if (previousDirectory != currentDirectory)
            {
                currentDirectory = previousDirectory;
            }
            else
            {
                Console.WriteLine("Cannot go back. No previous directory.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }
}