using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigLaser : MonoBehaviour
{
    LineRenderer laser;
    ParticleSystem ps;

    public GameObject owltransform;

    public float explosionRadius = 20f;
    public float explosionForce = 300f;

    void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();
        laser = GetComponentInChildren<LineRenderer>();

    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        laser.SetPosition(0, owltransform.transform.position);
        if(Physics.Raycast(ray, out hit))
        {
            laser.enabled = true;
            laser.SetPosition(1, hit.point);
            ps.Play();
            ps.transform.position = hit.point;
            SetExplosion(hit.point);
        }
        else
        {
            laser.enabled = false;
        }
    }
    private void SetExplosion(Vector3 pos)
    {
        Vector3 explosionPos = pos;
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

                    b.isDeconstruct = true;
                }
            }
        }
    }

}
