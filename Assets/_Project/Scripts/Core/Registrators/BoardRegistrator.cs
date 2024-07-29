using _Project.Configs.Boards;
using _Project.Scripts.Core.Abstract;
using _Project.Scripts.Data;
using _Project.Scripts.Windows.HUD;
using ServiceLocator.Core;
using UnityEngine;

namespace _Project.Scripts.Core.Registrators
{
    public class BoardRegistrator : BaseMonoServicesRegistrator
    {
        [SerializeField] private BoardDatabase boardDatabase;
        [SerializeField] private BoardConfig _boardConfig;
        
        public override void Register()
        {
            Locator.Register<IGameRules>(new GameRules());
            Locator.Register<IBoardFactory>(new BoardFactory(boardDatabase, _boardConfig));
        }
    }
}