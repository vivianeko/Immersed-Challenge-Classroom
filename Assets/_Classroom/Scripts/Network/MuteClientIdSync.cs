using UnityEngine;
using Normal.Realtime;
using TMPro;

public class MuteClientIdSync : RealtimeComponent<MuteClientIdSyncModel>
{
    private int _muteClientId;
    [SerializeField] private TMP_InputField _inputField;

    protected override void OnRealtimeModelReplaced(MuteClientIdSyncModel previousModel, MuteClientIdSyncModel currentModel)
    {
        if (previousModel != null)
        {
            previousModel.muteClientIdDidChange -= MuteClientIdDidChange;
        }

        if (currentModel != null)
        {
            if (currentModel.isFreshModel)
                currentModel.muteClientId = _muteClientId;
            UpdateMuteClientId();
            currentModel.muteClientIdDidChange += MuteClientIdDidChange;
        }
    }

    private void MuteClientIdDidChange(MuteClientIdSyncModel model, int value)
    {
        UpdateMuteClientId();
    }

    private void UpdateMuteClientId()
    {
        _muteClientId = model.muteClientId;
    }

    public void SetIdFromText()
    {
        int tempId = 0;
        tempId = int.Parse(_inputField.text);
        SetMuteClientId(tempId);
    }

    public void SetMuteClientId(int id)
    {
        model.muteClientId = id;
    }

    public int GetMuteClientId()
    {
        return model.muteClientId;
    }
}
