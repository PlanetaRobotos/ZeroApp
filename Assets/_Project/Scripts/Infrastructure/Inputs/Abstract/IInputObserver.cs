using System;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Inputs.Abstract
{
    public interface IInputObserver
    {
        event Action<Vector2> OnClick;
    }
}