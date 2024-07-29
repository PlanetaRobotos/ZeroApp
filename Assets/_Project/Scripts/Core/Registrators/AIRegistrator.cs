using _Project.Scripts.AI;
using ServiceLocator.Core;

namespace _Project.Scripts.Core.Registrators
{
    public class AIRegistrator : BaseMonoServicesRegistrator
    {
        public override void Register()
        {
            Locator.Register(new AIGameplayMediator());
        }
    }
}