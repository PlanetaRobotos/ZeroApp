using UnityEngine;
using UnityEngine.UI;
using WindowsSystem.Core;

namespace _Project.Windows.SelectModeWidget.Views
{
    public class SelectModeWindow : BaseWindow
    {
        [SerializeField] private Button _multiplayerModeButton;
        [SerializeField] private Button _aiModeButton;

        public Button MultiplayerModeButton => _multiplayerModeButton;
        public Button AiModeButton => _aiModeButton;

        public override void OnOpen()
        {
        }
    }
}