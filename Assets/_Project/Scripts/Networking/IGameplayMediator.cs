using Cysharp.Threading.Tasks;

public interface IGameplayMediator
{
    UniTask StartGame();
    void ExitSession();
    void TryMakeMove(int x, int y);
    NetworkPlayerHandler GetOtherPlayer { get; }
}