using UnityEngine;
using Normal.Realtime;

public class SimpleCamController : MonoBehaviour
{
    private float _lookSpeed = 2f;
    private float _rotation = 0f;
    private float _speed = 2.0f;
    [SerializeField] private GameObject _menu;
    [SerializeField] private Realtime _realtime;
    private bool _isConnected = false;

    private void OnEnable()
    {
        _realtime.didConnectToRoom += DidConnectToRoom;
    }

    private void DidConnectToRoom(Realtime realtime)
    {
        _rotation = transform.rotation.eulerAngles.y;
        _isConnected = true;        
    }
    private void LateUpdate()
    {
        Vector3 pos = transform.position;

        if (_isConnected)
        {            
            if (Input.GetMouseButton(1))
            {
                _rotation += _lookSpeed * Input.GetAxis("Mouse X");
                transform.eulerAngles = new Vector3(0f, _rotation, 0f);
            }
            if (Input.GetKey("a"))
                transform.Translate(new Vector3(-_speed * Time.deltaTime, 0, 0));

            if (Input.GetKey("d"))
                transform.Translate(new Vector3(_speed * Time.deltaTime, 0, 0));

            if (Input.GetKey("w"))
                transform.Translate(new Vector3(0, 0, _speed * Time.deltaTime));

            if (Input.GetKey("s"))
                transform.Translate(new Vector3(0, 0, -_speed * Time.deltaTime));

            if (Input.GetKeyUp(KeyCode.Space))
                _menu.SetActive(!_menu.activeInHierarchy);
        }
    }

}