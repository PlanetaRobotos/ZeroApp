using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Project.Scripts.Configs
{
    public class ConfigsController
    {
        public event Action OnLoadingCompleteEvent;
    
        private readonly Dictionary<Type, object> _configs;
        private readonly AssetReferenceT<ScriptableObject>[] _assets;

        public ConfigsController(AssetReferenceT<ScriptableObject>[] assets)
        {
            _configs = new Dictionary<Type, object>();
            _assets = assets ?? throw new ArgumentNullException(nameof(assets));
        }

        public void Initialize(Action onComplete = null)
        {
            onComplete?.Invoke();
        }

        public void Teardown()
        {
            UnloadAssets();
        }

        public void LoadAssets()
        {
            for (var i = 0; i < _assets.Length; i++)
            {
                _assets[i].LoadAssetAsync().Completed += OnAssetLoadingCompleted;
            }
        }

        public void UnloadAssets()
        {
            foreach (object config in _configs.Values)
            {
                Addressables.Release(config);
            }
        
            _configs.Clear();
        }

        public void Register(object config)
        {
            Register(config.GetType(), config);
        }

        public void Register<TConfig>(object config)
        {
            Register(typeof(TConfig), config);
        }

        public void Register(Type type, object config)
        {
            if (!_configs.ContainsKey(type))
            {
                _configs.Add(type, config);
            }
            else
            {
                Debug.LogError($"[ConfigsController] Object already exist. Type: [{type}].");
            }        
        }

        public void Release(object config)
        {
            Release(config.GetType());
        }

        public void Release<TConfig>()
        {
            Release(typeof(TConfig));
        }

        public void Release(Type type)
        {
            if (_configs.ContainsKey(type))
            {
                Addressables.Release(_configs[type]);
            }
        
            _configs.Remove(type);
        }

        public TOut Resolve<TOut>()
        {
            var configType = typeof(TOut);

            if (_configs.ContainsKey(configType))
            {
                return (TOut)_configs[configType];
            }

            return default;
        }

        private void OnAssetLoadingCompleted(AsyncOperationHandle<ScriptableObject> opResult)
        {
            if (opResult.Result != null)
            {
                Register(opResult.Result);
            }
            else
            {
                Debug.LogError($"[ConfigsController] Asset loading failed. Error: [{opResult.OperationException}]");
            }

            if (_configs.Count == _assets.Length) 
                OnLoadingCompleteEvent?.Invoke();
        }
    }
}