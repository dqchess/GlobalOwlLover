using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_manager : MonoBehaviour
{
    public BombThrower owl_stuff;
    public Slider altitudeSlider;
    public Image sliderHandle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        altitudeSlider.value = RemapClamp(owl_stuff.altitude, 0, 400, 0, 1);
        if(owl_stuff.altitude<owl_stuff.palier1)
        {
            //Red
            sliderHandle.color = Color.red;
        }
        else if(owl_stuff.altitude<owl_stuff.palier2)
        {
            //yellow
            sliderHandle.color = Color.yellow;
        }
        else
        {
            //white
            sliderHandle.color = Color.white;
        }

    }


    public float Remap(float value, float low1, float high1, float low2, float high2)
    {
        //remaps value from [low1, high1] into [low2, high2]

        return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
    }
    public float RemapClamp(float value, float low1, float high1, float low2, float high2)
    {
        //remaps value from [low1, high1] into [low2, high2]

        return Mathf.Clamp(low2 + (value - low1) * (high2 - low2) / (high1 - low1), low2, high2);
    }
}
