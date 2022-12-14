using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;

[RealtimeModel]
public partial class MuteBoolSyncModel
{
    [RealtimeProperty(1, true, true)] private bool _muteBool;
}

/* ----- Begin Normal Autogenerated Code ----- */
public partial class MuteBoolSyncModel : RealtimeModel {
    public bool muteBool {
        get {
            return _muteBoolProperty.value;
        }
        set {
            if (_muteBoolProperty.value == value) return;
            _muteBoolProperty.value = value;
            InvalidateReliableLength();
            FireMuteBoolDidChange(value);
        }
    }
    
    public delegate void PropertyChangedHandler<in T>(MuteBoolSyncModel model, T value);
    public event PropertyChangedHandler<bool> muteBoolDidChange;
    
    public enum PropertyID : uint {
        MuteBool = 1,
    }
    
    #region Properties
    
    private ReliableProperty<bool> _muteBoolProperty;
    
    #endregion
    
    public MuteBoolSyncModel() : base(null) {
        _muteBoolProperty = new ReliableProperty<bool>(1, _muteBool);
    }
    
    protected override void OnParentReplaced(RealtimeModel previousParent, RealtimeModel currentParent) {
        _muteBoolProperty.UnsubscribeCallback();
    }
    
    private void FireMuteBoolDidChange(bool value) {
        try {
            muteBoolDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    protected override int WriteLength(StreamContext context) {
        var length = 0;
        length += _muteBoolProperty.WriteLength(context);
        return length;
    }
    
    protected override void Write(WriteStream stream, StreamContext context) {
        var writes = false;
        writes |= _muteBoolProperty.Write(stream, context);
        if (writes) InvalidateContextLength(context);
    }
    
    protected override void Read(ReadStream stream, StreamContext context) {
        var anyPropertiesChanged = false;
        while (stream.ReadNextPropertyID(out uint propertyID)) {
            var changed = false;
            switch (propertyID) {
                case (uint) PropertyID.MuteBool: {
                    changed = _muteBoolProperty.Read(stream, context);
                    if (changed) FireMuteBoolDidChange(muteBool);
                    break;
                }
                default: {
                    stream.SkipProperty();
                    break;
                }
            }
            anyPropertiesChanged |= changed;
        }
        if (anyPropertiesChanged) {
            UpdateBackingFields();
        }
    }
    
    private void UpdateBackingFields() {
        _muteBool = muteBool;
    }
    
}
/* ----- End Normal Autogenerated Code ----- */
