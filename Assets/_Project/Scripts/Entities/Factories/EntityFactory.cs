using System.Linq;
using _Project.Scripts.Entities.Abstract;
using _Project.Scripts.Entities.Configs;
using _Project.Scripts.Infrastructure.AssetsProviders.Abstract;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Entities.Factories
{
    public class EntityFactory : IEntityFactory
    {
        [Inject] private IAssetsProvider AssetsProvider { get; }

        private readonly EntitiesConfig _entitiesConfig;

        public EntityFactory(EntitiesConfig entitiesConfig)
        {
            _entitiesConfig = entitiesConfig;
        }

        public async UniTask<BaseEntity> Load<T>() where T : BaseEntity
        {
            var entityGO = await AssetsProvider.LoadAssetAsync<GameObject>(typeof(T).Name);
            var entity = entityGO.GetComponent<BaseEntity>();
            return entity;
        }

        public BaseEntity Instantiate(BaseEntity entityTemplate, string name, Vector2 at = default)
        {
            string key = entityTemplate.name;
            var entityConfig = _entitiesConfig.Entities.FirstOrDefault(x => x.AddressableKey == key);
            if (entityConfig == null)
            {
                Debug.LogError($"Entity with key {key} not found in config");
                return null;
            }

            var instance = Object.Instantiate(entityTemplate,
                at == default ? entityConfig.DefaultSpawnPosition : at, Quaternion.identity);
            
            instance.name = name;
            
            return instance;
        }
    }
}