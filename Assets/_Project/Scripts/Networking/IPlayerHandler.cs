using System.Threading;
using _Project.Scripts.Core;
using _Project.Scripts.Core.Abstract;
using Cysharp.Threading.Tasks;
using Fusion;

public interface IPlayerHandler
{
    IBoard Board { get; set; }
    IGameplayMediator GameplayMediator { get; set; }
    UniTask StartGame(SymbolType symbol, NetworkBool isInteractable);
    UniTask EndGame(CancellationToken cancellationToken);
}