using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// using UnityEditor;

public class ItemSpawner : MonoBehaviour
{
    private Dictionary<string, string> levelItems;
    private Dictionary<string, string> itemPaths;

    public ItemSpawner()
    {
        // set which items each level can spawn
        levelItems = new Dictionary<string, string>();

        // set the name of each item to it's Resource location
        itemPaths = new Dictionary<string, string>();
    }

    public List<string> ChooseSpawnItems(string levelName, int amountOfItems)
    {
        //will return which items it spawned
        List<string> spawnedItems = new List<string>();
        


        return spawnedItems;
    }

    public List<Vector3> GetPossiblePositions()
    {
        List<Vector3> positions = new List<Vector3>();


        return positions;
    }

    public void SpawnItems(List<string> items, List<Vector3> positions, Scene scene)
    {
        // GameObject item = Instantiate(Resources.Load("EnemyPrefab1") as GameObject, positions[0], Quaternion.identity);
        GameObject item = Resources.Load("EnemyPrefab1") as GameObject;
        item.transform.position = positions[0];
        // PrefabUtility.InstantiatePrefab(item, scene);
    }
}
