using System;


partial class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("File manager.");
        Console.WriteLine("Type `help' to find out more about functions");
        while (true)
        {
            Console.Write(">>>");
            ReadOrExit(out string userInput);
            CommandQuery(userInput);
        }
    }
}
