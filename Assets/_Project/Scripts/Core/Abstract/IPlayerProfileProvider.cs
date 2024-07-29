using System;
using _Project.Models;

namespace _Project.Core
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