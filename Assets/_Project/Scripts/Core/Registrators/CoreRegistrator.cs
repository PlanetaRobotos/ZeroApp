using _Project.Scripts.Core.Abstract;
using _Project.Scripts.Data;
using _Project.Scripts.Windows.HUD;
using ServiceLocator.Core;
using UnityEngine;

namespace _Project.Scripts.Core.Registrators
{
    public class CoreRegistrator : BaseMonoServicesRegistrator
    {
        [SerializeField] private BoardDatabase boardDatabase;
        [SerializeField] private Board boardPrefab;

        [SerializeField] private PhotonManager _photonManager;
        
        public override void Register()
        {
            Locator.Register(_photonManager);
            
            Locator.Register(new BoardData());
            Locator.Register(new BoardFactory(boardDatabase));
            Locator.Register<IPlayerProvider>(new PlayerProvider());
        }
    }
}