using _Project.Scripts.UI.Mediators;
using _Project.Scripts.Windows.HUD;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Windows.BoardWidget.Tasks
{
    public class LoadBoardWidgetTaskAsync: AsyncTask
    {
        [Inject] private BaseUIMediator<BoardWindow> _boardWindowMediator;
        
        protected override UniTask DoAsync()
        {
            
            
            return UniTask.CompletedTask;
        }
    }
}