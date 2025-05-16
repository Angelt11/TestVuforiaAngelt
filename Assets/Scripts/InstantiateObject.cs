using UnityEngine;

public class InstantiateObject : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToInstantiate;
    public void Instantiate(Transform target)
    {
        Instantiate(objectToInstantiate, target.position, Quaternion.identity);
    }
}
