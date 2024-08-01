using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class ChangeVectorVariable : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private string _fieldName;
    [SerializeField] private Vector2 _fieldValue;

    private Vector2 _value;

    private void Awake()
    {
        if(_image != null)
            _value = _image.material.GetVector(_fieldName);
    }

    private void LateUpdate()
    {
        if (_fieldValue != _value)
        {
            _value = _fieldValue;
            _image.material.SetVector(_fieldName, _fieldValue);
        }
    }
}