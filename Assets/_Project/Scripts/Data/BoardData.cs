using System;
using WindowsSystem.Core;

namespace _Project.Scripts.Core
{
    public class BoardData: IncomingData
    {
        // public Action<SymbolType, int, int> OnBoardCellChanged;
        public Action<SymbolType[,]> OnBoardChanged;
        public Action<bool> OnPlayerTurn;
        public Action<SymbolType> OnPlayerWin;
        public Action OnDraw;
        public Action<bool> OnInteractiveChanged;
    }
}