using UnityEngine;
using Normal.Realtime;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabRequest : MonoBehaviour
{
    private RealtimeTransform _realtimeTransform;
    private XRDoubeInteractable _xrGrabInteractable;

    private void Awake()
    {
        _realtimeTransform = GetComponent<RealtimeTransform>();
    }

    protected void OnEnable()
    {
        _xrGrabInteractable = GetComponent<XRDoubeInteractable>();
        if (_xrGrabInteractable != null)
        {
            _xrGrabInteractable.selectEntered.AddListener(OnSelectEntered);
        }
    }

    protected void OnDisable()
    {
        if (_xrGrabInteractable != null)
        {
            _xrGrabInteractable.selectEntered.RemoveListener(OnSelectEntered);
        }
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        if(!_realtimeTransform.isOwnedRemotelyInHierarchy)
            _realtimeTransform.RequestOwnership();
    }
}
