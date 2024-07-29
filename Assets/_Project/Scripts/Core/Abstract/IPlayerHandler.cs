using System.Threading;
using _Project.Models;
using Cysharp.Threading.Tasks;
using Fusion;

namespace _Project.Core
{
    public interface IPlayerHandler
    {
        IBoard Board { get; set; }
        IGameplayMediator GameplayMediator { get; set; }
        UniTask StartGame(SymbolType symbol, NetworkBool isInteractable);
        UniTask EndGame(CancellationToken cancellationToken);
    }
}