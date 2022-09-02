using UnityEngine;
using Normal.Realtime;
using TMPro;

public class NameSync : RealtimeComponent<NameSyncModel>
{
    private string _updatedName;
    [SerializeField] private TextMeshPro _avatarName;

    private void Awake()
    {
        _updatedName = GameObject.FindGameObjectWithTag("Manager").GetComponent<NameChange>().GetName();
    }

    private void Start()
    {
        _avatarName.text = _updatedName;
    }

    protected override void OnRealtimeModelReplaced(NameSyncModel previousModel, NameSyncModel currentModel)
    {
        if (previousModel != null)
        {
            previousModel.nameDidChange -= NameDidChange;
        }

        if (currentModel != null)
        {
            if (currentModel.isFreshModel)
                currentModel.name = _updatedName;
            UpdateName();
            currentModel.nameDidChange += NameDidChange;
        }
    }

    private void NameDidChange(NameSyncModel model, string value)
    {
        UpdateName();
    }

    private void UpdateName()
    {
        _updatedName = model.name;
    }

    public string GetName()
    {
        return model.name;
    }
}
