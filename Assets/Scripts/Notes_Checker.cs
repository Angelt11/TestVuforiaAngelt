using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Notes_Checker : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onButtonPressed;
    private List<GameObject> notes = new List<GameObject>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag ("Note"))
        {
            notes.Add(collision.gameObject);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag ("Note"))
        {
            notes.Remove(collision.gameObject);
        }
    }
    public void DestroyNotes()
    {
        onButtonPressed.Invoke();
        while (notes.Count > 0)
        {
            GameObject note = notes[0];
            notes.RemoveAt (0);
            Destroy(note);
        }
        notes.Clear();
    }
}

