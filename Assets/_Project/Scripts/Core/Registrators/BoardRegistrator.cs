using _Project.Core.Rules;
using _Project.Models.Boards;
using _Project.Windows.BoardWidget.Factories;
using ServiceLocator.Core;
using UnityEngine;

namespace _Project.Core.Registrators
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