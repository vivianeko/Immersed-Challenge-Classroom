using UnityEngine;
using TMPro;

public class NameChange : MonoBehaviour
{
    private string _input;

    [SerializeField] private TMP_InputField _inputField;

    public void NameHasChanged()
    {
        _input = _inputField.text;
    }

    public string GetName()
    {
        return _input;
    }
}
