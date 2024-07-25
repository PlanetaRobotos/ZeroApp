namespace _Project.Scripts.Core
{
    public class PlayerProfile
    {
        public string Name { get; set; }
        public SymbolType Symbol { get; set; }
        public int Wins { get; set; }
    
        public void Initialize(PlayerData playerData)
        {
            Name = playerData.Name;
            Symbol = playerData.Symbol;
            Wins = 0;
        }
    }

    public struct PlayerData
    {
        public string Name;
        public SymbolType Symbol;
    }
}