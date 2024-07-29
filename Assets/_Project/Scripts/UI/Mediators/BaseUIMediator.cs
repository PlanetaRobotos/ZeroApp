using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using WindowsSystem.Core;

namespace _Project.UI.Mediators
{
    public abstract class BaseUIMediator<TView> where TView : class
    {
        protected TView View { get; private set; }

        protected CompositeDisposable CancellationToken { get; } = new();


        public UniTask InitializeMediator(BaseWindow view, CancellationToken cancellationToken)
        {
            View = view as TView;
            return InitializeMediator(cancellationToken);
        }

        protected abstract UniTask InitializeMediator(CancellationToken cancellationToken);

        public abstract UniTask RunMediator(CancellationToken cancellationToken);

        protected abstract UniTask DisposeMediator();

        public virtual void Dispose()
        {
            CancellationToken.Dispose();
            DisposeMediator().Forget();
        }
    }
}