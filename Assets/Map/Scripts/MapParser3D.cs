using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using UnityEngine.SceneManagement;


[System.Serializable]
public class xyz
{
    public int x;
	public int y;
	public int z;
}

[System.Serializable]
public class MapObject
{
    public string filename;
	public string category;
    public xyz position;
}

[System.Serializable]
public class MapHolder
{
	public List<MapObject> objects;
}

public class MapParser3D : MonoBehaviour {
	public string mapFile = "Assets/Map/map.json";	
	private MapHolder worldMap;

	public void loadMapFromJson() {
        if(File.Exists(mapFile)) {
            string dataFile = File.ReadAllText(mapFile); 
            worldMap = JsonUtility.FromJson<MapHolder>(dataFile);
        }
		else {
			Debug.Log("World map failed to load!");
		}
	}

	public void instantiateMapObject(MapObject mapObject) {
		 GameObject mapObjectPrefab = GameObject.Instantiate((GameObject)Resources.Load("MapPrefabs/" + mapObject.category + "/3D/" + mapObject.filename));
		 mapObjectPrefab.transform.position = new Vector3(mapObject.position.x, mapObject.position.y, mapObject.position.z);
	}

	public void instantiateMap() {
		foreach (MapObject mapObject in worldMap.objects) {
			instantiateMapObject(mapObject);
		}
	}

	void Start () {
		loadMapFromJson();
		instantiateMap();
	}
	
	void Update () {
		
	}
}
