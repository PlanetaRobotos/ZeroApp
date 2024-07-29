using TMPro;
using UnityEngine;
using WindowsSystem.Core;

namespace _Project.Windows.SearchOpponentWidget.Views
{
    public class SearchOpponentWindow : BaseWindow
    {
        [SerializeField] private TMP_Text _waitingText;

        public override void OnOpen()
        {
            SetActiveWaitView(true);
        }

        public void SetWaitingView(string text)
        {
            _waitingText.text = text;
        }

        public void SetActiveWaitView(bool isActive)
        {
            _waitingText.gameObject.SetActive(isActive);
        }
    }
}