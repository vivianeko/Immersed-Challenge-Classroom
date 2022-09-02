using UnityEngine;
using Normal.Realtime;
using UnityEngine.XR.Interaction.Toolkit;

public class AvatarSync : RealtimeComponent<AvatarSyncModel>
{
    private RealtimeAvatarManager _avatarManager;
    private int _avatarPrefabIndex;
    private int _avatarType;

    [SerializeField] private GameObject _realtime;
    [SerializeField] private Transform _xrOrigin;
    [SerializeField] private Transform _professorSpawnPoint;
    [SerializeField] private GameObject _professorMenu;
    [SerializeField] private GameObject _professorAvatar;
    [SerializeField] private GameObject _studentSpawnArea;
    [SerializeField] private GameObject _studentMenu;
    [SerializeField] private XRDirectInteractor _leftXrDirect;
    [SerializeField] private XRDirectInteractor _rightXrDirect;
    [SerializeField] private GameObject[] _studentAvatar;

    private Vector3 _studentSpawnPoint;

    private void Awake()
    {
        _avatarManager = _realtime.GetComponent<RealtimeAvatarManager>();
    }

    protected override void OnRealtimeModelReplaced(AvatarSyncModel previousModel, AvatarSyncModel currentModel)
    {
        if (previousModel != null)
        {
            previousModel.avatarTypeDidChange -= AvatarDidChange;
        }

        if (currentModel != null)
        {
            if (currentModel.isFreshModel)
                currentModel.avatarType = _avatarPrefabIndex;
            UpdateAvatarIndex();
            currentModel.avatarTypeDidChange += AvatarDidChange;
        }
    }

    private void AvatarDidChange(AvatarSyncModel model, int value)
    {
        UpdateAvatarIndex();
    }

    private void UpdateAvatarIndex()
    {
        _avatarPrefabIndex = model.avatarType;
    }

    public void SetAvatar(int type)
    {
        _avatarType = type;
    }

    private Vector3 RandomStudentPosition()
    {
        Mesh area = _studentSpawnArea.GetComponent<MeshFilter>().mesh;
        Bounds bounds = area.bounds;
        Transform areaTransform = _studentSpawnArea.transform;
        float minX = bounds.size.x * -0.5f;
        float minZ = bounds.size.z * -0.5f;
        _studentSpawnPoint = areaTransform.TransformPoint(new Vector3(Random.Range(minX, -minX), areaTransform.position.y, Random.Range(minZ, -minZ)));
        return _studentSpawnPoint;
    }

    public void SpawnAvatar()
    {
        if (_avatarType == 1)
        {
            SpawnAvatarProfessor();
        }
        if (_avatarType == 2)
        {
            SpawnAvatarStudent();
        }        
    }

    private void SpawnAvatarProfessor()
    {
        _avatarPrefabIndex = 0;
        _avatarManager.localAvatarPrefab = _professorAvatar;
        _xrOrigin.position = _professorSpawnPoint.position;
        _xrOrigin.rotation = _professorSpawnPoint.rotation;
        _professorMenu.SetActive(true);
        _studentMenu.SetActive(false);
        if (_leftXrDirect != null)
        {
            _leftXrDirect.interactionLayers = InteractionLayerMask.GetMask("Special Object", "Object");
            _rightXrDirect.interactionLayers = InteractionLayerMask.GetMask("Special Object", "Object");
        }        
        Connect();
    }

    private void SpawnAvatarStudent()
    {
        _avatarPrefabIndex = Random.Range(0, _studentAvatar.Length);
        _avatarManager.localAvatarPrefab = _studentAvatar[_avatarPrefabIndex];
        RandomStudentPosition();
        _xrOrigin.position = _studentSpawnPoint;
        _professorMenu.SetActive(false);
        _studentMenu.SetActive(true);
        if (_leftXrDirect != null)
        {
            _leftXrDirect.interactionLayers = InteractionLayerMask.GetMask("Object");
            _rightXrDirect.interactionLayers = InteractionLayerMask.GetMask("Object");
        }
        Connect();
    }

    private void Connect()
    {
        _realtime.GetComponent<Realtime>().Connect("Classroom");
    }
}
