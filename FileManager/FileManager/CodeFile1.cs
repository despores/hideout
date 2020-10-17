using System;
using System.IO;
//using System.Windows;
partial class Program
{
    private static string currPath = Directory.GetCurrentDirectory();

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
        while(userInput != "help" && !CheckCorrectIntInput(userInput, 1, 11, out int num))
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
        Console.WriteLine(File.ReadAllText("Help.txt"));
    }

    /// <summary>
    /// Функция 
    /// </summary>
    private static void WriteAllDrives()
    {
        try
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            Console.WriteLine("Available drives:");
            for (int i = 0; i < drives.Length; i++)
            {
                Console.WriteLine($"{i + 1}) {drives[i].Name}");
            }
            Console.Write("Choose available drive, number from 1 to {0}:", drives.Length);
            ReadOrExit(out string input);
            CheckCorrectIntInput(input, 1, drives.Length, out int intInput);
            currPath = Path.GetFullPath(drives[intInput-1].Name);
        }
        catch
        {
            Console.WriteLine("Drive acsess error!");
        }
    }

    private static void DirInformation()
    {
        DirectoryInfo currentDir = new DirectoryInfo(currPath);
        FileSystemInfo[] infos = currentDir.GetFileSystemInfos();
        foreach (FileSystemInfo file in infos)
        {
            Console.WriteLine(file.Name);
        }
    }


    private static void ReadOrExit(out string input)
    {
        input = Console.ReadLine();
        if(input == "exit")
        {
            Console.WriteLine("До связи.");
            Environment.Exit(0);
        }
    }
    private static void MoveFile(string[] input)
    {
        if(input.Length !=3)
        {

        } 
        while(!File.Exists(path))
        {
            Console.WriteLine("Введите корректный путь к файлу!");
            ReadOrExit(out path);
        }
        FileInfo fileToMove = new FileInfo(path);
        Console.WriteLine(fileToMove.Name);
        Console.WriteLine("Введите путь к директории, куда вы хотите переместить файл.");
        ReadOrExit(out string pathToMove);
        while (!File.Exists(pathToMove))
        {
            Console.WriteLine("Введите корректный путь к файлу!");
            ReadOrExit(out pathToMove);
        }
        pathToMove += @"\" + fileToMove.Name;
        File.Move(path, pathToMove);
    }

    private static void CommandQuery(string userInput)
    {
        string[] input = userInput.Split();
        switch(input[0])
        {
            case "help":
                HelpText();
                break;
            case "driveslist":
                WriteAllDrives();
                Console.WriteLine(currPath);
                break;
            case "showfiles":
                DirInformation();
                break;
            case "movefile":
                MoveFile(input);
                break;
            default:
                Console.WriteLine("File manager: command not found.");
                break;
        }
    }
}