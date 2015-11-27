namespace MovieStreaming.Actors
{
    using System;
    using System.Threading.Tasks;

    using Akka.Actor;

    using MovieStreaming.Messages;

    public class UserActor : ReceiveActor
    {
        private string _currentlyWatching;

        public UserActor()
        {
            Console.WriteLine("Creating a UserActor");

            Receive<PlayMovieMessage>(mes => HandlePlayMovieMessage(mes));
            Receive<StopMovieMessage>(mes => HandlePlayMovieMessage());
        }

        private void HandlePlayMovieMessage()
        {
            if (_currentlyWatching == null)
            {
                ColorConsole.WriteRedLine("Error: cannot stop if nothing is playing.");
            }
            else
            {
                StopPlayingCurrentMovie();
            }
        }

        private void StopPlayingCurrentMovie()
        {
            ColorConsole.WriteYellowLine("User has stopped watching " + _currentlyWatching);
            _currentlyWatching = null;
        }

        private void HandlePlayMovieMessage(PlayMovieMessage mes)
        {
            if (_currentlyWatching != null)
            {
                ColorConsole.WriteRedLine("Error: cannot start playing another movie before stoping existing one.");
            }
            else
            {
                StartPlayingMoview(mes.MovieTitle);
                ColorConsole.WriteYellowLine("User is currently watching " + _currentlyWatching);
            }
        }

        private void StartPlayingMoview(string movieTitle)
        {
            _currentlyWatching = movieTitle;
        }

        protected override void PreStart()
        {
            ColorConsole.WriteGreenLine("UserActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteGreenLine("UserActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteGreenLine("UserActor PreRestart because: " + reason);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteGreenLine("UserActor PostRestart because: " + reason);
            base.PostRestart(reason);
        }
    }
}