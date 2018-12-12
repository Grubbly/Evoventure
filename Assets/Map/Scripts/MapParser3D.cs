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
	public int rotation;
}

[System.Serializable]
public class MapHolder
{
	public List<MapObject> objects;
}

public class MapParser3D : MonoBehaviour {
	public string mapFile = "Assets/Map/map.json";	
	private MapHolder worldMap;

	private List <GameObject> loadedObjects;

	public void loadMapFromJson() {
        if(File.Exists(mapFile)) {
            string dataFile = File.ReadAllText(mapFile); 
            worldMap = JsonUtility.FromJson<MapHolder>(dataFile);
        }
		else {
			Debug.Log("World map failed to load!");
		}
	}

	public GameObject updateMapObject(MapObject mapObject, GameObject mapObjectPrefab) {
		mapObjectPrefab.transform.rotation = gameObject.transform.rotation;
		mapObjectPrefab.transform.Rotate(0, mapObject.rotation, 0);
		mapObjectPrefab.transform.position = new Vector3(gameObject.transform.position.x + mapObject.position.x * Mathf.Sin(gameObject.transform.rotation.y), 
		 												  gameObject.transform.position.y + mapObject.position.y, 
														  gameObject.transform.position.z - mapObject.position.z * Mathf.Cos(gameObject.transform.rotation.x));
		return mapObjectPrefab;
	}

	public GameObject instantiateMapObject(MapObject mapObject) {
		GameObject mapObjectPrefab = GameObject.Instantiate((GameObject)Resources.Load("MapPrefabs/" + mapObject.category + "/3D/" + mapObject.filename));
		return updateMapObject(mapObject, mapObjectPrefab);
	}

	public void instantiateMap() {
		foreach (MapObject mapObject in worldMap.objects) {
			GameObject instantiated = instantiateMapObject(mapObject);
			loadedObjects.Add(instantiated);
		}
	}

	public void updateMap() {
		for (int i = 0; i < worldMap.objects.Count; ++i) {
			if (worldMap.objects.Count == loadedObjects.Count)
				updateMapObject(worldMap.objects[i], loadedObjects[i]);
			else {
				if (i < loadedObjects.Count) {
					Destroy(loadedObjects[i]);
					loadedObjects[i] = instantiateMapObject(worldMap.objects[i]);
				}
				else {
					GameObject instantiated = instantiateMapObject(worldMap.objects[i]);
					loadedObjects.Add(instantiated);
				}
			}
		}
	}

	void Start () {
		loadedObjects = new List<GameObject>();
		loadMapFromJson();
		instantiateMap();
	}
	
	void Update () {
		loadMapFromJson();
		updateMap();
	}
}
