using System;
using System.IO;
//using System.Windows;
partial class Program
{
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
        DriveInfo[] drives = DriveInfo.GetDrives();
        Console.WriteLine("Доступные диски:");
        for (int i = 0; i < drives.Length; i++)
        {
            Console.WriteLine($"{i+1}) {drives[i].Name}");
        }
        Console.WriteLine("Введите число, соответствующее нужному вам диску.");

    }

    private static void DirInformation()
    {
        ReadOrExit(out string path);
        DirectoryInfo currentDir = new DirectoryInfo(path);
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
    private static void MoveFile()
    {
        Console.WriteLine("Введите путь к нужному файлу.");
        ReadOrExit(out string path); 
        while(!File.Exists(path))
        {
            Console.WriteLine("Введите корректный путь к файлу!");
            ReadOrExit(out path);
        }
        FileInfo fileToMove = new FileInfo(path);
        Console.WriteLine(fileToMove.Name);
        //как блять ввести правильный путь кисленько нахуй ненавижу прогу
        // ну да меня же унизили на контесте
        Console.WriteLine("Введите путь к директории, куда вы хотите переместить файл.");
        ReadOrExit(out string pathToMove);
        while (!File.Exists(pathToMove))
        {
            Console.WriteLine("Введите корректный путь к файлу!");
            ReadOrExit(out pathToMove);
        }
        pathToMove += @"\" + fileToMove.Name;
        File.Move(path, pathToMove);
        // ну если честно то проверяющим поебать :/
        // как нормально двигать файл

    }
}