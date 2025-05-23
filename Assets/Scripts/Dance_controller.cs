using UnityEngine;
using UnityEngine.Events;

public class Dance_controller: MonoBehaviour
{
    [SerializeField]
    private Animator _characterAnimator;
    [SerializeField]
    private UnityEvent _onSelectDance;
    [SerializeField]
    private UnityEvent _onDanceSelected;
    [SerializeField]
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
    }

}
