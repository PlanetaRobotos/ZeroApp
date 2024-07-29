using Constellation.SceneManagement;
using ServiceLocator.Core;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Registrators
{
    public class SceneManagementInstaller : BaseMonoServicesRegistrator
    {
        [SerializeField] private ScenesLibrary scenesLibrary;

        public override void Register()
        {
            Locator.Register(scenesLibrary);
            Locator.Register<IScenesManager>(new ScenesManager(scenesLibrary));
        }
    }
}