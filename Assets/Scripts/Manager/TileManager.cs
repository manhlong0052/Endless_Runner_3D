using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefab;
    public float zSpawn = 0f;
    public float tileLength = 30f;
    public int numberOfTile = 5;
    private List<GameObject> activeTiles = new List<GameObject>();
    public Transform playerTransform;

    private void Start()
    {
        for (int i = 0; i < numberOfTile; i++)
        {
            if (i==0) SpawnTile(0);
            else SpawnTile(Random.Range(0, tilePrefab.Length));
        }
    }

    private void Update()
    {
        if (playerTransform.position.z - 35 > zSpawn - (numberOfTile * tileLength)) {
            SpawnTile(Random.Range(0, tilePrefab.Length));
            deleteTile();
        }
    }

    private void deleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    public void SpawnTile(int tileIndex)
    {
        GameObject go = Instantiate(tilePrefab[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
    }
}
