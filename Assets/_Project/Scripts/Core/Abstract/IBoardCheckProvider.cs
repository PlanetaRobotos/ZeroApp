namespace _Project.Scripts.Core.Abstract
{
    public interface IBoardCheckProvider
    {
        PlayerProfile CheckWinner();
        bool IsDraw();
    }
}