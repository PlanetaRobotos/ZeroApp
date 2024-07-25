using System;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.ReactiveAnimations.AnimationNodes.Runtime.Values
{
    public enum ToFromType
    {
        Direct,Dynamic
    }
    
    [Serializable]
    public class ToFromValue<T>
    {
        [SerializeField] private T _value;
        [SerializeField] private ToFromType _type;

        public static implicit operator T(ToFromValue<T> source)
        {
            return source._value;
        }
        public ToFromType Type => _type;
    }

    [Serializable]
    public class ToFromFloat : ToFromValue<float>
    {
        
    }
    
    [Serializable]
    public class ToFromVector3 : ToFromValue<Vector3>
    {
        
    }
    
    [Serializable]
    public class ToFromInt : ToFromValue<Vector3>
    {
        
    }
}