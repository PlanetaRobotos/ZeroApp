using System;
using _Project.Models;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Windows.BoardWidget.Views
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private Image _symbolImage;
        [SerializeField] private Button _button;

        public Image SymbolImage => _symbolImage;

        public void Initialize()
        {
        }

        public void Subscribe(Action action)
        {
            _button.onClick.AddListener(() => action());
        }

        public void Unsubscribe()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void SetSymbolSprite(Sprite symbol, SymbolType symbolType)
        {
            SetSymbolImageAltha(symbolType == SymbolType.None ? 0 : 1);
            _symbolImage.sprite = symbol;
        }

        public void SetSymbolImageAltha(int altha)
        {
            Color color = _symbolImage.color;
            color.a = altha;
            _symbolImage.color = color;
        }

        public void Clear()
        {
            _symbolImage.sprite = null;
        }

        public void SetInteractable(bool interactable)
        {
            _button.interactable = interactable;
        }
    }
}