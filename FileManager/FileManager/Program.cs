using System;
using System.IO;

partial class Program
{
    
    static void Main(string[] args)
    {
        Console.WriteLine("File manager.");
        Console.WriteLine("Type `help' to find out more about functions");
        Console.WriteLine("You are currently in " + currentPath);
        while (true)
        {
            ReadOrExit(out string userInput);
            CommandQuery(userInput);
        }
    }
}
