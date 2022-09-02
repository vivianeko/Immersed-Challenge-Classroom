using UnityEngine;
using Normal.Realtime;


public class MouseInteractable : MonoBehaviour
{
	private Vector3 _screenPoint;
	private Vector3 _offset;
	private bool _isConnected = false;
	private string _role;
	[SerializeField] private bool _specialObject;

	private RealtimeTransform _realtimeTransform;

	[SerializeField] private Realtime _realtime;
	[SerializeField] private RealtimeAvatarManager _avatarManager;

    private void OnEnable()
	{
		_realtime.didConnectToRoom += DidConnectToRoom;
		_realtimeTransform = GetComponent<RealtimeTransform>();
	}

	private void DidConnectToRoom(Realtime realtime)
    {
		SetMouseRole();
	}

	private void SetMouseRole()
    {
		_role = _avatarManager.localAvatar.gameObject.GetComponent<Role>().GetRole();
		if (_role != "Professor" && _specialObject)
			enabled = false;
		_isConnected = true;
	}

    private void OnMouseDown()
	{
		if (!enabled) return;
		if (!_realtimeTransform.isOwnedRemotelyInHierarchy && _isConnected)
		{
			_screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
			_offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
			_realtimeTransform.RequestOwnership();
		}			
	}

	private void OnMouseDrag()
	{
		if (!enabled) return;
		if (!_realtimeTransform.isOwnedRemotelyInHierarchy && _isConnected)
        {
			_realtimeTransform.RequestOwnership();
			Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
			Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + _offset;
			transform.position = cursorPosition;
		}		
	}

	private void OnMouseOver()
	{
		if (!enabled) return;
		if (!_realtimeTransform.isOwnedRemotelyInHierarchy && _isConnected)
        {
			_realtimeTransform.RequestOwnership();
			if (Input.GetKeyDown("v"))
			{
				transform.localScale = transform.localScale * 2;
			}
			if (Input.GetKeyDown("b"))
			{
				transform.localScale = transform.localScale / 2;
			}
		}	
	}
}