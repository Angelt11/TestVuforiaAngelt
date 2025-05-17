using UnityEngine;
using UnityEngine.Events;

public class Dance_controller: MonoBehaviour
{
    [SerializeField]
    private UnityEvent _OnSelectDance;
    [SerializeField]
    private UnityEvent _OnDanceSelected;
    public void ActivateSelectDance()
    {
        _OnSelectDance?.Invoke();
    }
    public void OnSelectedDance()
    {
        _OnDanceSelected?.Invoke();
    } 

}
