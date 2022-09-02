using Normal.Realtime;

public class MuteBoolSync : RealtimeComponent<MuteBoolSyncModel>
{
    private bool _muted;

    protected override void OnRealtimeModelReplaced(MuteBoolSyncModel previousModel, MuteBoolSyncModel currentModel)
    {
        if (previousModel != null)
        {
            previousModel.muteBoolDidChange -= MuteBoolDidChange;
        }

        if (currentModel != null)
        {
            if (currentModel.isFreshModel)
                currentModel.muteBool = _muted;
            UpdateMuteBool();
            currentModel.muteBoolDidChange += MuteBoolDidChange;
        }
    }

    private void MuteBoolDidChange(MuteBoolSyncModel model, bool value)
    {
        UpdateMuteBool();
    }

    private void UpdateMuteBool()
    {
        _muted = model.muteBool;
    }

    public void SetMuteBool(bool mute)
    {        
        model.muteBool = mute;
    }

    public bool GetMuteBool()
    {
        return model.muteBool;
    }
}
