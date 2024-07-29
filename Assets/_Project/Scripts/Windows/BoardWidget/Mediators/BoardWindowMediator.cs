using System.Threading;
using _Project.Core;
using _Project.Models;
using _Project.UI.Mediators;
using _Project.Windows.BoardWidget.Handlers;
using _Project.Windows.BoardWidget.Views;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Windows.BoardWidget.Mediators
{
    public class BoardWindowMediator : BaseUIMediator<BoardWindow>
    {
        private readonly BoardHandler _boardHandler;

        private CellView _cellPrefab;
        private IGameplayMediator _gameplayMediator;
        private GameObject _linePrefab;
        [Inject] private IPlayerProfileProvider _playerProvider;

        public BoardWindowMediator(BoardHandler boardHandler)
        {
            _boardHandler = boardHandler;
        }

        protected override async UniTask InitializeMediator(CancellationToken cancellationToken)
        {
            _gameplayMediator = View.Data.GameplayMediator;
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
            _gameplayMediator.TryMakeMove(row, column);
        }

        private void OnQuitGameplay()
        {
            _gameplayMediator.ExitSession();
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