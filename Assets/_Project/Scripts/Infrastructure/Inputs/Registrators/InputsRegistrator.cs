using _Project.Scripts.Infrastructure.Inputs.Abstract;
using ServiceLocator.Core;

namespace _Project.Scripts.Infrastructure.Inputs.Registrators
{
    public class InputsRegistrator: BaseMonoServicesRegistrator
    {
        public override void Register()
        {
            Locator.Register<IInputObserver>(new InputObserver());
        }
    }
}