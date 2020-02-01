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
        Debug.Log(colliders.Length);
        foreach (Collider hit in colliders)
        {

            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(explosionForce, explosionPos, explosionRadius, 3.0f);
        }
    }
}
