using System.Numerics;
using System.Timers;

namespace MonoGamePaddleBoard
{
    public class Humanoid
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Timer DurationPlayed { get; set; }
        public Vector2 PositionObserved;

        public Humanoid()
        {
            DurationPlayed = new Timer();
            DurationPlayed.Elapsed += OnTimedEvent;
            DurationPlayed.AutoReset = true;
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {

        }
    }
}
