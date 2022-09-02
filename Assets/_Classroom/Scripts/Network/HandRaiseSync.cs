using Normal.Realtime;

public class HandRaiseSync : RealtimeComponent<HandRaiseSyncModel>
{
    private bool _handRaised;

    protected override void OnRealtimeModelReplaced(HandRaiseSyncModel previousModel, HandRaiseSyncModel currentModel)
    {
        if (previousModel != null)
        {
            previousModel.handRaiseBoolDidChange -= HandRaiseBoolDidChange;
        }

        if (currentModel != null)
        {
            if (currentModel.isFreshModel)
                currentModel.handRaiseBool = _handRaised;
            UpdateHandRaiseBool();
            currentModel.handRaiseBoolDidChange += HandRaiseBoolDidChange;
        }
    }

    private void HandRaiseBoolDidChange(HandRaiseSyncModel model, bool value)
    {
        UpdateHandRaiseBool();
    }

    private void UpdateHandRaiseBool()
    {
        _handRaised = model.handRaiseBool;
    }

    public void SetHandRaiseBool(bool hand)
    {
        model.handRaiseBool = hand;
    }

    public bool GetHandRaiseBool()
    {
        return model.handRaiseBool;
    }
}
