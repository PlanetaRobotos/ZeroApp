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
        // [SerializeField] private Button resetButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private CanvasGroup _boardCanvasGroup;

        [SerializeField] private BoardView _boardView;
        
        [Inject] private ICustomLogger _logger;
        [Inject] private PhotonManager _photonManager;
        [Inject] private IPlayerProvider _playerProvider;
        
        public override void OnOpen()
        {
            _boardView.Construct(_logger, _photonManager);
            _boardView.Initialize(3, 3);
            
            Data.OnBoardChanged += OnBoardChanged;
            Data.OnPlayerTurn += OnPlayerTurn;
            // Data.OnBoardCellChanged += OnBoardCellChanged;
            Data.OnPlayerWin += OnPlayerWin;
            Data.OnDraw += OnDraw;
            Data.OnInteractiveChanged += OnInteractiveChanged;
            
            _exitButton.onClick.AddListener(OnQuitGameplay);

            SetActiveResultView(false);
            SetActivePlayerTurnView(true);
        }

        public override void Close()
        {
            Data.OnBoardChanged -= OnBoardChanged;
            Data.OnPlayerTurn -= OnPlayerTurn;
            // Data.OnBoardCellChanged -= OnBoardCellChanged;
            Data.OnPlayerWin -= OnPlayerWin;
            Data.OnDraw -= OnDraw;
            Data.OnInteractiveChanged -= OnInteractiveChanged;
            
            _exitButton.onClick.RemoveListener(OnQuitGameplay);
            
            base.Close();
        }
        
        private void OnInteractiveChanged(bool isInteractive)
        {
            _boardCanvasGroup.interactable = isInteractive;
        }
        
        private void OnPlayerWin(SymbolType symbol)
        {
            SetActivePlayerTurnView(false);
            
            SetActiveResultView(true);
            resultText.text = "You " + (symbol == _playerProvider.Player.Symbol ? "win!" : "lose!");
        }

        private void SetActiveResultView(bool isActive)
        {
            resultText.gameObject.SetActive(isActive);
        }

        private void OnDraw()
        {
            SetActivePlayerTurnView(false);
            
            SetActiveResultView(true);
            resultText.text = "Draw!";
        }

        private void SetActivePlayerTurnView(bool isActive)
        {
            playerTurnText.gameObject.SetActive(isActive);
        }

        private void OnQuitGameplay()
        {
            _photonManager.ExitSession();
            
            Close();
        }
        
        private void OnBoardChanged(SymbolType[,] grid)
        {
            _boardView.UpdateBoard(grid);
        }
        
        private void OnBoardCellChanged(SymbolType symbol, int row, int column)
        {
            _boardView.UpdateCell(symbol, row, column);
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