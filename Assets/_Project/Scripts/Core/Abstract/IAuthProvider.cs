using _Project.Models;
using Cysharp.Threading.Tasks;

namespace _Project.Core
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