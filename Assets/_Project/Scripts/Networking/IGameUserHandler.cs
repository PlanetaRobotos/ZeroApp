using _Project.Scripts.Core.Abstract;

namespace _Project.Scripts.Networking
{
    public class IGameUserHandler
    {
    }

    public class GameUserHandler : IGameUserHandler
    {
        public IBoard GetOtherPlayer { get; set; }
    }
}