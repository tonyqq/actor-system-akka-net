namespace MovieStreaming
{
    using System;

    public class ColorConsole
    {
        public static void WriteGreenLine(string message)
        {
            var beforeColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = beforeColor;
        }

        public static void WriteYellowLine(string message)
        {
            var beforeColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ForegroundColor = beforeColor;
        }

        public static void WriteRedLine(string message)
        {
            var beforeColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = beforeColor;
        }
    }
}