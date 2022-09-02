using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRInputController : MonoBehaviour
{
    [SerializeField] private XRController _leftController;
    [SerializeField] private XRController _rightController;

    private InputHelpers.Button _teleportRayButton = InputHelpers.Button.PrimaryAxis2DUp;
    private InputHelpers.Button _primaryButton = InputHelpers.Button.PrimaryButton;
    
    private float _activationThreshold = 0.1f;

    private XRInteractorLineVisual _rightRay;
    private GameObject _rightReticle;

    private XRInteractorLineVisual _leftRay;
    private GameObject _leftReticle;

    [SerializeField] private GameObject _menu;


    private void Start()
    {
        _leftRay = _leftController.gameObject.GetComponent<XRInteractorLineVisual>();
        _leftReticle = _leftRay.reticle;

        _rightRay = _rightController.gameObject.GetComponent<XRInteractorLineVisual>();
        _rightReticle = _rightRay.reticle;
    }

    private void Update()
    {
        bool leftIsPressed = ChechIfAxisUpDown(_leftController);
        _leftRay.enabled = leftIsPressed;
        _leftReticle.SetActive(leftIsPressed);

        bool rightIsPressed = ChechIfAxisUpDown(_rightController);
        _rightRay.enabled = rightIsPressed;
        _rightReticle.SetActive(rightIsPressed);

        bool primaryIsPressed = CheckIfPrimaryDown(_leftController) || CheckIfPrimaryDown(_rightController);
        _menu.SetActive(primaryIsPressed);
    }

    private bool ChechIfAxisUpDown(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, _teleportRayButton, out bool isPressed, _activationThreshold);
        return isPressed;
    }

    private bool CheckIfPrimaryDown(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, _primaryButton, out bool isPressed, _activationThreshold);
        return isPressed;
    }
}
