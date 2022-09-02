using UnityEngine;
using Normal.Realtime;

public class MuteChange : MonoBehaviour
{
    private int _muteClientId;
    private int _previousMuteClientId;    
    private bool _muted;
    private bool _previousMuted;
    
    [SerializeField] private RealtimeAvatarManager _avatarManager;
    
    private ClientDataSync _clientDataSync;
    private MuteClientIdSync _muteClientIdSync;
    private MuteBoolSync _muteBoolSync;
    private VisibleSync _visibleSync;

    private void Start()
    {
        _clientDataSync = GetComponent<ClientDataSync>();
        _muteClientIdSync = GetComponent<MuteClientIdSync>();
        _muteBoolSync = GetComponent<MuteBoolSync>();
        _visibleSync = GetComponent<VisibleSync>();
    }

    private void Update()
    {
        _muteClientId = _muteClientIdSync.GetMuteClientId();
        _muted = _muteBoolSync.GetMuteBool();

        if (_muteClientId != _previousMuteClientId || _muted != _previousMuted)
        {
            MuteSwitch();
        }        
    }

    private void MuteSwitch()
    {
        foreach (var item in _avatarManager.avatars)
        {
            if (_avatarManager.avatars.ContainsKey(_muteClientId)) //&& _muteClientId != _avatarManager.localAvatar.ownerIDInHierarchy
            {
                _avatarManager.avatars[_muteClientId].gameObject.GetComponentInChildren<RealtimeAvatarVoice>().mute = _muted;
                ChangeVisibility(_muteClientId, _muted);
                _previousMuted = _muted;
                _previousMuteClientId = _muteClientId;
                _clientDataSync.SetClientData();
            }
        }
    }

    private void ChangeVisibility(int Id, bool mute)
    {
        if (mute == true)
        {
            _visibleSync.RemoveVisible();
            SetLayerRecursively(_avatarManager.avatars[Id].gameObject , 7);
        }
        if (mute == false)
        {
            _visibleSync.AddVisible();
            SetLayerRecursively(_avatarManager.avatars[Id].gameObject, 0);
        }            
    }

    private void SetLayerRecursively(GameObject avatar, int layer)
    {
        if (null == avatar)
            return;
        avatar.layer = layer;
        foreach (Transform child in avatar.transform)
            SetLayerRecursively(child.gameObject, layer);
    }
}
