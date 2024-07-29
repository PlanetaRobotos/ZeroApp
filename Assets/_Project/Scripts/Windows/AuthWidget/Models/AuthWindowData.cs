using System;
using _Project.Models;
using WindowsSystem.Core;

namespace _Project.Windows.AuthWidget.Models
{
    public class AuthWindowData : IncomingData
    {
        public Action<SignUpModel> OnCompleteSignIn;
        public Action<SignUpModel> OnCompleteSignUp;
    }
}