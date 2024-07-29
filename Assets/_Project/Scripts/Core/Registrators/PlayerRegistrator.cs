using _Project.Scripts.Core.Abstract;
using _Project.Scripts.Core.Auth;
using ServiceLocator.Core;

namespace _Project.Scripts.Core.Registrators
{
    public class PlayerRegistrator : BaseMonoServicesRegistrator
    {
        public override void Register()
        {
            Locator.Register<IAuthProvider>(new AuthProvider());
            Locator.Register<IPlayerProfileProvider>(new PlayerProfileProvider());
            Locator.Register<IGameTracker>(new PlayFabGameTracker());
        }
    }
}