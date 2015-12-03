using Akka.Actor;
using MovieStreaming.Actors;
using MovieStreaming.Messages;
using System;

namespace MovieStreaming
{
    internal class Program
    {
        private static ActorSystem movieStreamingActorSystem;

        private static void Main(string[] args)
        {
            ColorConsole.WriteColorLine("Actor system created", ConsoleColor.Gray);
            movieStreamingActorSystem = ActorSystem.Create("movieStreamingActorSystem");
            

            var userActorProps = Props.Create<UserActor>();
            var userActorRef = movieStreamingActorSystem.ActorOf(userActorProps, "UserActor");

            Console.ReadKey();
            Console.WriteLine("Sending a PlayMovieMessage (Movie 2");
            userActorRef.Tell(new PlayMovieMessage("Movie 2", 42));

            Console.ReadKey();
            Console.WriteLine("Sending another PlayMovieMessage (Movie 3)");
            userActorRef.Tell(new PlayMovieMessage("Movie 3", 42));

            Console.ReadKey();
            Console.WriteLine("Sending a StopMovieMessage");
            userActorRef.Tell(new StopMovieMessage());

            Console.ReadKey();
            Console.WriteLine("Sending another StopMovieMessage");
            userActorRef.Tell(new StopMovieMessage());

            Console.ReadKey();
            movieStreamingActorSystem.Shutdown();
            movieStreamingActorSystem.AwaitTermination();

            Console.WriteLine("Actor system shutdown");
            Console.ReadKey();
        }
    }
}
