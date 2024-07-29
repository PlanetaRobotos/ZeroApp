using System;
using _Project.Scripts.Models;

namespace _Project.Scripts.Core.Abstract
{
    public interface IPlayerProfileProvider
    {
        SignUpModel AuthModel { get; set; }
        int WinsAmount { get; set; }
        Action<int> OnWinsAmountChanged { get; set; }
        Action<string> OnUsernameChanged { get; set; }
        SymbolType Symbol { get; set; }
        void Reset();
    }
}