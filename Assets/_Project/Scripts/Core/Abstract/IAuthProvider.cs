using _Project.Scripts.Models;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Core.Auth
{
    public interface IAuthProvider
    {
        bool TryAutoAuth(out string email, out string password, out string username);
        void SetAuth(SignUpModel signUpModel);
        void CreateAccount(SignUpModel signUpModel);
        UniTask<bool> SignInAsync(string username, string password);
        void SignOut();
    }
}