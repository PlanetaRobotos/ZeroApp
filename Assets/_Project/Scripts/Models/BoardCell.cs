using _Project.Scripts.Core;
using Fusion;

namespace _Project.Scripts.Data
{
    [System.Serializable]
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