namespace _Project.Scripts.Entities.Models
{
    public interface IEntity
    {
        EntityType EntityType { get; }
        string Name { get; }
    }
}