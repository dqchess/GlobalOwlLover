using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[ExecuteInEditMode]
public class LevelBuilder : MonoBehaviour
{
    public bool GenerateLevel;

    public bool textureNoise;

    public Texture2D map;

    public int resolution;
    public Vector3 blockSize;
    public Vector2 blockOffset;
    public int minFloor,maxFloor;

    [Range(.0f,1.0f)]
    public float fillAmount;
    
    public List <GameObject>  Base,Top;
    
    public bool clearLevel;
    public List<GameObject> GeneratedBuildings;

    public int BuildingsCount;

    private  float[] rotations =new float[4] {0,90,180,-90};

    

    void Start()
    {
        if(GenerateLevel)
        {
            BuildingsCount=0;
            BuildLevel();
        }
    }

   
    

   

	void BuildLevel()
	{
		for (int x = 0; x < map.width; x++)
		{
			for (int y = 0; y < map.height; y++)
			{
                float randomFill = Random.Range(0.0f,1.0f);
                
                if(randomFill<fillAmount)
                {
                int randomColor =Random.Range(0,Base.Count);

				   StartCoroutine(GenerateTile(x,y,randomColor));
                   BuildingsCount++;
                }
			}
		}
        GenerateLevel=false;
 
	}

	IEnumerator GenerateTile (int x, int z, int colorIndex)
	{
		Color pixelColor = map.GetPixel(x, z);

        float h,s,v;
        
        Color.RGBToHSV(pixelColor,out h,out s,out v);

        int randomFloor;
        if(textureNoise)
        {
            randomFloor = (int)Mathf.Lerp(minFloor,maxFloor, v);
        }
        else
        {
            randomFloor = Random.Range(minFloor,maxFloor);
        }
        
        

		if (pixelColor == Color.black)
		{
			// The pixel is transparrent. Let's ignore it!
			yield return null;
		}
        else
        {
            GameObject building = new GameObject();
            for(int i = 0; i<=randomFloor; i++)
            {
                //top
                if(i==randomFloor)
                {
                    Vector3 rot = new Vector3(0,rotations[Random.Range(0,rotations.Length)]);
                    Vector3 pos = new Vector3(x*(blockSize.x+blockOffset.x)-(resolution*(blockSize.x+blockOffset.x)/2),randomFloor*blockSize.y,z*(blockSize.z+blockOffset.y)-(resolution*(blockSize.z+blockOffset.y)/2));
                    
                    GameObject topBuilding = Instantiate(Top[colorIndex],pos,Quaternion.Euler(rot));

                    GeneratedBuildings.Add(topBuilding);
                    topBuilding.transform.SetParent(building.transform);
                }
                //building
                else
                {
                    Vector3 rot = new Vector3(0,rotations[Random.Range(0,rotations.Length)]);
                    Vector3 pos = new Vector3(x*(blockSize.x+blockOffset.x)-(resolution*(blockSize.x+blockOffset.x)/2),i*blockSize.y,z*(blockSize.z+blockOffset.y)-(resolution*(blockSize.z+blockOffset.y)/2));
                    GameObject middleBuilding = Instantiate(Base[colorIndex],pos,Quaternion.Euler(rot));
                    GeneratedBuildings.Add(middleBuilding);
                    middleBuilding.transform.SetParent(building.transform);
                }                  
            }
            building.AddComponent<Building>();

            yield return null;
        }	
	}

    void Update()
    {
        if(clearLevel)
        {
             if(GeneratedBuildings.Count > 0)
            {
                foreach(GameObject building in GeneratedBuildings)
                {
                    DestroyImmediate(building);
                }
                clearLevel=false;
            }
        }    
    }
	


   
}
