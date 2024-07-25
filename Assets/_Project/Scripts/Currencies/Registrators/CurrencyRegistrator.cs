using _Project.Scripts.Currencies.Abstract;
using _Project.Scripts.Currencies.Providers;
using _Project.Scripts.Currencies.SaveLoad;
using ServiceLocator.Core;

namespace _Project.Scripts.Currencies.Registrators
{
    public class CurrencyRegistrator: BaseMonoServicesRegistrator
    {
        public override void Register()
        {
            Locator.Register<ICurrencyProvider>(new CurrencyProvider(new SaveLoadCurrencies()));
        }
    }
}