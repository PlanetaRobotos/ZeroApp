using _Project.Scripts.Core.Abstract;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WindowsSystem.Core;

namespace _Project.Scripts.Windows.HUD
{
    public class HUDWindow: BaseWindow
    {
        [SerializeField] private TMP_Text _winsAmountText;
        [SerializeField] private TMP_Text _usernameText;
        [SerializeField] private Button _signOutButton;
        
        public Button SignOutButton => _signOutButton;
        
        [Inject] private IPlayerProfileProvider _playerProvider;
        
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