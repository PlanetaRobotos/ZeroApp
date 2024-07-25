using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Entities.Abstract
{
    public interface IEntityFactory
    {
        UniTask<BaseEntity> Load<T>() where T : BaseEntity;
        BaseEntity Instantiate(BaseEntity entityTemplate, string name, Vector2 at = default);
    }
}