using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MFlight.Demo;
using UnityEngine.UI;
using DG.Tweening;

public class BombThrowerV2 : MonoBehaviour
{
    public GameObject bomb;
    public GameObject bombSpawner;
    private Animator anim;
    public GameObject eyeLasers;
    public GameObject bigLaser;

    public Transform eyeL;
    public Transform eyeR;

    public float altitude;
    public float palier1 = 150f;
    public float palier2 = 250f;
    public float palierMax = 500f;

    public int indexWeapon = 0;

    float timerBomb = 0f;
    float timerEyeLaser = 0f;
    float timerBigLaser = 0f;

    public float cooldownBomb = 1f;
    public float cooldownEyeLaser = 3f;
    public float cooldownBigLaser = 7f;

    public Text textBomb;
    public Text textEyeLaser;
    public Text textBigLaser;

    private GameObject bl;


    private void Start()
    {
        anim = GetComponentInChildren<Animator>();

    }
    void Update()
    {
        UpdateUI();
        altitude = transform.position.y;
        timerBomb += Time.deltaTime;
        timerEyeLaser += Time.deltaTime;
        timerBigLaser += Time.deltaTime;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            indexWeapon++;
            if (indexWeapon > 2)
                indexWeapon = 0;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            indexWeapon--;
            if (indexWeapon < 0)
                indexWeapon = 2;
        }


        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                Shoot(hit.point);
                anim.SetTrigger("Shoot");
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (bl != null)
                Destroy(bl);
        }
    }

    public void UpdateUI()
    {
        Color cLessAlpha = new Color(255, 255, 255, 0.5f);
        Color c = new Color(255, 255, 255, 255);
        switch (indexWeapon)
        {
            case (0):
                textBomb.transform.DOScale(Vector3.one * 1.2f, 0.3f);
                textBomb.DOColor(c, 0f);
                textEyeLaser.transform.DOScale(Vector3.one, 0.3f);
                textEyeLaser.DOColor(cLessAlpha, 0f);
                textBigLaser.transform.DOScale(Vector3.one, 0.3f);
                textBigLaser.DOColor(cLessAlpha, 0f);
                break;
            case (1):
                textBomb.transform.DOScale(Vector3.one, 0.3f);
                textBomb.DOColor(cLessAlpha, 0f);
                textEyeLaser.transform.DOScale(Vector3.one * 1.2f, 0.3f);
                textEyeLaser.DOColor(c, 0f);
                textBigLaser.transform.DOScale(Vector3.one, 0.3f);
                textBigLaser.DOColor(cLessAlpha, 0f);
                break;
            case (2):
                textBomb.transform.DOScale(Vector3.one, 0.3f);
                textBomb.DOColor(cLessAlpha, 0f);
                textEyeLaser.transform.DOScale(Vector3.one, 0.3f);
                textEyeLaser.DOColor(cLessAlpha, 0f);
                textBigLaser.transform.DOScale(Vector3.one * 1.2f, 0.3f);
                textBigLaser.DOColor(c, 0f);
                break;
        }
    }

    private void Shoot(Vector3 p)
    {
        Camera.main.transform.parent.DOShakePosition(5f, 0.5f, 5, 90);
        if (indexWeapon == 2) //big laser
       {
            bl = Instantiate(bigLaser, Vector3.zero, Quaternion.identity, null);
            bl.GetComponent<BigLaser>().owltransform = bombSpawner;
            bl.GetComponent<BigLaser>().owl = gameObject;
            timerBigLaser = 0;           
       }
       else if(indexWeapon == 1) //eye laser
       {
            StartCoroutine(EyeLaserSalve(p));
            
            timerEyeLaser = 0;
       }
       else if(indexWeapon == 0) //bomb
       {
            Instantiate(bomb, transform.position, Quaternion.identity, null);
            bomb.GetComponent<Bomb>().target = p;
            timerBomb = 0;
        }
    }

    IEnumerator EyeLaserSalve(Vector3 p)
    {
        Instantiate(eyeLasers, eyeL.position, Quaternion.identity, transform);
        Instantiate(eyeLasers, eyeR.position, Quaternion.identity, transform);
        eyeLasers.GetComponent<EyeLaser>().target = p;

        yield return new WaitForSeconds(0.1f);

        Instantiate(eyeLasers, eyeL.position, Quaternion.identity, transform);
        Instantiate(eyeLasers, eyeR.position, Quaternion.identity, transform);
        eyeLasers.GetComponent<EyeLaser>().target = p;

        yield return new WaitForSeconds(0.1f);

        Instantiate(eyeLasers, eyeL.position, Quaternion.identity, transform);
        Instantiate(eyeLasers, eyeR.position, Quaternion.identity, transform);
        eyeLasers.GetComponent<EyeLaser>().target = p;
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
        
        Gizmos.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), direction);
    }
}
