using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUnit : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Button _addButton;
    [SerializeField] private Button _removeButton;

    private void Awake()
    {
        _inputField.interactable = false;
    }

    public Button GetButton(bool Add) => Add ? _addButton : _removeButton;

    public TMP_InputField GetInputField() => _inputField;

}
