using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MFlight.Demo;

public class BombThrower : MonoBehaviour
{
    public GameObject bomb;
    public GameObject bombSpawner;
    public Hud hud; 
    private Animator anim;

    public GameObject eyeLasers;
    public GameObject bigLaser;

    public Transform eyeL;
    public Transform eyeR;

    public float altitude;
    public float palier1 = 150f;
    public float palier2 = 250f;

    float timerBomb = 0f;
    float timerEyeLaser = 0f;
    float timerBigLaser = 0f;

    public float cooldownBomb = 3f;
    public float cooldownEyeLaser = 3f;
    public float cooldownBigLaser = 7f;
    

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        altitude = transform.position.y;
        timerBomb += Time.deltaTime;
        timerEyeLaser += Time.deltaTime;
        timerBigLaser += Time.deltaTime;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse0)&& CanShoot())
        {
            if (Physics.Raycast(ray, out hit))
            {
                Shoot(hit.point);
                anim.SetTrigger("Shoot");
            }
        }
    }
    /*
    private void SpawnBomb(Vector3 p)
    {
        Instantiate(bomb, transform.position,Quaternion.identity,null);
        bomb.GetComponent<Bomb>().target = p;
    }
    private void SpawnEyeLaser(Vector3 p)
    {
        Instantiate(bomb, transform.position, Quaternion.identity, null);
        bomb.GetComponent<Bomb>().target = p;
    }
    private void SpawnBigLaser(Vector3 p)
    {
        Instantiate(bomb, transform.position, Quaternion.identity, null);
        bomb.GetComponent<Bomb>().target = p;
    }*/
    private void Shoot(Vector3 p)
    {
       if(altitude<palier1)
        {
            //Shoot big laser
            GameObject g = GameObject.Instantiate(bigLaser, transform.position, Quaternion.identity, null);
            g.GetComponent<BigLaser>().spawner = bombSpawner.transform;
            timerBigLaser = 0;
            Debug.Log("BigLaser");
        }
       else if(altitude<palier2)
        {
            //Shoot eye laser
            GameObject g = GameObject.Instantiate(eyeLasers, eyeL.position, Quaternion.identity, null);
            GameObject h = GameObject.Instantiate(eyeLasers, eyeR.position, Quaternion.identity, null);
            g.GetComponent<EyeLaser>().target = p;
            h.GetComponent<EyeLaser>().target = p;
            timerEyeLaser = 0;
            Debug.Log("EyeLaser");
        }
       else
        {
            //Shoot bomb
            GameObject g = GameObject.Instantiate(bomb, transform.position, Quaternion.identity, null);
            g.GetComponent<Bomb>().target = p;
            timerBomb = 0;
            Debug.Log("Bomb");
        }
    }
    bool CanShoot()
    {
        bool output;
        if (altitude < palier1)
        {
            if (timerBigLaser > cooldownBigLaser)
                output = true;
            else
                output = false;
        }
        else if (altitude < palier2)
        {
            if (timerEyeLaser > cooldownEyeLaser)
                output = true;
            else
                output = false;
        }
        else
        {
            if (timerBomb > cooldownBomb)
                output = true;
            else
                output = false;
        }
        return output;
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        Gizmos.DrawRay(hud.mousePos.position, direction);
    }
}
