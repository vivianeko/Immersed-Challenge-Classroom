using UnityEngine;
using Normal.Realtime;
using TMPro;

public class ClientDataSync : RealtimeComponent<ClientDataSyncModel>
{
    [SerializeField] private RealtimeAvatarManager _avatarManager;
    [SerializeField] private TextMeshProUGUI _clientData;


    private void OnEnable()
    {
        _avatarManager.avatarCreated += AvatarChangeUpdate;
        _avatarManager.avatarDestroyed += AvatarChangeUpdate;
    }

    private void AvatarChangeUpdate(RealtimeAvatarManager avatarManager, RealtimeAvatar avatar, bool isLocal)
    {
        SetClientData();
    }

    protected override void OnRealtimeModelReplaced(ClientDataSyncModel previousModel, ClientDataSyncModel currentModel)
    {
        if (previousModel != null)
        {
            previousModel.clientDataDidChange -= ClientDataDidChange;
        }

        if (currentModel != null)
        {
            if (currentModel.isFreshModel)
                currentModel.clientData = _clientData.text;
            UpdateClientData();
            currentModel.clientDataDidChange += ClientDataDidChange;
        }
    }

    private void ClientDataDidChange(ClientDataSyncModel model, string value)
    {
        UpdateClientData();
    }

    private void UpdateClientData()
    {
        _clientData.text = "Students: " + "\n" + model.clientData;
    }

    public void SetClientData()
    {
        int clientId;
        model.clientData = "";
        foreach (var item in _avatarManager.avatars)
        {
            clientId = item.Key;            
            string role = _avatarManager.avatars[clientId].gameObject.GetComponent<Role>().GetRole();

            if (role == "Student")
            {
                string name = _avatarManager.avatars[clientId].gameObject.GetComponent<NameSync>().GetName();
                bool mute = _avatarManager.avatars[clientId].gameObject.GetComponentInChildren<RealtimeAvatarVoice>().mute;
                bool HandRaise = _avatarManager.avatars[clientId].gameObject.GetComponent<HandRaiseSync>().GetHandRaiseBool();

                model.clientData += "Student " + clientId + ": " + name + (mute == true ? " muted" : "") + (HandRaise == true ? " Hand Raised" : "") + "\n";
            }
        }
    }
}
