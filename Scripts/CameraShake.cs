using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeTime = 1f;
    [SerializeField] float shakeIntensity = 0.5f;
    Vector3 initialPosition;
    void Start()
    {
        initialPosition = transform.position;
    }
    public void Play()
    {
        StartCoroutine(Shake());
    }
    IEnumerator Shake()
    {
        float elapsedTime = 0;
        while(elapsedTime < shakeTime)
        {
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeIntensity;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
