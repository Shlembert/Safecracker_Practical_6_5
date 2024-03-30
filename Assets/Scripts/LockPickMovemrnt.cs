using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class LockPickMovemrnt : MonoBehaviour
{
    [SerializeField] private List<Transform> lockPicts;
    [SerializeField] private Transform target;
    [SerializeField] private float angle, duration;

    private List<Vector2> _lockPickOrigPos;

    private void Start()
    {
        _lockPickOrigPos = new List<Vector2>();

        for (int i = 0; i < lockPicts.Count; i++)
        {
            _lockPickOrigPos.Add(lockPicts[i].position);
        }
    }

    public void MoveLockPick(int index)
    {
        lockPicts[index].DORotate(new Vector3(0, 0, angle), duration);
        lockPicts[index].DOMove(target.position, duration, false).OnComplete(() =>
        {
            lockPicts[index].DORotate(Vector3.zero, duration);
            lockPicts[index].DOMove(_lockPickOrigPos[index], duration, false);
        });
    }
}
