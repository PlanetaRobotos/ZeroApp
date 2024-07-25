using System.Collections.Generic;
using _Project.Scripts.Currencies.Abstract;
using _Project.Scripts.Currencies.Data;
using _Project.Scripts.Entities.Abstract;
using _Project.Scripts.Entities.Impl;
using CollectionsPooling;

namespace _Project.Scripts.Entities
{
    public class EntitiesProvider
    {
        [Inject] private ICurrencyProvider CurrencyProvider { get; }
        [Inject] private ICollectionsPoolService CollectionsPoolService { get; }

        public PlayerEntity PlayerEntity { get; set; }
        public List<AIBase> Animals { get; }

        public EntitiesProvider()
        {
            Animals = CollectionsPoolService.GetList<AIBase>();
        }

        public void AddScore(AIBase entity)
        {
            CurrencyProvider.AddCurrency(CurrencyIds.Coins, entity.Cost);
        }
    }
}