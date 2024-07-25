using System;
using WindowsSystem.Core;

namespace _Project.Scripts.Core
{
    public class BoardData: IncomingData
    {
        public Action<SymbolType, int, int> OnBoardCellChanged;
        public Action<SymbolType[,]> OnBoardChanged;
    }
}