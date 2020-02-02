using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Bomb : MonoBehaviour
{
    [HideInInspector] public Vector3 target;
    public GameObject particleExplosion;
    public float explosionRadius = 20f;
    public float explosionForce = 100000f;
    public float speed = 100f;
    void Start()
    {
        target = (transform.position - target).normalized;
    }

    void Update()
    {
        //this.gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,target,speed * Time.deltaTime);
        transform.position += -target * Time.deltaTime * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {

        Instantiate(particleExplosion, transform.position, Quaternion.identity, null);
        SetExplosion();            
        Destroy(this.gameObject);

    }
    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void SetExplosion()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider hit in colliders)
        {
            //la bombe a touché un building ?
            Building b = hit.gameObject.transform.parent?.GetComponent<Building>();
            if (b != null)
            {               
                if (!b.isDeconstruct)
                {
                    //pour chaque étage
                    for (int i = 0; i < hit.gameObject.transform.parent.childCount; i++)
                    {
                        Transform etage = hit.gameObject.transform.parent.GetChild(i);
                        etage.GetComponent<MeshRenderer>().enabled = false;
                        etage.GetComponent<Collider>().enabled = false;

                        //chaque petit morceau
                        for (int j = 0; j < etage.transform.childCount; j++)
                        {
                            etage.transform.GetChild(j).gameObject.SetActive(true);
                        }
                        Rigidbody[] rbs = etage.GetComponentsInChildren<Rigidbody>();

                        foreach (Rigidbody childRb in rbs)
                        {
                            childRb.constraints = RigidbodyConstraints.None;
                            childRb.useGravity = true;
                            childRb.AddExplosionForce(explosionForce, explosionPos, explosionRadius, 3.0f);
                            
                        }
                    }
                    LevelBuilder.Instance.buildingDestroyed++;
                    b.isDeconstruct = true;
                }                             
            }
        }
    }
}
