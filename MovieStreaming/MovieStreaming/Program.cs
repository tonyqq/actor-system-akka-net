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

            var playabackActorProps = Props.Create<PlaybackActor>();

            var playbackActorRef = movieStreamingActorSystem.ActorOf(playabackActorProps, "PlaybackActor");

            playbackActorRef.Tell(new PlayMovieMessage("Akka.NET: The Movie", 42));
            playbackActorRef.Tell(new PlayMovieMessage("Movie 2", 99));
            playbackActorRef.Tell(new PlayMovieMessage("Movie 3", 77));
            playbackActorRef.Tell(new PlayMovieMessage("Movie 4", 1));

            Console.ReadKey();

            movieStreamingActorSystem.Shutdown();
            movieStreamingActorSystem.AwaitTermination();

            Console.WriteLine("Actor system shutdown");
            Console.ReadKey();
        }
    }
}
