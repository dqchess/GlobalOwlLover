using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPos : MonoBehaviour
{
    public LevelBuilder levelBuilder;

    // Start is called before the first frame update
   

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag=="Limit")
        {
            Vector3 lastPos = transform.position;
            transform.position = new Vector3(-transform.position.x, transform.position.y,-transform.position.z);
            levelBuilder.clearLevel=true;
            levelBuilder.GenerateLevel=true;
        }
    }

   
}
