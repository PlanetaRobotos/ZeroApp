namespace _Project.Scripts.Core.Abstract
{
    public interface IBoard
    {
        // SymbolType[,] Grid { get; }
        bool PlaceSymbol(int row, int column, SymbolType symbol);
        bool IsCellEmpty(int row, int column);
        bool IsFull();
        void Reset();
    }
}