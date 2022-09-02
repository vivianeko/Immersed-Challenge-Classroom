using UnityEngine;
using Normal.Realtime;
using UnityEngine.XR.Interaction.Toolkit;

public class XRBrush : MonoBehaviour
{
    private XRController _controller;

    private InputHelpers.Button _triggerButton = InputHelpers.Button.TriggerButton;
    private float _activationThreshold = 0.1f;

    [SerializeField] private Realtime _realtime = null;
    [SerializeField] private GameObject _brushStrokePrefab = null;

    private Vector3 _handPosition;
    private Quaternion _handRotation;
    private BrushStroke _activeBrushStroke;

    private void Start()
    {
        _controller = GetComponent<XRController>();
    }

    private void Update()
    {
        if (!_realtime.connected)
            return;
        _handPosition = transform.position;
        _handRotation = transform.rotation;       

        bool triggerPressed = CheckIfTriggerDown(_controller);
        if (triggerPressed && _activeBrushStroke == null)
        {
            GameObject brushStrokeGameObject = Realtime.Instantiate(_brushStrokePrefab.name, new Realtime.InstantiateOptions
            {
                ownedByClient = true,
                useInstance = _realtime
            });
            _activeBrushStroke = brushStrokeGameObject.GetComponent<BrushStroke>();
            _activeBrushStroke.BeginBrushStrokeWithBrushTipPoint(_handPosition, _handRotation);
        }

        if (triggerPressed)
            _activeBrushStroke.MoveBrushTipToPoint(_handPosition, _handRotation);

        if (!triggerPressed && _activeBrushStroke != null)
        {
            _activeBrushStroke.EndBrushStrokeWithBrushTipPoint(_handPosition, _handRotation);
            _activeBrushStroke = null;
        }
    }

    private bool CheckIfTriggerDown(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, _triggerButton, out bool isPressed, _activationThreshold);
        return isPressed;
    }
}
