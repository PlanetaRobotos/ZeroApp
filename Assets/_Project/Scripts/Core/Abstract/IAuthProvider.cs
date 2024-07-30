using _Project.Models;
using Cysharp.Threading.Tasks;

namespace _Project.Core
{
    public interface IAuthProvider
    {
        UniTask<bool> TryAutoAuth();
        void SetAuth(SignUpModel signUpModel);
        UniTask<bool> CreateAccount(SignUpModel signUpModel);
        UniTask<bool> SignInAsync(string username, string password);
        void SignOut();
    }
}