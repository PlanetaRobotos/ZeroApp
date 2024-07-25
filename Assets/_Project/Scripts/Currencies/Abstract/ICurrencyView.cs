using UnityEngine;

namespace _Project.Scripts.Currencies.Abstract
{
    public interface ICurrencyView
    {
        RectTransform CurrencyIconRoot { get; }
        int CurrentAmount { get; }
        void RefreshView();
        void SetAmount(int amount);
        void SetAmountImmediately(int amount);
        void SetActualAmount();
        void SetActualAmountImmediately();
        void PlayReceiveCurrencyAnim();
        void LockView();
        void UnlockView(bool refresh, bool forceUpdate = false);
    }
}