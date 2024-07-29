using _Project.Networking;
using Cysharp.Threading.Tasks;

namespace _Project.Core
{
    public interface IGameplayMediator
    {
        NetworkPlayerHandler GetOtherPlayer { get; }
        UniTask StartGame();
        void ExitSession();
        void TryMakeMove(int x, int y);
    }
}