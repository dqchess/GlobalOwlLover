using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigLaser : MonoBehaviour
{
    LineRenderer laser;
    ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        laser = GetComponentInChildren<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        laser.SetPosition(0, transform.position);
        if(Physics.Raycast(ray, out hit))
        {
            laser.enabled = true;
            laser.SetPosition(1, hit.point);
        }
        else
        {
            laser.enabled = false;
        }
    }
}
