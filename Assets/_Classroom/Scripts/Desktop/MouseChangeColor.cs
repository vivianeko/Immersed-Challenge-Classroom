using UnityEngine;

public class MouseChangeColor : MonoBehaviour
{

	private ColorSync _colorSync;


	private void Awake()
	{
		_colorSync = GetComponent<ColorSync>();
	}

    private void OnMouseDown()
    {
		_colorSync.SetColor();
	}
}
