using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManipulator : MonoBehaviour
{
    [SerializeField] float normalTime;
    [SerializeField] float slowedTime;
    [SerializeField] float duration;

    // Gets called from Enemy Impact State
    public void SlowTime()
    {
        StartCoroutine(SlowTimeEffect());
    }

    IEnumerator SlowTimeEffect()
    {
        Time.timeScale = slowedTime;
        yield return new WaitForSeconds(duration);
        Time.timeScale = normalTime;
    }
}
