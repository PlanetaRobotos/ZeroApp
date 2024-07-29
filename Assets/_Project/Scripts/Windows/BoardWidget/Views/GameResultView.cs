using _Project.Scripts.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Windows.HUD
{
    public class GameResultView: MonoBehaviour
    {
        [SerializeField] private TMP_Text _resultText;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _quitButton;
        
        public Button QuitButton => _quitButton;
        
        public void SetResultText(ResultType resultType)
        {
            switch (resultType)
            {
                case ResultType.Win:
                    _resultText.text = "You win!";
                    break;
                case ResultType.Lose:
                    _resultText.text = "You lose!";
                    break;
                case ResultType.Draw:
                    _resultText.text = "Draw!";
                    break;
            }
        }

        public void SetActive(bool isActive)
        {
            _canvasGroup.alpha = isActive ? 1 : 0;
            _canvasGroup.interactable = isActive;
            _canvasGroup.blocksRaycasts = isActive;
        }
    }
}