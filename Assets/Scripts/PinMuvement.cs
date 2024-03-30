using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class PinMuvement : MonoBehaviour
{
    [SerializeField] private Transform pin1, pin2, pin3;
    [SerializeField] private float duration;
    [SerializeField] private List<Transform> targets;

    private async UniTask<float> GetPinYPositionAsync(int targetIndex)
    {
        Camera mainCamera = Camera.main;

        Vector3 targetWorldPosition = targets[targetIndex].position;
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(targetWorldPosition);
        Vector3 newScreenPoint = new Vector3(screenPoint.x, screenPoint.y, 0);
        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(newScreenPoint);

        await UniTask.DelayFrame(1); 
        return worldPoint.y;
    }

    public async UniTask MovePinAsync(Int3 pos, bool start)
    {
        float currentDuration;

        if (start) currentDuration = duration * 5f;
        else currentDuration = duration;

        await MovePinAsync(pin1, GetPinYPositionAsync(pos.a), currentDuration);
        await MovePinAsync(pin2, GetPinYPositionAsync(pos.b), currentDuration);
        await MovePinAsync(pin3, GetPinYPositionAsync(pos.c), currentDuration);
    }

    private async UniTask MovePinAsync(Transform pin, UniTask<float> targetYPositionTask, float duration)
    {
        float targetYPosition = await targetYPositionTask;
        pin.DOMoveY(targetYPosition, duration, false).SetEase(Ease.Linear);
    }
}
