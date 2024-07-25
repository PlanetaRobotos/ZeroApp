using _Project.Scripts.Currencies.Data;
using UnityEngine;

namespace _Project.Scripts.Currencies.SaveLoad
{
    public interface ISaveLoadCurrencies
    {
        void SaveCurrency(CurrencyIds id, int amount);
        int LoadCurrency(CurrencyIds id);
    }

    public class SaveLoadCurrencies : ISaveLoadCurrencies
    {
        public void SaveCurrency(CurrencyIds id, int amount)
        {
            PlayerPrefs.SetInt($"Currency_{id}", amount);
        }
        
        public int LoadCurrency(CurrencyIds id)
        {
            return PlayerPrefs.GetInt($"Currency_{id}", 0);
        }
    }
}