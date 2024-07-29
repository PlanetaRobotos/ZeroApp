using System;
using _Project.Scripts.Windows.HUD;
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
    }
}