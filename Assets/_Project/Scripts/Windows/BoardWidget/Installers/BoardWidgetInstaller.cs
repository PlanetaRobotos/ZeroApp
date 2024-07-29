using _Project.Scripts.UI.Mediators;
using _Project.Scripts.Windows.BoardWidget.Handlers;
using _Project.Scripts.Windows.HUD;
using ServiceLocator.Core;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Windows.BoardWidget.Installers
{
    public class BoardWidgetInstaller: BaseMonoServicesRegistrator
    {
        [SerializeField] private AssetReference _boardCellRef;
        [SerializeField] private AssetReference _lineRef;
        
        
        public override void Register()
        {
            var boardHandler = new BoardHandler(_boardCellRef, _lineRef);
            Locator.Register<BaseUIMediator<BoardWindow>>(new BoardWindowMediator(boardHandler));
        }
    }
}