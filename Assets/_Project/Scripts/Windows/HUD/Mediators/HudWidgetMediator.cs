using System.Threading;
using _Project.Core;
using _Project.Core.Tasks;
using _Project.UI.Mediators;
using _Project.Windows.HUD.Views;
using Cysharp.Threading.Tasks;

namespace _Project.Windows.HUD.Mediators
{
    public class HudWidgetMediator : BaseUIMediator<HUDWindow>
    {

        protected override UniTask InitializeMediator(CancellationToken cancellationToken)
        {

            return UniTask.CompletedTask;
        }

        public override UniTask RunMediator(CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }

        protected override UniTask DisposeMediator()
        {
            return UniTask.CompletedTask;
        }
    }
}