using _Project.Scripts.Core.Abstract;

namespace _Project.Scripts.Core
{
    public class GameRules : IGameRules
    {
        public bool CheckWin(IBoard board, PlayerProfile player)
        {
            // Implement win checking logic
            // Example: Check rows, columns, and diagonals for three in a row
            return false;
        }

        public bool CheckDraw(IBoard board)
        {
            return board.IsFull() && !CheckWin(board, null);
        }
    }
}