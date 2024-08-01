using System;
using UnityEngine;

namespace Game.Components
{
  [ExecuteAlways]
  public class ShaderTimeComponent : MonoBehaviour
  {
    private static readonly int TimeParam = Shader.PropertyToID("_TimeParam");
    public static bool ScaledTime;
    
    private float _currentTime => ScaledTime ? Time.time : Time.unscaledTime;

    private void Start()
    {
      ScaledTime = true;
    }

    private void Update()
    {
      Shader.SetGlobalVector(TimeParam, new Vector4(Mathf.Repeat(_currentTime, 10f), Mathf.Repeat(_currentTime, 1f), Mathf.Sin(_currentTime)));
    }
  }
}
