using System.Threading;
using _Project.Scripts.Core;
using _Project.Scripts.Core.Abstract;
using _Project.Scripts.Models;
using _Project.Scripts.UI.Mediators;
using _Project.Scripts.Windows.BoardWidget.Handlers;
using _Project.Scripts.Windows.HUD;
using Cysharp.Threading.Tasks;
using MVVM;
using UnityEngine;
using WindowsSystem.Core;

namespace _Project.Scripts.Windows.BoardWidget
{
    public class BoardWindowMediator: BaseUIMediator<BoardWindow>
    {
        [Inject] private PhotonManager _photonManager;
        [Inject] private IPlayerProfileProvider _playerProvider;
        
        private readonly BoardHandler _boardHandler;
        
        private CellView _cellPrefab;
        private GameObject _linePrefab;

        public BoardWindowMediator(BoardHandler boardHandler)
        {
            _boardHandler = boardHandler;
        }

        protected override async UniTask InitializeMediator(CancellationToken cancellationToken)
        {
            _cellPrefab = await _boardHandler.LoadCellView();
            _linePrefab = await _boardHandler.LoadLineView();
            
            View.ExitButton.onClick.AddListener(OnQuitGameplay);
            View.GameResultView.QuitButton.onClick.AddListener(OnQuitGameplay);
            View.Data.OnPlayerWin += OnPlayerWin;
        }

        public override UniTask RunMediator(CancellationToken cancellationToken)
        {
            View.BoardView.CreateBoard(_cellPrefab, OnCellClicked);
            View.BoardView.GenerateLines(_linePrefab);
            
            return UniTask.CompletedTask;
        }

        private void OnCellClicked(int row, int column)
        {
            _photonManager.TryMakeMove(row, column);
        }

        private void OnQuitGameplay()
        {
            _photonManager.ExitSession();
        }
        
        private void OnPlayerWin(SymbolType symbol)
        {
            View.SetActivePlayerTurnView(false);
            View.GameResultView.SetActive(true);
            View.ExitButton.gameObject.SetActive(false);
            View.GameResultView.SetResultText(symbol == _playerProvider.Symbol ? ResultType.Win : ResultType.Lose);
        }

        protected override UniTask DisposeMediator()
        {
            View.ExitButton.onClick.RemoveListener(OnQuitGameplay);
            View.GameResultView.QuitButton.onClick.RemoveListener(OnQuitGameplay);
            View.Data.OnPlayerWin -= OnPlayerWin;
            
            return UniTask.CompletedTask;
        }
    }
}