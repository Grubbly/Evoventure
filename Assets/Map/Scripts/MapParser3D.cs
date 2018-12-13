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
	public string mapPrefabsLocation = "Assets/resources/MapPrefabs";
	public bool setMapMode = true;
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

	public string findFolderFor(string fileName) {
		string [] categories = Directory.GetDirectories(mapPrefabsLocation);

		foreach (string category in categories) {
			string [] categoryPath = category.Split('/');
			string categoryName = categoryPath[categoryPath.Length - 1];
			string category3DPath = mapPrefabsLocation + "/" + categoryName  + "/3D";
			if (Directory.GetFiles(category3DPath, fileName + ".*").Length > 0)
				return categoryName;
		}

		return "";
	}

	public MapObject formMapObject(GameObject child) {
		MapObject instantiated = new MapObject();

		string fName = child.name.Split(' ')[0];
		xyz position = new xyz();
		position.x = (int)((child.transform.position.x - gameObject.transform.position.x)/Mathf.Sin(gameObject.transform.rotation.y));
		position.z = (int)((child.transform.position.z - gameObject.transform.position.z)/Mathf.Cos(gameObject.transform.rotation.x));
		position.y = (int)(child.transform.position.y  - gameObject.transform.position.y);
		int rotation = (int)(child.transform.rotation.eulerAngles.y - gameObject.transform.rotation.eulerAngles.y) - 180;
		string category = findFolderFor(fName);

		instantiated.filename = fName;
		instantiated.category = category;
		instantiated.position = position;
		instantiated.rotation = rotation;

		return instantiated;
	}

	public void captureMap() {
		worldMap = new MapHolder();
		worldMap.objects = new List<MapObject>();
         foreach (Transform child in transform)
         {
			loadedObjects.Add(child.gameObject);
			MapObject childMapObject = formMapObject(child.gameObject);
			worldMap.objects.Add(childMapObject);
         }
	}

	public string stringifyMapObject(MapObject mapObject) {
		string mapObjectJSON = "{\n";

		mapObjectJSON += @"""filename"": """ + mapObject.filename + "\",\n";
		mapObjectJSON += @"""category"": """ + mapObject.category + "\",\n";
		mapObjectJSON += @"""position"": {" + "\n\"x\": " + mapObject.position.x + ",\n\"y\": " + mapObject.position.y + ",\n\"z\": " + -mapObject.position.z + "\n" + "},\n";
		mapObjectJSON += @"""rotation"": """ + mapObject.rotation + "\"\n";

		mapObjectJSON += "}";

		return mapObjectJSON;
	}

	public string stringifyMap() {
		string mapJSON = "{\n\"objects\": [\n";

		bool firstMapObject = true;
		mapJSON += stringifyMapObject(worldMap.objects[0]);

		foreach(MapObject mapObject in worldMap.objects) {
			if (firstMapObject)
				firstMapObject = false;
			else
				mapJSON +=  ",\n" + stringifyMapObject(mapObject);
		}

		mapJSON += "\n]\n}";

		return mapJSON;
	}

	public void writeMapToJSON() {
		string worldMapJSON = stringifyMap();
		System.IO.File.WriteAllText(mapFile, worldMapJSON);
	}

	void Start () {
		loadedObjects = new List<GameObject>();
		if (setMapMode) {
			captureMap();
			writeMapToJSON();
		}
		else {
			loadMapFromJson();
			instantiateMap();
		}
		
	}
	
	void Update () {
		if (!setMapMode) {
			loadMapFromJson();
			updateMap();
		}
	}
}
