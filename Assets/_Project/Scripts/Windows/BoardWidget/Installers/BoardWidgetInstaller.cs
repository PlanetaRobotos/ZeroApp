using _Project.UI.Mediators;
using _Project.Windows.BoardWidget.Handlers;
using _Project.Windows.BoardWidget.Mediators;
using _Project.Windows.BoardWidget.Views;
using ServiceLocator.Core;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Windows.BoardWidget.Installers
{
    public class BoardWidgetInstaller : BaseMonoServicesRegistrator
    {
        [SerializeField] private AssetReference _boardCellRef;
        [SerializeField] private AssetReference _lineRef;


        public override void Register()
        {
            BoardHandler boardHandler = new BoardHandler(_boardCellRef, _lineRef);
            Locator.Register<BaseUIMediator<BoardWindow>>(new BoardWindowMediator(boardHandler));
        }
    }
}