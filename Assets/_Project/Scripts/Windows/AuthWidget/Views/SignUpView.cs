using System;
using _Project.Scripts.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Windows.AuthWidget
{
    public class SignUpView: MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _approveButton;

        [SerializeField] private TMP_InputField _emailInput;
        [SerializeField] private TMP_InputField _usernameInput;
        [SerializeField] private TMP_InputField _passwordInput;
        
        [SerializeField] private TMP_Text _errorText;
        
        private Action _onBackButtonClick;
        private Func<SignUpModel, string> _onApprove;        
        
        public void Initialize(Action onBackButtonClick, Func<SignUpModel, string> onApprove)
        {
            _onBackButtonClick = onBackButtonClick;
            _onApprove = onApprove;

            Reset();
        }

        private void OnEnable()
        {
            _backButton.onClick.AddListener(OnBackButtonClick);
            _approveButton.onClick.AddListener(OnApproveButtonClicked);
        }

        private void OnBackButtonClick()
        {
            Reset();
            Debug.Log("Back button clicked");
            _onBackButtonClick?.Invoke();
        }

        private void OnApproveButtonClicked()
        {
            Debug.Log($"Sign up button clicked with email: {_emailInput.text} and password: {_passwordInput.text} and username: {_usernameInput.text}");
            
            _errorText.text = _onApprove?.Invoke(new SignUpModel
            {
                Email = _emailInput.text,
                Password = _passwordInput.text,
                Username = _usernameInput.text
            });
        }

        private void Reset()
        {
            _emailInput.text = "";
            _usernameInput.text = "";
            _passwordInput.text = "";
            _errorText.text = "";
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(OnBackButtonClick);
            _approveButton.onClick.RemoveListener(OnApproveButtonClicked);
        }
    }
}