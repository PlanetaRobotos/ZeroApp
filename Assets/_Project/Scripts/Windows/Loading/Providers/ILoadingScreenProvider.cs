using _Project.Scripts.Windows.Loading.Views;
using UnityEngine;

namespace _Project.Scripts.Windows.Loading.Providers
{
    public interface ILoadingScreenProvider
    {
        LoadingScreenView GetScreen(Transform parent);
    }
}