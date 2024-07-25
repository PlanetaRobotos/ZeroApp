using _Project.Scripts.Currencies.Abstract;
using _Project.Scripts.Currencies.Data;
using MVVM;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Currencies.Views
{
    public class BaseCurrencyView : MonoBehaviour, ICurrencyView
    {
        [SerializeField] private CurrencyIds _currencyId;
        [SerializeField] private Image _currencyIcon;
        [SerializeField] private Animation _anim;
        [SerializeField] private string _onReceiveAnimName;
    
        private bool _autoUpdateLock;
    
        [Inject] private ICurrencyProvider CurrencyController { get; }

        public ReactiveProperty<IntSmoothValueData> SmoothAmount { get; } = new(new IntSmoothValueData(0, false));
        public ReactiveProperty<int> AmountChanged { get; } = new();
        public CurrencyIds CurrencyId => _currencyId;
        public RectTransform CurrencyIconRoot => _currencyIcon.rectTransform;
        public int CurrentAmount => CurrencyController.GetCurrency(_currencyId).CurrencyValue;
    
        private void OnEnable()
        {
            CurrencyController.OnCurrencyChangedEvent += OnCurrencyChanged;

            SetActualAmountImmediately();
        }

        private void OnDisable()
        { 
            CurrencyController.OnCurrencyChangedEvent -= OnCurrencyChanged;
        }

        public virtual void RefreshView()
        {
            Refresh(forceUpdate: false);
        }

        private void OnCurrencyChanged(CurrencyIds currency, int value)
        {
            if (currency != _currencyId)
                return;

            RefreshView();
        }

        public virtual void SetAmount(int amount)
        {
            SmoothAmount.Value = new IntSmoothValueData(amount);
        
            if (amount > AmountChanged)
                AmountChanged.Value = amount;
            else
                AmountChanged.SetWithoutNotify(amount);
        }

        public virtual void SetAmountImmediately(int amount)
        {
            SmoothAmount.Value = new IntSmoothValueData(amount, false);
            AmountChanged.SetWithoutNotify(amount);
        }

        public virtual void SetActualAmount()
        {
            var amount = CurrentAmount;

            SmoothAmount.Value = new IntSmoothValueData(amount);

            if (amount > AmountChanged)
                AmountChanged.Value = amount;
        }

        public virtual void SetActualAmountImmediately()
        {
            SmoothAmount.Value = new IntSmoothValueData(CurrentAmount, false);
            AmountChanged.SetWithoutNotify(CurrentAmount);
        }

        public void PlayReceiveCurrencyAnim()
        {
            if (_anim != null) 
                _anim.Play(_onReceiveAnimName);
        }

        public void LockView()
        {
            _autoUpdateLock = true;
        }

        public void UnlockView(bool refresh, bool forceUpdate = false)
        {
            _autoUpdateLock = false;

            if (refresh) 
                Refresh(forceUpdate);
        }

        private void Refresh(bool forceUpdate = false)
        {
            if (_autoUpdateLock)
                return;

            if (forceUpdate)
                SetAmountImmediately(CurrentAmount);
            else
                SetActualAmount();
        }
    }
}