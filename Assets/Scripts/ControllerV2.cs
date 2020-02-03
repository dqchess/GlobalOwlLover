using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerV2 : MonoBehaviour
{
    public float startSpeed,maxSpeed, rotateXSpeed, rotateYSpeed, transitionXSpeed,transitionYSpeed;
    private float speed;

    public GameObject Pivot;

     [SerializeField] private  Vector3 rot;

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

        //Debug.Log(transform.eulerAngles.x);

    
            rot = transform.rotation.eulerAngles + new Vector3(v*Time.deltaTime*rotateXSpeed, h*Time.deltaTime*rotateYSpeed, 0f); //use local if your char is not always oriented Vector3.up
            rot.x = ClampAngle(rot.x, -60f, 90f);
        //rot.y = ClampAngle(rot.y, -90f, 90f);


        transform.rotation = Quaternion.Euler(rot);

        //Quaternion.Euler(Mathf.Lerp(Pivot.transform.rotation.x, transform.rotation.x, Time.deltaTime / transitionXSpeed), Mathf.Lerp(Pivot.transform.rotation.y, transform.rotation.y, Time.deltaTime / transitionYSpeed), Mathf.Lerp(Pivot.transform.rotation.z, transform.rotation.z, Time.deltaTime));
        Pivot.transform.rotation = Quaternion.Lerp(Pivot.transform.rotation, transform.rotation, Time.deltaTime * transitionXSpeed);
        //Pivot.transform.rotation =Quaternion.Euler( new Vector3( Mathf.Lerp(Pivot.transform.eulerAngles.x, transform.eulerAngles.x, Time.deltaTime * transitionXSpeed),
         //   Mathf.Lerp(Pivot.transform.eulerAngles.y, transform.eulerAngles.y, Time.deltaTime * transitionYSpeed),0));


            
           
                   

            
           
        
        

        //Pivot.transform.position = transform.position;
        //Pivot.transform
        
    }
}
