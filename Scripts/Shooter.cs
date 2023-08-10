using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject bullet;
    [SerializeField] float speed = 10f;
    [SerializeField] float LifeTime = 5f;
    [SerializeField] float baseFiringRate = 0.2f;
    public float timeFire = 0.2f;
    private float LastTime = 0f;

    [HideInInspector]
    public bool isFiring;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;
    public Transform bulletPos;
    Coroutine firingCoroutine;
    PlayerAudio playerAudio;
    void Awake()
    {
        playerAudio = FindObjectOfType<PlayerAudio>();
    }
    void Start()
    {
        if(useAI == true)
        {
            isFiring = true;
        }
    }
    void Update()
    {
        Fire();
    }
    void Fire()
    {
        if(isFiring && firingCoroutine == null && Time.time - LastTime > timeFire)
        {
            firingCoroutine = StartCoroutine(FireContinously());
            LastTime = Time.time;
        }
        else if(!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }    
    }
    IEnumerator FireContinously()
    {
        while(true)
        {
            GameObject instance = Instantiate(bullet,
                                            bulletPos.position,
                                            Quaternion.identity);
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if(rb != null && useAI != true)
            {
                rb.velocity = transform.up * speed;
            }
            if(rb != null && useAI == true)
            {
                rb.velocity = -transform.up * speed;
            }
            Destroy(instance, LifeTime);
            float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance,
                                        baseFiringRate + firingRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);
            playerAudio.PlayShootingClip();
            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }
}
