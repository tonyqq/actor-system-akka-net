using Akka.Actor;
using MovieStreaming.Actors;
using MovieStreaming.Messages;
using System;

namespace MovieStreaming
{
    using System.Threading;

    internal class Program
    {
        private static ActorSystem movieStreamingActorSystem;

        private static void Main(string[] args)
        {
            ColorConsole.WriteColorLine("Creating MovieStreamingActorSystem", ConsoleColor.Gray);
            movieStreamingActorSystem = ActorSystem.Create("movieStreamingActorSystem");

            ColorConsole.WriteColorLine("Creating actor supervisory hierarchy", ConsoleColor.Gray);
            movieStreamingActorSystem.ActorOf(Props.Create<PlaybackActor>(), "Playback");

            do
            {
                ShortPause();
                Console.WriteLine();
                ColorConsole.WriteColorLine("enter a command and hit enter", ConsoleColor.Gray);

                var command = Console.ReadLine();

                if (command.StartsWith("play"))
                {
                    var userId = int.Parse(command.Split(',')[1]);
                    var movieTitle = command.Split(',')[2];

                    var mes = new PlayMovieMessage(movieTitle, userId);
                    movieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(mes);
                }

                if (command.StartsWith("stop"))
                {
                    var userId = int.Parse(command.Split(',')[1]);
                    var mes = new StopMovieMessage(userId);
                    movieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(mes);
                }

                if (command == "exit")
                {
                    movieStreamingActorSystem.Shutdown();
                    movieStreamingActorSystem.AwaitTermination();
                    ColorConsole.WriteColorLine("Actor system shutdown", ConsoleColor.Gray);
                    Console.ReadKey();
                    Environment.Exit(1);
                }
            }
            while (true);
        }

        private static void ShortPause()
        {
            Thread.Sleep(200);
        }
    }
}
