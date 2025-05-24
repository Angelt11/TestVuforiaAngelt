using UnityEngine;

public class Lane : MonoBehaviour
{
    [SerializeField]
    private Transform _notesOrigin;
    public Transform NotesOrigin
    {
        get { return _notesOrigin; }
    }
    [SerializeField]
    private GameObject _notePrefab;
    public GameObject NotePrefab
    {
        get { return _notePrefab; }
    }
}
