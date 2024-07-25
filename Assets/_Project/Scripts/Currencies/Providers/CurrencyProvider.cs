using System;
using System.Collections.Generic;
using _Project.Scripts.Currencies.Abstract;
using _Project.Scripts.Currencies.Data;
using _Project.Scripts.Currencies.SaveLoad;

namespace _Project.Scripts.Currencies.Providers
{
    public class CurrencyProvider : ICurrencyProvider
    {
        public event Action<CurrencyIds, int> OnCurrencyChangedEvent;

        private readonly List<InGameCurrency> _currenciesList = new();
        private readonly SaveLoadCurrencies _saveLoad;

        public CurrencyProvider(SaveLoadCurrencies saveLoad)
        {
            _saveLoad = saveLoad;
        }

        public void AddCurrency(CurrencyIds id, int value)
        {
            SetCurrency(id, GetCurrency(id).CurrencyValue + value);
        }

        public void SetCurrency(CurrencyIds id, int value)
        {
            var currency = GetCurrency(id);
            currency.CurrencyValue = value;
            
            OnCurrencyChangedEvent?.Invoke(id, currency.CurrencyValue);
            if (_saveLoad != null) _saveLoad.SaveCurrency(id, value);

        }

        public InGameCurrency GetCurrency(CurrencyIds id)
        {
            var currency = _currenciesList.Find(x => x.CurrencyId == id);

            if (currency == null)
            {
                currency = new InGameCurrency(id, 0);
                _currenciesList.Add(currency);

                if (_saveLoad != null && _saveLoad.LoadCurrency(id) > 0)
                {
                    currency.CurrencyValue = _saveLoad.LoadCurrency(id);
                }
            }

            return currency;
        }
    }
}