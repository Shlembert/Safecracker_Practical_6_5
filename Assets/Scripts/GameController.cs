using System.Collections;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Int3 lockpick_1, lockpick_2, lockpick_3;
    [SerializeField] private int openCombine, minPosition, maxPosition;
    [SerializeField] private float timeValue;
    [SerializeField] private TMP_Text lockText, timeText;
    [SerializeField] private PinMuvement pinMuvement;
    [SerializeField] private ColorPinController colorPin;
    [SerializeField] private WindowsController windowsController;

    private Int3 _lock;
    private Coroutine timerCoroutine;

    private void Start()
    {
        SetLockStartCombine();
    }

    public void SetLockStartCombine()
    {
        _lock = GetRandomCombine();
        CheckPinPosition();
        DisplayLock(true);
        StartTimer();
    }

    private void StartTimer()
    {
        timerCoroutine = StartCoroutine(Timer());
    }

    private void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
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
        DisplayLock(false);
    }

    private async void DisplayLock(bool start)
    {
        lockText.text = $" {_lock.a} {_lock.b} {_lock.c}";
        await pinMuvement.MovePinAsync(_lock, start);
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

        windowsController.GameOver();
    }

    private void CheckCombine()
    {
        if (   _lock.a == openCombine
            && _lock.b == openCombine
            && _lock.c == openCombine)
        {
            windowsController.GameWin();
            StopTimer();
        }

        CheckPinPosition();
    }

    private void CheckPinPosition()
    {
        for (int i = 0; i < 3; i++)
        {
            bool isPinCorrect = GetPinValue(i) == openCombine;
            colorPin.SetColor(i, isPinCorrect);
        }
    }

    private int GetPinValue(int index)
    {
        switch (index)
        {
            case 0: return _lock.a;
            case 1: return _lock.b;
            case 2: return _lock.c;
            default: return 0;
        }
    }

    private void UpdateTimerText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
