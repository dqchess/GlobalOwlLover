using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MFlight.Demo;

public class BombThrower : MonoBehaviour
{
    public GameObject bomb;
    public GameObject bombSpawner;
    public Hud hud; 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(hud.mousePos.position);

            if (Physics.Raycast(ray, out hit))
            {
                SpawnBomb(hit.point);
            }
        }
    }

    private void SpawnBomb(Vector3 p)
    {
        Instantiate(bomb, transform.position,Quaternion.identity,null);
        bomb.GetComponent<Bomb>().target = p;
    }
}
