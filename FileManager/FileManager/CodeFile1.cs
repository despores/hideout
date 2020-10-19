using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
//using System.Windows;
partial class Program
{
    private static string currentPath = Directory.GetCurrentDirectory();
    private static string[] encodingList = new string[] { "UTF-8", };
    /// <summary>
    /// Функция для вывода стартового меню в консоль.
    /// </summary>
    /// <returns>Возвращает строковое значение выбранной пользователем комманды.</returns>
    private static string StartMenu()
    {
        Console.WriteLine("Введите число от 1 до 11 для соответствующей команды " +
            "или help для подробной информации о командах.");
        ReadOrExit(out string userInput);
        // Проверка корректности ввода.
        while (userInput != "help" && !CheckCorrectIntInput(userInput, 1, 11, out int num))
        {
            // Запрос на повторный ввод.
            Console.WriteLine("Пожалуйста, введите корректную команду.");
            ReadOrExit(out userInput);
        }
        return userInput;
    }

    /// <summary>
    /// Проверка корректности введенного пользователем числа.
    /// </summary>
    /// <param name="input">Ввод пользователя</param>
    /// <param name="startingPos">Нижняя граница числа.</param>
    /// <param name="endingPos">Вверхняя граница числа.</param>
    /// <param name="num">Выходной параметр - само число.</param>
    /// <returns>True, если введено корректное число, False в противном случае.</returns>
    private static bool CheckCorrectIntInput(string input, int startingPos, int endingPos, out int num)
    {
        return (int.TryParse(input, out num) && num >= startingPos && num <= endingPos);
    }

    /// <summary>
    /// Функция для вывода в консоль подробной информации о доступных командах.
    /// </summary>
    private static void HelpText()
    {
        try
        {
            Console.WriteLine(File.ReadAllText("Help.txt"));
        }
        catch
        {
            FailureMessage("Could not get access to Help.txt");
        }
    }

    /// <summary>
    /// Функция 
    /// </summary>
    private static void DriveChoosing()
    {
        try
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            Console.WriteLine("Available drives:");
            for (int i = 0; i < drives.Length; i++)
            {
                Console.WriteLine($"{i + 1}) {drives[i].Name}");
            }
            Console.WriteLine("Choose available drive, number from 1 to {0}:", drives.Length);
            ReadOrExit(out string input);
            if (!CheckCorrectIntInput(input, 1, drives.Length, out int intInput))
            {
                FailureMessage("Incorrect drive number.");
                return;
            }
            currentPath = Path.GetFullPath(drives[intInput - 1].Name);
            Console.WriteLine("Drive chosen succesfully.");
        }
        catch
        {
            FailureMessage("Drive acsess error.");
        }
    }

    private static void DirInformation()
    {
        try
        {
            MakeCorrectPath(ref currentPath);
            DirectoryInfo currentDir = new DirectoryInfo(currentPath);
            FileSystemInfo[] infos = currentDir.GetFileSystemInfos();
            foreach (FileSystemInfo file in infos)
            {
                Console.WriteLine(" " + file.Name);
            }
            if(infos.Length == 0)
            {
                Console.WriteLine("Directory is empty.");
            }
        }
        catch
        {
            FailureMessage("Could not get access to directory info.");
        }
    }

    private static void ReadOrExit(out string input)
    {
        Console.Write(">>>");
        input = Console.ReadLine().Trim();
        if (input == "exit")
        {
            Console.WriteLine("До связи.");
            Environment.Exit(0);
        }
    }

    private static void MakeCorrectPath(ref string path)
    {
        if (!path.EndsWith(Path.DirectorySeparatorChar))
        {
            path += Path.DirectorySeparatorChar;
        }
    }

    private static void MoveFile()
    {
        try
        {
            Console.WriteLine("Please write the file name");
            ReadOrExit(out string userInput);
            MakeCorrectPath(ref currentPath);
            string filename = currentPath + userInput;
            if (!File.Exists(filename))
            {
                FailureMessage("File manager: file not found.");
                return;
            }
            Console.WriteLine("Input directory path you want to move file into.");
            ReadOrExit(out string pathToMove);
            MakeCorrectPath(ref pathToMove);
            if (!Directory.Exists(pathToMove))
            {
                FailureMessage("File manager: path not found.");
                return;
            }
            pathToMove = pathToMove + userInput;
            if (File.Exists(pathToMove) && !OverwriteFile())
            {
                Console.WriteLine("Command abort.");
                return;
            }
            File.Move(filename, pathToMove, true);
            Console.WriteLine("File was moved succesfully.");
        }
        catch
        {
            FailureMessage("File cannot be moved.");
        }
    }

    private static void CopyFile()
    {
        try
        {
            Console.WriteLine("Please write the file name");
            ReadOrExit(out string userInput);
            MakeCorrectPath(ref currentPath);
            string fileName = currentPath + userInput;
            if (!File.Exists(fileName))
            {
                FailureMessage("File manager: file not found.");
                return;
            }
            Console.WriteLine("Write directory path you want to copy file into.");
            ReadOrExit(out string pathToMove);
            MakeCorrectPath(ref pathToMove);
            if (!Directory.Exists(pathToMove))
            {
                FailureMessage("File manager: path not found.");
                return;
            }

            pathToMove = pathToMove + userInput;
            if (File.Exists(pathToMove) && !OverwriteFile())
            {
                Console.WriteLine("Command abort.");
                return;
            }
            File.Copy(fileName, pathToMove, true);
            Console.WriteLine("File was copied succesfully.");
        }
        catch
        {
            FailureMessage("File cannot be moved.");
        }
    }

    private static bool OverwriteFile()
    {
        Console.WriteLine("File with same name already exists in chosen directory.");
        return DoubleCheck("overwrite");

    }

    private static bool DoubleCheck(string command)
    {
        Console.WriteLine($"Are you sure you want to {command} it? Write yes to continue.");
        ReadOrExit(out string userInput);
        if (userInput.ToLower() == "yes")
        {
            return true;
        }
        return false;
    }

    private static void DeleteFile()
    {
        Console.WriteLine("Please write the file name.");
        try
        {
            string fileName = MakePath(false);
            if (!File.Exists(fileName))
            {
                FailureMessage("File manager: file not found.");
                return;
            }
            if (!DoubleCheck("delete"))
            {
                Console.WriteLine("Command abort.");
                return;
            }
            File.Delete(fileName);
            Console.WriteLine("File deleted successfully.");
        }
        catch
        {
            FailureMessage("Could not delete the file.");
        }
    }

    private static void FailureMessage(string message)
    {
        Console.WriteLine(message + " Failure.");
    }

    private static void ChangeDirectory()
    {
        Console.WriteLine("Please write the directory name.");
        
        try
        {
            string dirName = MakePath(false);
            if (!Directory.Exists(dirName))
            {
                FailureMessage("File manager: directory not found.");
                return;
            }
            DirectoryInfo currentDir = new DirectoryInfo(currentPath);
            currentPath = dirName;
            Console.WriteLine("Operation successful.");
        }
        catch
        {
            FailureMessage("Could not get access to chosen directory.");
        }
    }

    private static void PreviousDirectory()
    {
        try
        {
            string previousDir = Path.GetFullPath(".." + Path.DirectorySeparatorChar, currentPath);
            currentPath = previousDir;
            Console.WriteLine("Operation successful.");
        }
        catch
        {
            FailureMessage("Could not return to previous directory");
        }
    }

    private static void MakeTxtFile()
    {
        Console.WriteLine("Choose the file name, without .txt .");
        try
        {
            string filePath = MakePath(true);
            if (File.Exists(filePath) && !OverwriteFile())
            {
                Console.WriteLine("Command abort.");
                return;
            }
            File.WriteAllText(filePath, "check");
            Console.WriteLine("Choose amount of lines you want to write, up to 10000.");
            ReadOrExit(out string input);
            if(!CheckCorrectIntInput(input, 0, 10000, out int stringNum))
            {
                FailureMessage("Incorrect number of lines.");
                return;
            }
            string[] text = new string[stringNum];
            if (stringNum > 0)
            {
                Console.WriteLine("Write text.");
                for (int i = 0; i < text.Length; i++)
                {
                    Console.Write(">>>");
                    text[i] = Console.ReadLine();
                }
            }
            File.WriteAllLines(filePath, text);
            Console.WriteLine("Txt file was made successfully.");
        }
        catch
        {
            FailureMessage("Could not create .txt file");
        }
    }

    private static void ReadTxtFile()
    {
        try
        {
            Console.WriteLine("Please write the file name.");
            string filePath = MakePath(false);
            if (!File.Exists(filePath))
            {
                FailureMessage("File manager: file not found.");
                return;
            }
            Console.WriteLine(File.ReadAllText(filePath));
            Console.WriteLine("Txt file was written successfully.");
        }
        catch
        {
            FailureMessage("Could not read from file.");
        }
    }

    private static void ChangePath()
    {
        try
        {
            Console.WriteLine("Write the path you want to go to.");
            ReadOrExit(out string userInput);
            DirectoryInfo dir = new DirectoryInfo(userInput);
            currentPath = dir.FullName;
            Console.WriteLine("Path was switched successfully.");
        }
        catch
        {
            FailureMessage("Could not get access to chosen path.");
        }
    }

    private static string MakePath(bool addTxt)
    {
        ReadOrExit(out string fileName);
        MakeCorrectPath(ref currentPath);
        string filePath = currentPath + fileName;
        if(addTxt)
        {
            filePath += ".txt";
        }
        return filePath;
    }
    private static void ConcatenateTxtFiles()
    {
        Console.WriteLine("Choose the amount of .txt files you want to concatenate");
        ReadOrExit(out string userInput);
        if(!CheckCorrectIntInput(userInput, 1, int.MaxValue, out int filesAmount))
        {
            FailureMessage("Incorrect input.");
            return;
        }
        Console.WriteLine("Choose the file name, without .txt .");
        try
        {
            string filePath = MakePath(true);
            if (File.Exists(filePath) && !OverwriteFile())
            {
                Console.WriteLine("Command abort.");
                return;
            }
            File.WriteAllText(filePath, "check");

        }
        catch (ArgumentException)
        {
            FailureMessage("File name contains forbidden characters.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            FailureMessage("Could not create .txt file");
        }
    }

    private static void WriteTextToFile(int filesAmount, string path)
    {
        for (int i = 0; i < filesAmount; i++)
        {
            Console.WriteLine("Write");
        }
    }

    private static void CommandQuery(string userInput)
    {
        switch (userInput)
        {
            case "help":
                HelpText();
                break;
            case "choosedrive":
                DriveChoosing();
                break;
            case "showfiles":
                DirInformation();
                break;
            case "movefile":
                MoveFile();
                break;
            case "deletefile":
                DeleteFile();
                break;
            case "copyfile":
                CopyFile();
                break;
            case "chdir":
                ChangeDirectory();
                break;
            case "back":
                PreviousDirectory();
                break;
            case "currpath":
                Console.WriteLine(currentPath);
                break;
            case "maketxtfile":
                MakeTxtFile();
                break;
            case "readtxtfile":
                ReadTxtFile();
                break;
            case "jumpto":
                ChangePath();
                break;
            case "concatenate":
                ConcatenateTxtFiles();
                break;
            default:
                Console.WriteLine("File manager: command not found.");
                break;
        }
    }
}