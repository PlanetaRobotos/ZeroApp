using _Project.Extensions;
using _Project.Models;
using _Project.Windows.AuthWidget.Models;
using UnityEngine;
using WindowsSystem.Core;

namespace _Project.Windows.AuthWidget.Views
{
    public class AuthWindow : BaseWindow<AuthWindowData>
    {
        [SerializeField] private SelectAuthView _selectView;
        [SerializeField] private SignInView _signInView;
        [SerializeField] private SignUpView _signUpView;

        public SignInView SignInView => _signInView;

        public override void OnOpen()
        {
            _selectView.Initialize(OnSignIn, OnSignUp);
            _signUpView.Initialize(OnBackToSelect, OnSignUpApprove);
            _signInView.Initialize(OnBackToSelect, OnSignInApprove);
        }

        private string OnSignUpApprove(SignUpModel signUpModel)
        {
            var statusText = "Success!";

            if (!signUpModel.Email.IsValidEmail(out string emailError))
            {
                statusText = emailError;
                return statusText;
            }

            if (!signUpModel.Username.IsValidUsername(out string usernameError))
            {
                statusText = usernameError;
                return statusText;
            }

            if (!signUpModel.Password.IsValidPassword(out string passwordError))
            {
                statusText = passwordError;
                return statusText;
            }

            Data.OnCompleteSignUp?.Invoke(signUpModel);
            return statusText;
        }

        private string OnSignInApprove(SignUpModel signInModel)
        {
            var statusText = "";

            if (!signInModel.Username.IsValidUsername(out string usernameError))
            {
                statusText = usernameError;
                return statusText;
            }

            if (!signInModel.Password.IsValidPassword(out string passwordError))
            {
                statusText = passwordError;
                return statusText;
            }

            Data.OnCompleteSignIn?.Invoke(signInModel);
            return statusText;
        }

        private void OnBackToSelect()
        {
            _selectView.gameObject.SetActive(true);
            _signInView.gameObject.SetActive(false);
            _signUpView.gameObject.SetActive(false);
        }

        private void OnSignIn()
        {
            _selectView.gameObject.SetActive(false);
            _signInView.gameObject.SetActive(true);
            _signUpView.gameObject.SetActive(false);
        }

        private void OnSignUp()
        {
            _selectView.gameObject.SetActive(false);
            _signInView.gameObject.SetActive(false);
            _signUpView.gameObject.SetActive(true);
        }
    }
}