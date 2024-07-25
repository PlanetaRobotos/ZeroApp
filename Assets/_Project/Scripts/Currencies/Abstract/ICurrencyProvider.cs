using System;
using _Project.Scripts.Currencies.Data;

namespace _Project.Scripts.Currencies.Abstract
{
    public interface ICurrencyProvider
    {
        event Action<CurrencyIds, int> OnCurrencyChangedEvent;
        void SetCurrency(CurrencyIds id, int value);
        InGameCurrency GetCurrency(CurrencyIds id);
        void AddCurrency(CurrencyIds id, int value);
    }
}