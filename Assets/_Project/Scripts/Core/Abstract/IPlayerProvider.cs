namespace _Project.Scripts.Core.Abstract
{
    public interface IPlayerProvider
    {
        PlayerProfile Player { get; }
        void SetPlayer(PlayerProfile player);
    }
}