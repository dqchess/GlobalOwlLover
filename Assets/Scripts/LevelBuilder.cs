using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//[ExecuteInEditMode]
public class LevelBuilder : MonoBehaviour
{
    public bool GenerateLevel;

    public bool textureNoise;

    public List <Texture2D> maps;

    public int resolution;
    public Vector3 blockSize;
    public Vector2 blockOffset;
    public int minFloor,maxFloor;

    [Range(.0f,1.0f)]
    public float fillAmount;
    
    public List <GameObject>  Base,Top;
    
    public bool clearLevel;
    public List<GameObject> GeneratedBuildings;

    public float buildingCount;
    public float buildingDestroyed;

    private  float[] rotations =new float[4] {0,90,180,-90};

    public Text chaosText;
    public Text destroyedBuildingText;

    public static LevelBuilder Instance;

    void Awake()
    {
        if (Instance == null)
        {

            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        if(GenerateLevel)
        {
            buildingCount=0;
            BuildLevel(maps[Random.Range(0,maps.Count)]);
            GenerateLevel=false;
        }
    }

    

	void BuildLevel(Texture2D map)
	{
		for (int x = 0; x < map.width; x++)
		{
			for (int y = 0; y < map.height; y++)
			{
                float randomFill = Random.Range(0.0f,1.0f);
                
                if(randomFill<fillAmount)
                {
                int randomColor =Random.Range(0,Base.Count);

				   StartCoroutine(GenerateTile(x,y,randomColor,map));
                   buildingCount++;
                }
			}
		}
        GenerateLevel=false;
	}

	IEnumerator GenerateTile (int x, int z, int colorIndex,Texture2D map)
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            GeneratedBuildings.Clear();
            buildingCount = 0;
            buildingDestroyed = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        chaosText.text = Mathf.RoundToInt((buildingDestroyed / buildingCount * 100)).ToString() + " % of chaos";
        Debug.Log(buildingDestroyed);
        Debug.Log(buildingCount);
        destroyedBuildingText.text = buildingDestroyed.ToString() + " destroyed buildings";

        if(clearLevel)
        {
             if(GeneratedBuildings.Count > 0)
            {
                foreach(GameObject building in GeneratedBuildings)
                {
                    Destroy(building);
                }
                clearLevel=false;
            }
        }
        if(GenerateLevel)
        {
            buildingCount=0;
            BuildLevel(maps[Random.Range(0,maps.Count)]);
            GenerateLevel=false;
        }    
    }
	


   
}
