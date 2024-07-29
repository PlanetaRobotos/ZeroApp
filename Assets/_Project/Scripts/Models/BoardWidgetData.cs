using System;
using WindowsSystem.Core;

namespace _Project.Scripts.Core
{
    public class BoardWidgetData: IncomingData
    {
        public Action<SymbolType[,]> OnBoardChanged;
        public Action<bool> OnPlayerTurn;
        public Action<SymbolType> OnPlayerWin;
        public Action OnDraw;
        public Action<bool> OnInteractiveChanged;
        
        public int BoardSize;
        public IGameplayMediator GameplayMediator;
    }
}