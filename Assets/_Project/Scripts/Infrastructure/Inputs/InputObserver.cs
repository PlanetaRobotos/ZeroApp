using System;
using _Project.Scripts.Infrastructure.Inputs.Abstract;
using _Project.Scripts.Infrastructure.Services.ApplicationObservers.Runtime;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Inputs
{
    public class InputObserver : IInputObserver
    {
        private const float UPDATE_INPUT_DELAY = 0.001f;

        [Inject] private IUpdater _updater;
        [Inject] private UIMainController _uiMainController;
        
        public event Action<Vector2> OnClick;

        public InputObserver()
        {
            _updater.Subscribe(OnUpdate, UPDATE_INPUT_DELAY);
        }

        public void OnUpdate(float _)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Input.mousePosition;
                Vector2 worldPos = _uiMainController.MainCamera.ScreenToWorldPoint(mousePos);
                RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

                if (hit.collider != null)
                {
                    OnClick?.Invoke(hit.point);
                }
            }
        }
    }
}