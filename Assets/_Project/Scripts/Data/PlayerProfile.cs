using Fusion;

namespace _Project.Scripts.Core
{
    [System.Serializable]
    public struct PlayerProfile : INetworkStruct
    {
        public NetworkString<_16> Name;
        public SymbolType Symbol;
        public int Wins;

        public override string ToString() => $"{Name} ({Symbol})";
    }
}