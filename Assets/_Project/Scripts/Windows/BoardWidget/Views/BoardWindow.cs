using _Project.Scripts.Core;
using _Project.Scripts.Models;
using Logging;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WindowsSystem.Core;

namespace _Project.Scripts.Windows.HUD
{
    public class BoardWindow : BaseWindow<BoardWidgetData>
    {
        [SerializeField] private TMP_Text playerTurnText;
        [SerializeField] private Button _exitButton;
        [SerializeField] private CanvasGroup _boardCanvasGroup;

        [SerializeField] private BoardView _boardView;
        [SerializeField] private GameResultView _gameResultView;
        
        public Button ExitButton => _exitButton;
        public BoardView BoardView => _boardView;
        public GameResultView GameResultView => _gameResultView;

        [Inject] private ICustomLogger _logger;
        
        public override void OnOpen()
        {
            _boardView.Construct(_logger);
            _boardView.Initialize(Data.BoardSize);
            
            Data.OnBoardChanged += OnBoardChanged;
            Data.OnPlayerTurn += OnPlayerTurn;
            Data.OnDraw += OnDraw;
            Data.OnInteractiveChanged += OnInteractiveChanged;
            
            GameResultView.SetActive(false);
            SetActivePlayerTurnView(true);
        }

        public override void Close()
        {
            Data.OnBoardChanged -= OnBoardChanged;
            Data.OnPlayerTurn -= OnPlayerTurn;
            Data.OnDraw -= OnDraw;
            Data.OnInteractiveChanged -= OnInteractiveChanged;
            
            base.Close();
        }
        
        private void OnInteractiveChanged(bool isInteractive)
        {
            _boardCanvasGroup.interactable = isInteractive;
        }

        private void OnDraw()
        {
            SetActivePlayerTurnView(false);
            
            GameResultView.SetActive(true);
            ExitButton.gameObject.SetActive(false);
            _gameResultView.SetResultText(ResultType.Draw);
        }

        public void SetActivePlayerTurnView(bool isActive)
        {
            playerTurnText.gameObject.SetActive(isActive);
        }
        
        private void OnBoardChanged(SymbolType[,] grid)
        {
            _boardView.UpdateBoard(grid);
        }
        
        private void OnPlayerTurn(bool isPlayerTurn)
        {
            UpdatePlayerTurnText(isPlayerTurn);
        }
        
        void UpdatePlayerTurnText(bool isPlayerTurn)
        {
            playerTurnText.text = isPlayerTurn ? "Your turn" : "Opponent's turn";
        }
    }
}