using _Project.Scripts.Core;
using _Project.Scripts.Core.Abstract;
using Logging;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WindowsSystem.Core;

namespace _Project.Scripts.Windows.HUD
{
    public class BoardWindow : BaseWindow<BoardData>
    {
        [SerializeField] private TMP_Text playerTurnText;
        [SerializeField] private TMP_Text resultText;
        [SerializeField] private Button resetButton;

        [SerializeField] private BoardView _boardView;

        private PlayerProfile _currentPlayer;
        
        [Inject] private ICustomLogger _logger;
        
        public override void OnOpen()
        {
            _boardView.Construct(_logger);
            _boardView.Initialize(3, 3);

            // UpdatePlayerTurnText();
            
            Data.OnBoardChanged += OnBoardChanged;
            Data.OnBoardCellChanged += OnBoardCellChanged;
        }

        public override void Close()
        {
            Data.OnBoardChanged -= OnBoardChanged;
            Data.OnBoardCellChanged -= OnBoardCellChanged;
            
            base.Close();
        }
        
        private void OnBoardChanged(SymbolType[,] grid)
        {
            _boardView.UpdateBoard(grid);
        }
        
        private void OnBoardCellChanged(SymbolType symbol, int row, int column)
        {
            _boardView.UpdateCell(symbol, row, column);
        }
        
        void UpdatePlayerTurnText()
        {
            playerTurnText.text = "Player Turn: " + _currentPlayer;
        }
    }
}