using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class KeyboardButton : MonoBehaviour, IPointerUpHandler
{
    private Keyboard _keyboard;
    private TextMeshProUGUI _buttonText;

    private void Start()
    {
        _keyboard = GetComponentInParent<Keyboard>();
        _buttonText = GetComponentInChildren<TextMeshProUGUI>();

        if(_buttonText.text.Length == 1)
        {
            NameToButtonText();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_buttonText.text.Length == 1)
            _keyboard.InsetCharcter(_buttonText.text);
        if (_buttonText.text == "SPACE")
            _keyboard.InsertSpace();
        if (_buttonText.text == "DELETE" | _buttonText.text == "DEL")
            _keyboard.DelectCharacter();
    }

    private void NameToButtonText()
    {
        _buttonText.text = gameObject.name;
    }
}
