using System;
using System.Collections.Generic;
using MEC;
using UnityEngine;

public class C_ParticleSystemAutoDestroy : MonoBehaviour
{
    [SerializeField]
    private float time = 0.0f;

    private ParticleSystem ps;
    private float t = 0.0f;

    [Obsolete]
    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        if(ps != null) t = ps.duration + ps.startDelay;
    }

    private void Start()
    {
        if (time > 0.0f && time > t) t = time;
        Timing.RunCoroutine(_AutoDestroy());        
    }

    private IEnumerator<float> _AutoDestroy()
    {
        if(FightingGame.instance)
            yield return Timing.WaitForSeconds(t / FightingGame.instance.myTimeScale);
        if (this != null && gameObject != null) Destroy(gameObject);
    }
}
