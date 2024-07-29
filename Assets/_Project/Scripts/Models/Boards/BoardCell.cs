using System;
using Fusion;

namespace _Project.Models.Boards
{
    [Serializable]
    public struct BoardCell : INetworkStruct
    {
        public int Row;
        public int Column;
        public SymbolType Symbol;

        public override string ToString()
        {
            return $"Row: {Row}, Column: {Column}, Symbol: {Symbol}";
        }
    }
}