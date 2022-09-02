using UnityEngine;
using Normal.Realtime;

public class ColorSync : RealtimeComponent<ColorSyncModel>
{
    [SerializeField] Color[] _colors;
    private MeshRenderer _meshRenderer;
    private int _currentColor = 0;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    protected override void OnRealtimeModelReplaced(ColorSyncModel previousModel, ColorSyncModel currentModel)
    {
        if (previousModel != null)
        {
            previousModel.colorDidChange -= ColorDidChange;
        }

        if (currentModel != null)
        {
            if (currentModel.isFreshModel)
                currentModel.color = _meshRenderer.material.color;
            UpdateMeshRendererColor();
            currentModel.colorDidChange += ColorDidChange;
        }
    }

    private void ColorDidChange(ColorSyncModel model, Color value)
    {
        UpdateMeshRendererColor();
    }

    private void UpdateMeshRendererColor()
    {
        _meshRenderer.material.color = model.color;
    }

    public void SetColor()
    {
        for(int i=0; i<_colors.Length; i++)
        {
            if(model.color == _colors[i])
                _currentColor = i;
        }
        _currentColor = (_currentColor + 1) % _colors.Length;
        model.color = _colors[_currentColor];

    }
}
