using System.Threading;
using _Project.AI;
using _Project.Core;
using _Project.Core.Tasks;
using _Project.GameConstants;
using _Project.Networking;
using _Project.Scripts.Infrastructure;
using _Project.UI.Mediators;
using _Project.Windows.HUD.Views;
using _Project.Windows.SelectModeWidget.Views;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using WindowsSystem.Core.Managers;

namespace _Project.Windows.SelectModeWidget.Mediators
{
    public class SelectModeWindowMediator : BaseUIMediator<SelectModeWindow>
    {
        private readonly AssetReference _boardCellRef;
        private NetworkPlayerHandler _networkPlayer;

        [Inject] private IAuthProvider _authProvider;
        [Inject] private IPlayerProfileProvider PlayerProvider { get; }
        [Inject] private WindowsController _windowsController;

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
            View.SettingsButton.onClick.AddListener(OnSettingsClicked);

            View.SignOutButton.onClick.AddListener(OnSignOut);

            PlayerProvider.OnWinsAmountChanged += OnWinsAmountChanged;
            PlayerProvider.OnUsernameChanged += OnUsernameChanged;

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

        private void OnSignOut()
        {
            _authProvider.SignOut();
            PlayerProvider.Reset();

            new AuthAsyncTask().Do();
        }

        private void OnSettingsClicked()
        {
            View.UsernameText.text = $"Username: {PlayerProvider.AuthModel.Username}";
            View.WinsAmountText.text = $"Wins: {PlayerProvider.WinsAmount}";
            
            View.SetSettingsPanelActive(true);
        }

        private void OnWinsAmountChanged(int winsAmount)
        {
            View.WinsAmountText.gameObject.SetActive(winsAmount > 0);
            View.WinsAmountText.text = $"Wins: {winsAmount}";
        }

        private void OnUsernameChanged(string username)
        {
            View.UsernameText.gameObject.SetActive(!string.IsNullOrEmpty(username));
            View.UsernameText.text = $"Username: {username}";
        }

        protected override UniTask DisposeMediator()
        {
            View.MultiplayerModeButton.onClick.RemoveListener(MultiplayerModeSelected);
            View.AiModeButton.onClick.RemoveListener(AIModeSelected);
            View.SettingsButton.onClick.RemoveListener(OnSettingsClicked);

            View.SignOutButton.onClick.RemoveListener(OnSignOut);


            PlayerProvider.OnWinsAmountChanged -= OnWinsAmountChanged;
            PlayerProvider.OnUsernameChanged -= OnUsernameChanged;

            return UniTask.CompletedTask;
        }
    }
}