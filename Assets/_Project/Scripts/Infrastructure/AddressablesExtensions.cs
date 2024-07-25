using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Infrastructure
{
    public static class AddressablesExtensions
    {
        public static async UniTask<T> LoadAssetAsyncOnce<T>(this AssetReference assetReference) where T : Object
        {
            if (assetReference == null || !assetReference.RuntimeKeyIsValid())
                return null;

            if (!assetReference.IsValid())
            {
                return await assetReference.LoadAssetAsync<T>();
            }

            if (assetReference.Asset == null)
            {
                await assetReference.OperationHandle;
                return assetReference.OperationHandle.Result as T;

            }

            return assetReference.Asset as T;
        }
    }
}