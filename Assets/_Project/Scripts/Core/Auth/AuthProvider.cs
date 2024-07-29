using _Project.Models;
using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace _Project.Core.Auth
{
    public class AuthProvider : IAuthProvider
    {
        private const string AuthPrefsKey = "AuthData";

        [Inject] private IGameTracker GameTracker { get; }
        [Inject] private IPlayerProfileProvider PlayerProvider { get; }

        public bool TryAutoAuth(out string email, out string password, out string username)
        {
            if (PlayerPrefs.GetString(AuthPrefsKey) != "")
            {
                string[] authData = PlayerPrefs.GetString(AuthPrefsKey).Split('|');
                email = authData[0];
                username = authData[1];
                password = authData[2];

                SignInAsync(username, password);

                return true;
            }

            email = null;
            password = null;
            username = null;
            return false;
        }

        public void SetAuth(SignUpModel signUpModel)
        {
            PlayerPrefs.SetString(AuthPrefsKey, $"{signUpModel.Email}|{signUpModel.Username}|{signUpModel.Password}");
            PlayerPrefs.Save();
        }

        public void CreateAccount(SignUpModel signUpModel)
        {
            PlayFabClientAPI.RegisterPlayFabUser(
                new RegisterPlayFabUserRequest
                {
                    Email = signUpModel.Email,
                    Password = signUpModel.Password,
                    Username = signUpModel.Username,
                    RequireBothUsernameAndEmail = true
                },
                response => { Debug.Log($"Successful Account Creation: {signUpModel.Username}, {signUpModel.Email}"); },
                error =>
                {
                    Debug.Log(
                        $"Unsuccessful Account Creation: {signUpModel.Username}, {signUpModel.Email}\n{error.ErrorMessage}");
                }
            );
        }

        public UniTask<bool> SignInAsync(string username, string password)
        {
            UniTaskCompletionSource<bool> completionSource = new();

            PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest
                {
                    Username = username,
                    Password = password
                },
                response =>
                {
                    Debug.Log($"Successful Account Login: {username}");
                    GameTracker.InitializeWins();
                    SignUpModel model = new()
                    {
                        Username = username,
                        Password = password
                    };
                    PlayerProvider.AuthModel = model;
                    SetAuth(model);
                    completionSource.TrySetResult(true);
                },
                error =>
                {
                    Debug.Log($"Unsuccessful Account Login: {username}\n{error.ErrorMessage}");
                    completionSource.TrySetResult(false);
                });

            return completionSource.Task;
        }

        public void SignOut()
        {
            PlayerPrefs.SetString(AuthPrefsKey, "");
            PlayerPrefs.Save();
        }
    }
}