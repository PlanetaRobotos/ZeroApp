namespace _Project.Scripts.Core
{
    public class Move : IMove
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public PlayerProfile PlayerProfile { get; set; }

        public Move(int row, int column, PlayerProfile playerProfile)
        {
            Row = row;
            Column = column;
            PlayerProfile = playerProfile;
        }
    }

    public interface IMove
    {
        int Row { get; set; }
        int Column { get; set; }
        PlayerProfile PlayerProfile { get; set; }
    }

}