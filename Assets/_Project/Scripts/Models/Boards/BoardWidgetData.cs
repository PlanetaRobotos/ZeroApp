using System;
using _Project.Core;
using WindowsSystem.Core;

namespace _Project.Models.Boards
{
    public class BoardWidgetData : IncomingData
    {
        public int BoardSize;
        public IGameplayMediator GameplayMediator;
        public Action<SymbolType[,]> OnBoardChanged;
        public Action OnDraw;
        public Action<bool> OnInteractiveChanged;
        public Action<bool> OnPlayerTurn;
        public Action<SymbolType> OnPlayerWin;
    }
}