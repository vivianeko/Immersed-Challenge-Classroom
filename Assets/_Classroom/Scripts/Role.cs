using UnityEngine;

public class Role : MonoBehaviour
{
    [SerializeField] private string _role;

    public string GetRole()
    {
        return _role;
    }
}
