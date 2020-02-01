using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Bomb : MonoBehaviour
{
    [HideInInspector] public Vector3 target;
    public float explosionRadius = 20f;
    public float explosionForce = 100000f;
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        transform.DOMove(target,2f).OnComplete(() => SetExplosion());
    }

    void Update()
    {
        
    }

    private void SetExplosion()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        //Debug.Log(colliders.Length);
        foreach (Collider hit in colliders)
        {
            if(hit.isTrigger)
            {
                hit.GetComponent<MeshRenderer>().enabled=false;
                
                for(int i = 0; i< hit.transform.childCount;i++)
                {
                    hit.transform.GetChild(i).gameObject.SetActive(true);
                }
                Rigidbody[] rbs = hit.GetComponentsInChildren<Rigidbody>();
                
                
                
                foreach(Rigidbody childRb in rbs)
                {                    
                    childRb.constraints =RigidbodyConstraints.None;
                    childRb.useGravity =true;
                    childRb.AddExplosionForce(explosionForce, explosionPos, explosionRadius, 3.0f);

                }

            }

            //Rigidbody rb = hit.GetComponent<Rigidbody>();

            //if (rb != null)
             //   rb.AddExplosionForce(explosionForce, explosionPos, explosionRadius, 3.0f);
        }
    }
}
