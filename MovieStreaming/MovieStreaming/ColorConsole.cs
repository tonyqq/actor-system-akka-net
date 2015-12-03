namespace MovieStreaming
{
    using System;

    public class ColorConsole
    {
        public static void WriteColorLine(string message, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(message);
        }
    }
}