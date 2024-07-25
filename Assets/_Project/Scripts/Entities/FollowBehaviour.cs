using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Entities.Abstract;
using _Project.Scripts.Entities.Configs;
using _Project.Scripts.Entities.Formations;
using CollectionsPooling;
using UnityEngine;

namespace _Project.Scripts.Entities
{
    public class FollowBehaviour : IDisposable
    {
        [Inject] private EntitiesProvider EntitiesProvider { get; }
        [Inject] private EntitiesConfig EntitiesConfig { get; }
        [Inject] private ICollectionsPoolService CollectionsPoolService { get; }

        private List<AIBase> _entities;
        private IFormation _grid;

        public void Initialize()
        {
            _entities = CollectionsPoolService.GetList<AIBase>();
            _grid = EntitiesConfig.FollowScript;
        }

        public void AddToGroup(AIBase entity)
        {
            _entities.Add(entity);
        }

        public bool CanBeAddedToGroup(AIBase entity) =>
            !_entities.Contains(entity) && _entities.Count < EntitiesConfig.MaxFollowAmount;

        public void RemoveFromGroup(AIBase animal)
        {
            if (_entities.Contains(animal))
            {
                _entities.Remove(animal);
            }
        }

        public void UpdateAnimalPositions()
        {
            List<Vector2> gridPositions =
                _grid.CalculatePositions(EntitiesProvider.PlayerEntity.FollowParent.position,
                    _entities.Select(x => x.transform).ToList());
            EntitiesProvider.PlayerEntity.SetFollowPositions(gridPositions);
        }

        public void Dispose()
        {
            CollectionsPoolService.Release(_entities);
        }
    }
}