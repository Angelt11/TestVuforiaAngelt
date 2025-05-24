using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
}
