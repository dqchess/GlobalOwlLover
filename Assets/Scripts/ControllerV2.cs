using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerV2 : MonoBehaviour
{
    public float startSpeed,maxSpeed, rotateSpeed, transitionXSpeed,transitionYSpeed;
    private float speed;

    public GameObject Pivot;

    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        speed =startSpeed;
        offset = transform.position-Camera.main.transform.position;


        
    }

  
 float ClampAngle(float angle, float from, float to)
     {
         // accepts e.g. -80, 80
         if (angle < 0f) angle = 360 + angle;
         if (angle > 180f) return Mathf.Max(angle, 360+from);
         return Mathf.Min(angle, to);
     }

    // Update is called once per frame
    void Update()
    {
        float h, v;
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        transform.position += transform.forward*speed*Time.deltaTime;

        
        Pivot.transform.position = transform.position;

        Debug.Log(transform.eulerAngles.x);

    
            Vector3 rot = transform.rotation.eulerAngles + new Vector3(v*Time.deltaTime*rotateSpeed, h*Time.deltaTime*rotateSpeed, 0f); //use local if your char is not always oriented Vector3.up
            rot.x = ClampAngle(rot.x, -60f, 90f);
            rot.y = ClampAngle(rot.y, -90f, 90f);
         
            
            
           
            Pivot.transform.rotation =  Quaternion.Lerp(Pivot.transform.rotation, transform.rotation, Time.deltaTime / transitionXSpeed);

            
           
                   

            
           
        
        

        //Pivot.transform.position = transform.position;
        //Pivot.transform
        
    }
}
