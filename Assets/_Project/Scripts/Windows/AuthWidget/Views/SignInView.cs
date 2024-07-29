using System;
using _Project.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Windows.AuthWidget.Views
{
    public class SignInView : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _approveButton;

        [SerializeField] private TMP_InputField _usernameInput;
        [SerializeField] private TMP_InputField _passwordInput;

        [SerializeField] private TMP_Text _errorText;

        private Action _onBackButtonClick;
        private Func<SignUpModel, string> _onSignInButtonClick;

        public TMP_Text ErrorText => _errorText;

        private void Reset()
        {
            _usernameInput.text = "";
            _passwordInput.text = "";
            _errorText.text = "";
        }

        private void OnEnable()
        {
            _backButton.onClick.AddListener(OnBackButtonClick);
            _approveButton.onClick.AddListener(OnSignInButtonClick);
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(OnBackButtonClick);
            _approveButton.onClick.RemoveListener(OnSignInButtonClick);
        }

        public void Initialize(Action onBackButtonClick, Func<SignUpModel, string> onSignInButtonClick)
        {
            _onBackButtonClick = onBackButtonClick;
            _onSignInButtonClick = onSignInButtonClick;

            Reset();
        }

        private void OnBackButtonClick()
        {
            Reset();
            Debug.Log("Back button clicked");
            _onBackButtonClick?.Invoke();
        }

        private void OnSignInButtonClick()
        {
            Debug.Log("Sign in button clicked");
            _errorText.text = _onSignInButtonClick?.Invoke(new SignUpModel
            {
                Username = _usernameInput.text,
                Password = _passwordInput.text
            });
        }
    }
}