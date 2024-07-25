using _Project.Scripts.Core.Abstract;

namespace _Project.Scripts.Core
{
    public interface IGameTracker
    {
        void TrackWins(PlayerProfile player);
    }

    public class GameTracker : IGameTracker
    {
        public void TrackWins(PlayerProfile player)
        {
            throw new System.NotImplementedException();
        }
    }
}