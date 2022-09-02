using UnityEngine;
using Normal.Realtime;

public class HandRaiseChange : MonoBehaviour
{
    private bool _handRaised;
    [SerializeField] RealtimeAvatarManager _avatarManager;
    private ClientDataSync _clientDataSync;

    private void Start()
    {
        _clientDataSync = GetComponent<ClientDataSync>();
    }

    public void RaiseHand()
    {
        _handRaised = !_handRaised;
        _avatarManager.localAvatar.GetComponent<HandRaiseSync>().SetHandRaiseBool(_handRaised);
        _clientDataSync.SetClientData();
    }
}
