using UnityEngine;
using Normal.Realtime;
using UnityEngine.UI;

public class VisibleSync : RealtimeComponent<VisibleSyncModel>
{
    private int _visible;
    public int _maxVisibleAvatars;
    private MuteBoolSync _muteBoolSync;
    private MuteClientIdSync _muteIdSync;
    [SerializeField] private Button _unmuteButton;

    [SerializeField] private RealtimeAvatarManager _avatarManager;


    private void OnEnable()
    {
        _avatarManager.avatarCreated += AvatarChangeUpdate;
        _avatarManager.avatarDestroyed += AvatarChangeUpdate;
    }

    private void Start()
    {
        _muteBoolSync = GetComponent<MuteBoolSync>();
        _muteIdSync = GetComponent<MuteClientIdSync>();
    }

    private void AvatarChangeUpdate(RealtimeAvatarManager avatarManager, RealtimeAvatar avatar, bool isLocal)
    {
        if (_visible == _maxVisibleAvatars && avatar.gameObject.GetComponent<Role>().GetRole() != "Professor")
        {
            _muteBoolSync.SetMuteBool(true);
            _muteIdSync.SetMuteClientId(avatar.ownerIDInHierarchy);
        }
            
    }

    protected override void OnRealtimeModelReplaced(VisibleSyncModel previousModel, VisibleSyncModel currentModel)
    {
        if (previousModel != null)
        {
            previousModel.visibleDidChange -= VisibleDidChange;
        }

        if (currentModel != null)
        {
            if (currentModel.isFreshModel)
                currentModel.visible = _visible;
            UpdateVisible();
            currentModel.visibleDidChange += VisibleDidChange;
        }
    }

    private void VisibleDidChange(VisibleSyncModel model, int value)
    {
        UpdateVisible();
    }

    private void UpdateVisible()
    {
        _visible = model.visible;
    }

    public void AddVisible()
    {
        model.visible += 1;
    }

    public void RemoveVisible()
    {
        model.visible -= 1;
    }

    public int GetVisible()
    {
        return model.visible;
    }

    public void CheckVisible()
    {

        if (_visible == _maxVisibleAvatars)
            _unmuteButton.interactable = false;
        if (_visible < _maxVisibleAvatars)
            _unmuteButton.interactable = true;
    }

}
