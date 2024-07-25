using _Project.Scripts.Core.Abstract;

namespace _Project.Scripts.Core
{
    public class PlayerProvider : IPlayerProvider
    {
        public PlayerProfile Player { get; set; }
    }
}