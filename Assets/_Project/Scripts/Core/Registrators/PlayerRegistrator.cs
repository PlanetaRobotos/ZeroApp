using _Project.Core.Auth;
using _Project.Core.Providers;
using _Project.Core.Trackers;
using ServiceLocator.Core;

namespace _Project.Core.Registrators
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