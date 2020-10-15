using System;


partial class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Добро пожаловать в \"Файловый менеджер\".");
        while (true)
        {
            string userInput = StartMenu();
            if (userInput == "help")
            {
                HelpText();
            }
            if(userInput == "1")
            {
                WriteAllDrives();
            }
            if(userInput == "7")
            {
                MoveFile();
            }
        }
    }
}
