using System.Collections.Generic;
using _Project.Scripts.Core.Abstract;

namespace _Project.Scripts.Core
{
    public class BoardCheckProvider : IBoardCheckProvider
    {
        private readonly IGameRules _gameRules;
        private readonly IBoard _board;
        private readonly IEnumerable<PlayerProfile> _players;

        public BoardCheckProvider(IGameRules gameRules, IBoard board, IEnumerable<PlayerProfile> players)
        {
            _gameRules = gameRules;
            _board = board;
            _players = players;
        }

        public PlayerProfile CheckWinner()
        {
            foreach (var player in _players)
            {
                if (_gameRules.CheckWin(_board, player))
                {
                    return player;
                }
            }

            return null;
        }

        public bool IsDraw()
        {
            return _gameRules.CheckDraw(_board);
        }
    }
}