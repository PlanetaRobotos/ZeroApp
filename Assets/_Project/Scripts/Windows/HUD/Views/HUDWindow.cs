using _Project.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WindowsSystem.Core;

namespace _Project.Windows.HUD.Views
{
    public class HUDWindow : BaseWindow
    {
        [SerializeField] private TMP_Text _winsAmountText;
        [SerializeField] private TMP_Text _usernameText;
        [SerializeField] private Button _signOutButton;

        [Inject] private IPlayerProfileProvider _playerProvider;

        public Button SignOutButton => _signOutButton;

        public override void OnOpen()
        {
            _winsAmountText.text = $"Wins: {_playerProvider.WinsAmount}";

            _playerProvider.OnWinsAmountChanged += OnWinsAmountChanged;
            _playerProvider.OnUsernameChanged += OnUsernameChanged;
        }

        private void OnWinsAmountChanged(int winsAmount)
        {
            _winsAmountText.gameObject.SetActive(winsAmount > 0);
            _winsAmountText.text = $"Wins: {winsAmount}";
        }

        private void OnUsernameChanged(string username)
        {
            _usernameText.gameObject.SetActive(!string.IsNullOrEmpty(username));
            _usernameText.text = $"Username: {username}";
        }

        public override void Close()
        {
            _playerProvider.OnWinsAmountChanged -= OnWinsAmountChanged;
            _playerProvider.OnUsernameChanged -= OnUsernameChanged;

            base.Close();
        }
    }
}