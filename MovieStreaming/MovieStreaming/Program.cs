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
            movieStreamingActorSystem = ActorSystem.Create("movieStreamingActorSystem");
            Console.WriteLine("Actor system created");

            var userActorProps = Props.Create<UserActor>();
            var userActorRef = movieStreamingActorSystem.ActorOf(userActorProps, "UserActor");

            Console.ReadKey();
            Console.WriteLine("Sending a PlayMovieMessage (Movie 2");
            userActorRef.Tell(new PlayMovieMessage("Movie 1", 42));

            Console.ReadKey();
            Console.WriteLine("Sending a PlayMovieMessage (Movie 3)");
            userActorRef.Tell(new PlayMovieMessage("Movie 2", 42));

            Console.ReadKey();
            Console.WriteLine("Sending a StopMovieMessage");
            userActorRef.Tell(new StopMovieMessage());

            Console.ReadKey();
            Console.WriteLine("Sending another StopMovieMessage");
            userActorRef.Tell(new StopMovieMessage());

            //var playabackActorProps = Props.Create<PlaybackActor>();
            //var playbackActorRef = movieStreamingActorSystem.ActorOf(playabackActorProps, "PlaybackActor");
            //playbackActorRef.Tell(new PlayMovieMessage("Akka.NET: The Movie", 42));
            //playbackActorRef.Tell(new PlayMovieMessage("Movie 2", 99));
            //playbackActorRef.Tell(new PlayMovieMessage("Movie 3", 77));
            //playbackActorRef.Tell(new PlayMovieMessage("Movie 4", 1));
            //playbackActorRef.Tell(PoisonPill.Instance); // calls playbackActor PostStop eventhough Shutdown was not called

            Console.ReadKey();
            movieStreamingActorSystem.Shutdown();
            movieStreamingActorSystem.AwaitTermination();

            Console.WriteLine("Actor system shutdown");
            Console.ReadKey();
        }
    }
}
