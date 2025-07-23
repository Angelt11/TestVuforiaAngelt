using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    private Image image;

    private bool isTouched = false;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    private void Awake()
    {
        image = GetComponent<Image>();
        if (image == null)
        {
            Debug.LogError("Image component not found on Note object.");
            image = gameObject.GetComponentInChildren<Image>();
        }
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
            if (image != null)
            {
                image.color = Color.gray;
            }
            else
            {
                Debug.LogWarning("Image component was not found.");
            }
        }
    }
}
