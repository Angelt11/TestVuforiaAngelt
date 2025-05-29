using UnityEngine;
using UnityEngine.Events;

public class FailChecker : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _onfailnote;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            Destroy(collision.gameObject);
            _onfailnote?.Invoke();
        }
    }
}
