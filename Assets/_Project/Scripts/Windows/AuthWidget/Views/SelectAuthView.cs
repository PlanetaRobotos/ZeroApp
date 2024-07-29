﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Windows.AuthWidget
{
    public class SelectAuthView: MonoBehaviour
    {
        [SerializeField] private Button _signInButton;
        [SerializeField] private Button _signUpButton;
        
        private Action _onSignIn;
        private Action _onSignUp;

        public void Initialize(Action onSignIn, Action onSignUp)
        {
            _onSignUp = onSignUp;
            _onSignIn = onSignIn;
        }
        
        private void OnEnable()
        {
            _signInButton.onClick.AddListener(OnSignInButtonClick);
            _signUpButton.onClick.AddListener(OnSignUpButtonClick);
        }
        
        private void OnSignInButtonClick()
        {
            Debug.Log("Sign in button clicked");
            _onSignIn?.Invoke();
        }
        
        private void OnSignUpButtonClick()
        {
            Debug.Log("Sign up button clicked");
            _onSignUp?.Invoke();
        }
        
        private void OnDisable()
        {
            _signInButton.onClick.RemoveListener(OnSignInButtonClick);
            _signUpButton.onClick.RemoveListener(OnSignUpButtonClick);
        }
    }
}