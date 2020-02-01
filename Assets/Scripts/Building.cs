using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool isDeconstruct = false;

    public void Reconstruct()
    {
        if (!isDeconstruct)
            return;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
            transform.GetChild(i).GetComponent<Collider>().enabled = true;

            for (int j = 0; j < transform.GetChild(i).transform.childCount; j++)
            {
                transform.GetChild(i).transform.GetChild(j).gameObject.SetActive(false);
            }

            Rigidbody[] rbs = transform.GetChild(i).GetComponentsInChildren<Rigidbody>();

            foreach (Rigidbody childRb in rbs)
            {
                childRb.constraints = RigidbodyConstraints.FreezeAll;
                childRb.useGravity = false;
                childRb.gameObject.SetActive(false);
            }
        }
    }
}
