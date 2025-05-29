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
    private NoteChart _currentNoteChart;
    private float _currentSpeed;
    private float _timer;
    private int _correctNotesCount;
    private Coroutine _spawnNotesCoroutine;
    public void StartNoteChart(TextAsset noteChartAsset, float speed)
    {
        _currentSpeed = speed;
        _currentNoteChart = JsonUtility.FromJson<NoteChart>(noteChartAsset.text);
        _timer = 0f;
        _correctNotesCount = 0;
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
            }
        }
        yield return new WaitForSeconds(_finishTime);
        FinishSong();
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
    }

    public void AddCorrectNote()
    {
        _correctNotesCount++;
    }

    private void FinishSong()
    {
        if (_correctNotesCount >= _currentNoteChart.notes.Count * 0.7f)
        {
            _onWinSong?.Invoke(_correctNotesCount + " / " + _currentNoteChart.notes.Count);
        }
        else
        {
            _onLoseSong?.Invoke(_correctNotesCount + " / " + _currentNoteChart.notes.Count);
        }
    }
}
