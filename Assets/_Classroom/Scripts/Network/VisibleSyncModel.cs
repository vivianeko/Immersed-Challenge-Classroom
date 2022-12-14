using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;

[RealtimeModel]
public partial class VisibleSyncModel
{
    [RealtimeProperty(1, true, true)] private int _visible;
}

/* ----- Begin Normal Autogenerated Code ----- */
public partial class VisibleSyncModel : RealtimeModel {
    public int visible {
        get {
            return _visibleProperty.value;
        }
        set {
            if (_visibleProperty.value == value) return;
            _visibleProperty.value = value;
            InvalidateReliableLength();
            FireVisibleDidChange(value);
        }
    }
    
    public delegate void PropertyChangedHandler<in T>(VisibleSyncModel model, T value);
    public event PropertyChangedHandler<int> visibleDidChange;
    
    public enum PropertyID : uint {
        Visible = 1,
    }
    
    #region Properties
    
    private ReliableProperty<int> _visibleProperty;
    
    #endregion
    
    public VisibleSyncModel() : base(null) {
        _visibleProperty = new ReliableProperty<int>(1, _visible);
    }
    
    protected override void OnParentReplaced(RealtimeModel previousParent, RealtimeModel currentParent) {
        _visibleProperty.UnsubscribeCallback();
    }
    
    private void FireVisibleDidChange(int value) {
        try {
            visibleDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    protected override int WriteLength(StreamContext context) {
        var length = 0;
        length += _visibleProperty.WriteLength(context);
        return length;
    }
    
    protected override void Write(WriteStream stream, StreamContext context) {
        var writes = false;
        writes |= _visibleProperty.Write(stream, context);
        if (writes) InvalidateContextLength(context);
    }
    
    protected override void Read(ReadStream stream, StreamContext context) {
        var anyPropertiesChanged = false;
        while (stream.ReadNextPropertyID(out uint propertyID)) {
            var changed = false;
            switch (propertyID) {
                case (uint) PropertyID.Visible: {
                    changed = _visibleProperty.Read(stream, context);
                    if (changed) FireVisibleDidChange(visible);
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
        _visible = visible;
    }
    
}
/* ----- End Normal Autogenerated Code ----- */
