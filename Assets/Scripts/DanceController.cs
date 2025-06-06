using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DanceController : MonoBehaviour
{
    [SerializeField]
    private Animator _characterAnimator;
    [SerializeField]
    private NotesManager _notesManager;
    [SerializeField]
    private UnityEvent _onSelectDance;
    [SerializeField]
    private UnityEvent _onDanceSelected;
    [SerializeField]
    private string _failAnimationName;//="Fail"
    private Coroutine _resetDanceCouroutine;
    private SoundData _currentSoundData;
    public void ActivateSelectDance()
    {
        _onSelectDance?.Invoke();
    }
    public void OnSelectedDance(SoundData soundData)
    {
        _currentSoundData = soundData;
        _onDanceSelected?.Invoke();
    }
    public void StartDance()
    {
        _characterAnimator.Play(_currentSoundData.animationName);
        SoundManager.instance.PlayMusic(_currentSoundData.musicName);
        _notesManager.StartNoteChart(_currentSoundData.notesConfig, _currentSoundData.speed);
    }
    public void FailedNote()
    {
        if (_resetDanceCouroutine != null)
        {
            StopCoroutine(_resetDanceCouroutine);
        }
        _resetDanceCouroutine = StartCoroutine(ResetDance());
    }
    public IEnumerator ResetDance()
    {
        _characterAnimator.Play(_failAnimationName, 0, 0f);
        float _failAnimationLength = _characterAnimator.runtimeAnimatorController.
            animationClips.First(clip => clip.name == _failAnimationName).length;
        yield return new WaitForSeconds(_failAnimationLength);
        _characterAnimator.Play(_currentSoundData.animationName);
        _resetDanceCouroutine = null;
    }

}

