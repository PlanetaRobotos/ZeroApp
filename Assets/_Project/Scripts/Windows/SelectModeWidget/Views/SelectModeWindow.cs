using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WindowsSystem.Core;

namespace _Project.Windows.SelectModeWidget.Views
{
    public class SelectModeWindow : BaseWindow
    {
        [SerializeField] private Button _multiplayerModeButton;
        [SerializeField] private Button _aiModeButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private CanvasGroup _settingsCanvasGroup;
        
        [SerializeField] private TMP_Text _winsAmountText;
        [SerializeField] private TMP_Text _usernameText;
        [SerializeField] private Button _signOutButton;
        
        public Button SignOutButton => _signOutButton;
        public TMP_Text UsernameText => _usernameText;
        public TMP_Text WinsAmountText => _winsAmountText;

        public Button MultiplayerModeButton => _multiplayerModeButton;
        public Button AiModeButton => _aiModeButton;
        public Button SettingsButton => _settingsButton;

        public override void OnOpen()
        {
        }
        
        public void SetSettingsPanelActive(bool active)
        {
            _settingsCanvasGroup.gameObject.SetActive(active);
        }
    }
}