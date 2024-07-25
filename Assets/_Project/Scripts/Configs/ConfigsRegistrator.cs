using System;
using ServiceLocator.Core;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Configs
{
    public class ConfigsRegistrator : BaseMonoServicesRegistrator
    {
        [SerializeField]
        private AssetReferenceT<ScriptableObject>[] _assets = Array.Empty<AssetReferenceT<ScriptableObject>>();

        public override void Register()
        {
            Locator.Register(new ConfigsController(_assets));
        }
    }
}