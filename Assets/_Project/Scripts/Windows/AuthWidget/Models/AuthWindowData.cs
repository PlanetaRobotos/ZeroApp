using System;
using _Project.Scripts.Models;
using WindowsSystem.Core;

namespace _Project.Scripts.Windows.AuthWidget
{
    public class AuthWindowData : IncomingData
    {
        public Action<SignUpModel> OnCompleteSignUp;
        public Action<SignUpModel> OnCompleteSignIn;
    }
}