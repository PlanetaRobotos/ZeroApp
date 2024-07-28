using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using WindowsSystem.Core;

namespace _Project.Scripts.Windows.HUD
{
    public class SelectModeWindow : BaseWindow
    {
        [SerializeField] private Button _multiplayerModeButton;
        
        [Inject] private PhotonManager _photonManager;
        
        public override void OnOpen()
        {
            _multiplayerModeButton.OnClickAsObservable().Subscribe(_ => OnMultiplayerModeSelected().Forget()).AddTo(this);
        }
        
        private async UniTaskVoid OnMultiplayerModeSelected()
        {
            _photonManager.StartGame().Forget();
            
            Close();
        }
    }
}