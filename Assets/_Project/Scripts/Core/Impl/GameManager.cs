using _Project.Scripts.Core.Abstract;

namespace _Project.Scripts.Core
{
    public class GameManager
    {
        private IBoard _board;
        private IGameRules _gameRules;
        private PlayerProfile[] _players;
        
        //TODO Remove this field
        private int _currentPlayerIndex;

        public GameManager(IBoard board, IGameRules gameRules, PlayerProfile[] players)
        {
            _board = board;
            _gameRules = gameRules;
            _players = players;
            _currentPlayerIndex = 0;
        }

        public void StartGame()
        {
            _board.Reset();
        }

        public void EndGame()
        {
            // Implement end game logic
        }

        public void ResetGame()
        {
            _board.Reset();
            _currentPlayerIndex = 0;
        }

        public void MakeMove(PlayerProfile player, int row, int column)
        {
            // if (_board.PlaceSymbol(row, column, player.Symbol))
            // {
            //     // Check for win or draw
            //     
            //     if (CheckWinner() != null)
            //     {
            //         TrackWins(player);
            //     }
            //     else if (IsDraw())
            //     {
            //         // Implement draw logic
            //     }
            //     else
            //     {
            //         _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Length;
            //     }
            // }
        }

        public void TrackWins(PlayerProfile player)
        {
            player.Wins++;
        }
    }
}