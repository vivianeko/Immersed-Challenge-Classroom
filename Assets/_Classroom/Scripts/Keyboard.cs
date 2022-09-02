using UnityEngine;
using TMPro;

public class Keyboard : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;

    public void InsetCharcter(string c)
    {
        _inputField.text += c;
    }

    public void DelectCharacter()
    {
        if (_inputField.text.Length > 0)
        {
            _inputField.text = _inputField.text.Substring(0, _inputField.text.Length - 1);
        }
    }

    public void InsertSpace()
    {
        _inputField.text += " ";
    }
}
