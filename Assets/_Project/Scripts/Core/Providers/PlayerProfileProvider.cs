using System;
using _Project.Models;

namespace _Project.Core.Providers
{
    public class PlayerProfileProvider : IPlayerProfileProvider
    {
        private SignUpModel _authModel;
        private int _winsAmount;

        public SignUpModel AuthModel
        {
            get => _authModel;
            set
            {
                _authModel = value;
                OnUsernameChanged?.Invoke(_authModel.Username);
            }
        }

        public SymbolType Symbol { get; set; }

        public void Reset()
        {
            AuthModel = new SignUpModel();
            WinsAmount = 0;
        }

        public int WinsAmount
        {
            get => _winsAmount;
            set
            {
                _winsAmount = value;
                OnWinsAmountChanged?.Invoke(_winsAmount);
            }
        }

        public Action<int> OnWinsAmountChanged { get; set; }
        public Action<string> OnUsernameChanged { get; set; }

        public override string ToString()
        {
            return $"PlayerProvider: {AuthModel}, Symbol: {Symbol}, WinsAmount: {WinsAmount},";
        }
    }
}