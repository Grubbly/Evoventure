using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGenerator : MonoBehaviour {

    private int fileSaveCount = 0;

    public Vector3Int tilemapSize = new Vector3Int(40, 40, 0);
    public Tilemap map;
    public Tile terrainTile;
    public int width = 0;
    public int height = 0;

    private void Awake()
    {
        map = GameObject.Find("Grid").GetComponentInChildren<Tilemap>();
        populateMap();
    }

    public void populateMap()
    {
        map.BoxFill(tilemapSize, terrainTile, 0, 0, width, height);
    }

}
