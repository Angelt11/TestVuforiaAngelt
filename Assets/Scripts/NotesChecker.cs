using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NotesChecker : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onButtonPressed;
    [SerializeField]
    private UnityEvent onCorrectNote;
    [SerializeField]
    private UnityEvent onFailNote;
    private List<GameObject> notes = new List<GameObject>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            notes.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            notes.Remove(collision.gameObject);
        }
    }
    public void DestroyNotes()
    {
        onButtonPressed?.Invoke();
        GameObject noteToDestroy = null;
        int indexToRemove = -1;
        for (int i = 0; i < notes.Count; i++)
        {
            if (notes[i] != null)
            {
                noteToDestroy = notes[i];
                indexToRemove = i;
                break;
            }
        }

        if (noteToDestroy != null)
        {
            onCorrectNote?.Invoke();
            notes.RemoveAt(indexToRemove);
            Destroy(noteToDestroy);
        }
        else
        {
            onFailNote?.Invoke();
        }
    }

}
