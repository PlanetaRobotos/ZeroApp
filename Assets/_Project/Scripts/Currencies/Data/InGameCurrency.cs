using System;
using UnityEngine;

namespace _Project.Scripts.Currencies.Data
{
    [Serializable]
    public class InGameCurrency
    {
        [SerializeField] private CurrencyIds _cid;
        [SerializeField] private int _vl;

        public CurrencyIds CurrencyId => _cid;
        public int CurrencyValue { get => _vl; set => _vl = value; }

        public InGameCurrency() { }

        public InGameCurrency(CurrencyIds currencyId, int currencyValue)
        {
            _cid = currencyId;
            _vl = currencyValue;
        }
    }
}