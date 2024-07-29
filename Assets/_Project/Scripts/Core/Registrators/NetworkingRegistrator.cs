using ServiceLocator.Core;
using UnityEngine;

namespace _Project.Scripts.Core.Registrators
{
    public class NetworkingRegistrator : BaseMonoServicesRegistrator
    {
        [SerializeField] private PhotonManager _photonManager;
        
        public override void Register()
        {
            Locator.Register(_photonManager);
        }
    }
}