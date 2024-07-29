using _Project.Windows.Loading.Views;
using UnityEngine;

namespace _Project.Windows.Loading.Providers
{
    public interface ILoadingScreenProvider
    {
        LoadingScreenView GetScreen(Transform parent);
    }
}