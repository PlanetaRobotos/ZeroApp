using System.Collections.Generic;
using _Project.Scripts.Infrastructure.AssetsProviders.Abstract;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Project.Scripts.Infrastructure.AssetsProviders
{
    public class AssetsProvider : IAssetsProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _handles = new();

        public async UniTask<T> LoadAssetAsync<T>(AssetReference reference)
        {
            if (_handles.TryGetValue(reference.AssetGUID, out var handle))
            {
                if (handle.Result != null)
                    return (T)handle.Result;
                return await LoadWithCache<T>(reference.AssetGUID, handle);
            }

            return await LoadWithCache<T>(reference.AssetGUID, reference.LoadAssetAsync<T>());
        }

        public async UniTask<T> LoadAssetAsync<T>(string address)
        {
            if (_handles.TryGetValue(address, out var handle))
            {
                if (handle.Result != null)
                    return (T)handle.Result;
                return await LoadWithCache<T>(address, handle);
            }

            return await LoadWithCache<T>(address, Addressables.LoadAssetAsync<T>(address));
        }

        private async UniTask<T> LoadWithCache<T>(string address, AsyncOperationHandle handle)
        {
            _handles[address] = handle;
            await handle.ToUniTask();
            return (T)handle.Result;
        }

        public virtual void CleanUp()
        {
            foreach (AsyncOperationHandle handle in _handles.Values)
                Addressables.Release(handle);

            _handles.Clear();
        }
    }

    public static class AssetsExtensions
    {
        public static string GetAssetName(this AssetReference[] assetReferences, int index)
        {
            if (assetReferences.Length <= index || index < 0)
            {
                Debug.Log($"Index {index} is out of range");
                return string.Empty;
            }

            return assetReferences[index].Asset.name;
        }
    }
}