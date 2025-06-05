using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class NotesManager : MonoBehaviour
{
    [SerializeField]
    private List<Lane> _lanes;
    [SerializeField]
    private float _notesCheckInterval = 0.1f;
    [SerializeField]
    private float _finishTime = 1.5f;
    [SerializeField]
    private UnityEvent<string> _onWinSong;
    [SerializeField]
    private UnityEvent<string> _onLoseSong;
    [SerializeField]
    private UnityEvent _onFinishSong;
    private NoteChart _currentNoteChart;
    private float _currentSpeed;
    private float _timer;
    private int _correctNotesCount;
    private int _notesCount;
    private int _currentNotesCount;
    private Coroutine _spawnNotesCoroutine;
    private List<GameObject> _instantiatedNotes = new List<GameObject>();
    public void StartNoteChart(TextAsset noteChartAsset, float speed)
    {
        _currentSpeed = speed;
        _currentNoteChart = JsonUtility.FromJson<NoteChart>(noteChartAsset.text);
        _timer = 0f;
        _correctNotesCount = 0;
        _notesCount = 0;
        _currentNotesCount = _currentNoteChart.notes.Count;
        _spawnNotesCoroutine = StartCoroutine(SpawnNotes());
    }
    private IEnumerator SpawnNotes()
    {
        while (_currentNoteChart.notes.Count > 0)
        {
            yield return new WaitForSeconds(_notesCheckInterval);
            _timer += _notesCheckInterval;

            List<NoteData> notesToSpawn = _currentNoteChart.notes.FindAll(note => note.time <= _timer);
            foreach (NoteData noteData in notesToSpawn)
            {
                _currentNoteChart.notes.Remove(noteData);
                if (noteData.lane < 0 || noteData.lane >= _lanes.Count)
                {
                    Debug.LogWarning("Invalid lane index: " + noteData.lane);
                    continue;
                }
                Lane currentLane = _lanes[noteData.lane];
                GameObject noteObject = Instantiate(currentLane.NotePrefab, currentLane.NotesOrigin.position, Quaternion.identity);

                Note note = noteObject.GetComponent<Note>();
                note.transform.SetParent(currentLane.transform);
                note.transform.localScale = Vector3.one;
                note.Speed = _currentSpeed;

                _instantiatedNotes.Add(noteObject);
            }
        }
        _spawnNotesCoroutine = null;
    }

    public void StopNotes()
    {
        if (_spawnNotesCoroutine != null)
        {
            StopCoroutine(_spawnNotesCoroutine);
            _spawnNotesCoroutine = null;
        }
        _currentNoteChart = null;
        _timer = 0f;
        while (_instantiatedNotes.Count > 0)
        {
            GameObject note = _instantiatedNotes[0];
            _instantiatedNotes.RemoveAt(0);
            if (note != null)
            {
                Destroy(note);
            }
            
        }
    }

    public void AddCorrectNote()
    {
        _correctNotesCount++;
        AddNoteCount();
    }

    public void AddNoteCount()
    {
        _notesCount++;
        if (_notesCount >= _currentNotesCount)
        {
            StartCoroutine(FinishSong());
        }
    }

    public void RemoveCorrectNote()
    {
        if (_correctNotesCount > 0)
        {
            _correctNotesCount--;
        }
    }

    private IEnumerator FinishSong()
    {
        yield return new WaitForSeconds(_finishTime);
        _onFinishSong?.Invoke();
        if (_correctNotesCount >= _currentNotesCount * 0.7f)
        {
            _onWinSong?.Invoke(_correctNotesCount + " / " + _currentNotesCount);
        }
        else
        {
            _onLoseSong?.Invoke(_correctNotesCount + " / " + _currentNotesCount);
        }
    }
}
