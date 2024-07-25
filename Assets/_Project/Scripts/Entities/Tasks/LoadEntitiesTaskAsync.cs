using _Project.Scripts.Entities.Abstract;
using _Project.Scripts.Entities.AI.AIEntities;
using _Project.Scripts.Entities.Configs;
using _Project.Scripts.Entities.Impl;
using _Project.Scripts.Entities.Spawners;
using _Project.Scripts.Infrastructure.Services.ApplicationObservers.Runtime;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Entities.Tasks
{
    public class LoadEntitiesTaskAsync : AsyncTask
    {
        [Inject] private IEntityFactory _entityFactory { get; }
        [Inject] private EntitiesConfig _entitiesConfig { get; }
        [Inject] private EntitiesProvider EntitiesProvider { get; }
        [Inject] private IEntitiesSpawner EntitiesSpawner { get; }
        [Inject] private IUpdater Updater { get; }

        protected override async UniTask DoAsync()
        {
            var playerTemplate = await _entityFactory.Load<PlayerEntity>();
            var player = (PlayerEntity)_entityFactory.Instantiate(playerTemplate, "Player");
            EntitiesProvider.PlayerEntity = player;
            EntitiesProvider.PlayerEntity.Initialize(Updater,
                player.Config.PlayerRotationSpeed, player.Config.PlayerMoveSpeed);
            EntitiesProvider.PlayerEntity.SpawnFollowPoints(_entitiesConfig.MaxFollowAmount);

            var animalTemplate = await _entityFactory.Load<SimpleAI>();
        }
    }
}