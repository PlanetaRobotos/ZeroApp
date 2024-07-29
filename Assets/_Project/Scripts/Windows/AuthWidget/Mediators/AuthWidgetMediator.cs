using System.Threading;
using _Project.Scripts.Core.Abstract;
using _Project.Scripts.Core.Auth;
using _Project.Scripts.Models;
using _Project.Scripts.UI.Mediators;
using _Project.Scripts.Windows.AuthWidget;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Windows.BoardWidget
{
    public class AuthWidgetMediator : BaseUIMediator<AuthWindow>
    {
        [Inject] private readonly IAuthProvider _authProvider;
        [Inject] private readonly IPlayerProfileProvider _playerProvider;
        [Inject] private readonly IGameTracker _gameTracker;

        protected override UniTask InitializeMediator(CancellationToken cancellationToken)
        {
            View.Data.OnCompleteSignUp += CompleteSignUp;
            View.Data.OnCompleteSignIn += CompleteSignIn;

            return UniTask.CompletedTask;
        }

        public override UniTask RunMediator(CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }

        private void CompleteSignUp(SignUpModel signUpModel)
        {
            _authProvider.SetAuth(signUpModel);
            _playerProvider.AuthModel = signUpModel;

            _authProvider.CreateAccount(signUpModel);
            
            View.Close();
            Dispose();
        }
        
        private void CompleteSignIn(SignUpModel signInModel) => 
            CompleteSignInAsync(signInModel);

        private async void CompleteSignInAsync(SignUpModel signInModel)
        {
            var result = await _authProvider.SignInAsync(signInModel.Username, signInModel.Password);
            if (result)
            {
                View.SignInView.ErrorText.text = "Success!";
                View.Close();
                Dispose();
            }
            else
            {
                View.SignInView.ErrorText.text = "Invalid username or password";
            }
        }

        protected override UniTask DisposeMediator()
        {
            View.Data.OnCompleteSignUp -= CompleteSignUp;
            View.Data.OnCompleteSignIn -= CompleteSignIn;
            
            return UniTask.CompletedTask;
        }
    }
}