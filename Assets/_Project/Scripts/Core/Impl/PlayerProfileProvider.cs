using System;
using _Project.Scripts.Core.Abstract;
using _Project.Scripts.Models;

namespace _Project.Scripts.Core
{
    public class PlayerProfileProvider : IPlayerProfileProvider
    {
        private int _winsAmount;
        private SignUpModel _authModel;

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

        public override string ToString() => 
            $"PlayerProvider: {AuthModel}, Symbol: {Symbol}, WinsAmount: {WinsAmount},";
    }
}