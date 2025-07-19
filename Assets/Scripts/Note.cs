using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    private SpriteRenderer spriteRenderer;

    private bool isTouched = false;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
    public void WasFailed()
    {
        if (!isTouched)
        {
            isTouched = true;
            //Animator animator = GetComponent<Animator>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.gray;
            }
        }
    }
}
