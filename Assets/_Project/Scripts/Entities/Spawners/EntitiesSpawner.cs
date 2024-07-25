using System;
using System.Collections.Generic;
using System.Threading;
using _Project.Scripts.Entities.Abstract;
using _Project.Scripts.Entities.AI.States;
using _Project.Scripts.Entities.Configs;
using _Project.Scripts.Entities.Factories;
using Cysharp.Threading.Tasks;
using MVVM.Adapters.Float;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Entities.Spawners
{
    public interface IEntitiesSpawner
    {
        UniTask SpawnInInterval(List<BaseEntity> entities, Collider2D spawnArea,
            MinMaxValue spawnDelayInterval, int maxAmount, Transform parent, CancellationToken cancellationToken = default);

        void StopSpawnAnimals();
    }

    public class EntitiesSpawner: IDisposable, IEntitiesSpawner
    {
        [Inject] private FollowBehaviour FollowBehaviour { get; }
        [Inject] private EntitiesProvider EntitiesProvider { get; }
        
        private readonly EntitiesConfig _entitiesConfig;
        private readonly EntityFactory _entityFactory;

        private readonly CancellationTokenSource _cts;
        private int _nameCounter;

        public EntitiesSpawner(EntitiesConfig entitiesConfig, EntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
            _entitiesConfig = entitiesConfig;

            _cts = new CancellationTokenSource();
        }

        public async UniTask SpawnInInterval(List<BaseEntity> entities, Collider2D spawnArea,
            MinMaxValue spawnDelayInterval, int maxAmount, Transform parent, CancellationToken cancellationToken = default)
        {
            while (!cancellationToken.IsCancellationRequested || !_cts.Token.IsCancellationRequested)
            {
                if (EntitiesProvider.Animals.Count >= maxAmount)
                    return;

                var entity = entities[Random.Range(0, entities.Count)];
                
                var instance = (AIBase)_entityFactory.Instantiate(entity, $"Animal_{_nameCounter}", spawnArea.TryGetRandomPosition(_entitiesConfig.CheckSpawnRadius, _entitiesConfig.SpawnLayerMask));
                instance.transform.SetParent(parent);
                instance.Initialize(FollowBehaviour, EntitiesProvider, spawnArea, _entitiesConfig);
                instance.InitializeStateMachine();
                instance.StateMachine.ChangeState(typeof(IdleAIState));
                EntitiesProvider.Animals.Add(instance);
                _nameCounter++;
                
                await UniTask.Delay((int)Random.Range(spawnDelayInterval.min, spawnDelayInterval.max) * 1000,
                    cancellationToken: cancellationToken);
            }
        }
        
        public void StopSpawnAnimals()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }
    }

    public static class AreaExtensions
    {
        public static Vector2 TryGetRandomPosition(this Collider2D spawnArea, float checkRadius, LayerMask layerMask)
        {
            const int maxAttempts = 100;
            
            Vector2 randomPosition;
            int attempts = 0;
            do
            {
                if (attempts >= maxAttempts)
                {
                    Debug.LogWarning("Could not find a valid position to spawn the object.");
                    return Vector2.zero;
                }

                attempts++;

                randomPosition = spawnArea.GetRandomPosition();
            } while (!spawnArea.OverlapPoint(randomPosition) ||
                     Physics2D.OverlapCircle(randomPosition, checkRadius, layerMask));

            return randomPosition;
        }
        
        public static Vector2 GetRandomPosition(this Collider2D spawnArea)
        {
            Bounds bounds = spawnArea.bounds;
            return new(Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y));
        }
    }
}