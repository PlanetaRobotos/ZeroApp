using System.Threading;
using _Project.AI;
using _Project.Core;
using _Project.Networking;
using _Project.Scripts.Infrastructure;
using _Project.UI.Mediators;
using _Project.Windows.SelectModeWidget.Views;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Windows.SelectModeWidget.Mediators
{
    public class SelectModeWindowMediator : BaseUIMediator<SelectModeWindow>
    {
        private readonly AssetReference _boardCellRef;
        private NetworkPlayerHandler _networkPlayer;

        public SelectModeWindowMediator(AssetReference boardCellRef)
        {
            _boardCellRef = boardCellRef;
        }

        protected override async UniTask InitializeMediator(CancellationToken cancellationToken)
        {
            _networkPlayer = await LoadNetworkPlayer();
        }

        public override UniTask RunMediator(CancellationToken cancellationToken)
        {
            View.MultiplayerModeButton.onClick.AddListener(MultiplayerModeSelected);
            View.AiModeButton.onClick.AddListener(AIModeSelected);

            return UniTask.CompletedTask;
        }

        private void MultiplayerModeSelected()
        {
            OnModeSelected(new PhotonGameplayMediator(_networkPlayer)).Forget();
        }

        private void AIModeSelected()
        {
            OnModeSelected(new AIGameplayMediator()).Forget();
        }

        private async UniTask OnModeSelected(IGameplayMediator gameplayMediator)
        {
            View.Close();
            gameplayMediator.StartGame();
            Dispose();
        }

        private async UniTask<NetworkPlayerHandler> LoadNetworkPlayer()
        {
            GameObject player = await _boardCellRef.LoadAssetAsyncOnce<GameObject>();
            if (player == null)
            {
                Debug.LogError("BoardCell is not loaded");
                return null;
            }

            return player.GetComponent<NetworkPlayerHandler>();
        }

        protected override UniTask DisposeMediator()
        {
            View.MultiplayerModeButton.onClick.RemoveListener(MultiplayerModeSelected);
            View.AiModeButton.onClick.RemoveListener(AIModeSelected);

            return UniTask.CompletedTask;
        }
    }
}