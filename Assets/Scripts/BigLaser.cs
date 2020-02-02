﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigLaser : MonoBehaviour
{
    LineRenderer laser;
    public ParticleSystem ps_start;
    public Transform spawner;
    public ParticleSystem ps_impact;

    float timer = 0f;
    public float laserDuration = 5f;
    // Start is called before the first frame update
    void Start()
    {
       
        laser = GetComponentInChildren<LineRenderer>();
        StartCoroutine(StartLaser());
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        transform.position = spawner.position;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        laser.SetPosition(0, Vector3.zero);
        if(Physics.Raycast(ray, out hit))
        {
            laser.enabled = true;
            laser.SetPosition(1, ray.direction*Vector3.Distance(spawner.position, hit.point));
            ps_impact.transform.position = hit.point+hit.normal;
            ps_impact.transform.LookAt(ps_impact.transform.position + hit.normal);
        }
        else
        {
            laser.enabled = false;
        }
        if(timer>laserDuration)
        {
            StartCoroutine(StopLaser());
        }
    }

    public IEnumerator StartLaser()
    {
        float k = 0;
        float duration = 0.5f;
        float maxWidth = laser.widthMultiplier;
        while(k<duration)
        {
            k += Time.deltaTime;
            laser.widthMultiplier = maxWidth * k / duration;
            yield return new WaitForEndOfFrame();
        }
        laser.widthMultiplier = maxWidth;
    }
    public IEnumerator StopLaser()
    {
        float k = 0;
        float duration = 1f;
        float maxWidth = laser.widthMultiplier;
        while (k < duration)
        {
            k += Time.deltaTime;
            laser.widthMultiplier = maxWidth *(1- k / duration);
            yield return new WaitForEndOfFrame();
        }
        laser.widthMultiplier = 0f;
        Destroy(this.gameObject);
    }
}