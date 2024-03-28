using System.Collections;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Int3 lockpick_1, lockpick_2, lockpick_3;
    [SerializeField] private int openCombine, minPosition, maxPosition;
    [SerializeField] private float timeValue;
    [SerializeField] private TMP_Text lockText, timeText;

    private Int3 _lock;

    private void Start()
    {
        SetLockStartCombine();
    }

    private void SetLockStartCombine()
    {
        _lock = GetRandomCombine();
        DisplayLock();
        StartCoroutine(Timer());
    }

    private Int3 GetRandomCombine()
    {
        int maxRandom = maxPosition + 1;

        Int3 random = new Int3(
            Random.Range(minPosition, maxRandom),
            Random.Range(minPosition, maxRandom),
            Random.Range(minPosition, maxRandom));

        return random;
    }

    public void ApplyLockpick(int lockpickNum)
    {
        Int3 lockpick = new Int3();

        switch (lockpickNum)
        {
            case 0: lockpick = lockpick_1; break;
            case 1: lockpick = lockpick_2; break;
            case 2: lockpick = lockpick_3; break;
        }

        _lock = new Int3(
         Mathf.Clamp(_lock.a + lockpick.a, minPosition, maxPosition),
         Mathf.Clamp(_lock.b + lockpick.b, minPosition, maxPosition),
         Mathf.Clamp(_lock.c + lockpick.c, minPosition, maxPosition));

        CheckCombine();
        DisplayLock();
    }

    private void DisplayLock()
    {
        lockText.text = $" {_lock.a} {_lock.b} {_lock.c}";
    }

    private IEnumerator Timer()
    {
        float currentTime = timeValue;

        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1);
            currentTime -= 1f;
            UpdateTimerText(currentTime);
        }

        Debug.Log("Game Over!");
    }
    private void CheckCombine()
    {
        if (_lock.a == openCombine 
            && _lock.b == openCombine 
            && _lock.c == openCombine) Debug.Log("You Win!");
    }

    private void UpdateTimerText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
