using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private UnityEvent<string> _onSecondPassed;
    [SerializeField]
    private UnityEvent _onTimerFinished;
    private Coroutine _timerCoroutine;
    private void StartTimer(float duration)
        
    {
        _timerCoroutine = StartCoroutine(RunTimer(duration));
    }
    private IEnumerator RunTimer(float duration)
    {
        _onSecondPassed?.Invoke("" + duration);
        yield return new WaitForSeconds(duration);
        duration--; 
        if (duration > 0)
        {
            _timerCoroutine = StartCoroutine(RunTimer(duration));
        }
        else
        {
            _timerCoroutine = null;
            _onTimerFinished?.Invoke();
        }
    }
    public void StopTimer()
    {
        if (_timerCoroutine != null)
        {
            StopCoroutine(_timerCoroutine);
            _timerCoroutine = null;
        }
    }

}
