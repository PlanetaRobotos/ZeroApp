using _Project.Models;
using _Project.Models.Boards;

namespace _Project.Core
{
    public interface IBoard
    {
        SymbolType[,] Grid { get; }
        bool IsInteractive { get; }
        void PlaceSymbol(int row, int column, SymbolType symbol, out ResultType result);
        bool IsCellEmpty(int row, int column);
        bool IsFull();
        void Reset();
        void MakeMove(BoardCell cell);
        void SetInteract(bool isInteractable);
        void Initialize(BoardWidgetData boardWidgetData, IGameplayMediator gameplayMediator);
    }
}