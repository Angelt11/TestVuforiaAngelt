using UnityEngine;
using UnityEngine.Events;

public class Dance_controller: MonoBehaviour
{
    [SerializeField]
    private Animator _characterAnimator;
    [SerializeField]
    private UnityEvent _OnSelectDance;
    [SerializeField]
    private UnityEvent _OnDanceSelected;
    [SerializeField]
    private SoundData _currentSoundData;
    public void ActivateSelectDance()
    {
        _OnSelectDance?.Invoke();
    }
    public void OnSelectedDance()
    {
        _OnDanceSelected?.Invoke();
    } 
    public void OnselectedDance(SoundData soundData)
    {
        _OnDanceSelected?.Invoke();
    }

}
