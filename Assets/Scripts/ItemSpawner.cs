using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemSpawner
{
    public  Dictionary<string, List<string>> LevelItems;
    private Dictionary<string, string> itemPaths;
    private System.Random random;

    public ItemSpawner()
    {
        random = new System.Random();

        // set which items each level can spawn
        LevelItems = new Dictionary<string, List<string>>();
        LevelItems["SampleScene"] = new List<string>() {
            "Banana", "Orange", "Pineapple", "Corn", "Tomato", "Watermelon"};
        LevelItems["Scene 1"] = new List<string>() {
            "Banana", "Orange", "Pineapple", "Corn", "Tomato", "Watermelon"};

        

        // set the name of each item to it's Resource location
        itemPaths = new Dictionary<string, string>();
        itemPaths["Banana"] = "Items/Food/Banana";
        itemPaths["Orange"] = "Items/Food/Orange";
        itemPaths["Pineapple"] = "Items/Food/Pineapple";
        itemPaths["Corn"] = "Items/Food/Corn";
        itemPaths["Tomato"] = "Items/Food/Tomato";
        itemPaths["Watermelon"] = "Items/Food/Watermelon";
    }

    // returns which items it chose should be spawned
    public List<string> ChooseSpawnItems(string levelName, int amountOfItems)
    {
        List<string> spawnItems;
        List<string> possibleItems = LevelItems[levelName];

        // take random items from list of possible items
        spawnItems = possibleItems.OrderBy(x => random.Next()).Take(amountOfItems).ToList();

        return spawnItems;
    }

    public List<Vector3> GetPossiblePositions()
    {
        ItemSpawnLocation[] itemSpawnLocations = Object.FindObjectsOfType<ItemSpawnLocation>();
        List<Vector3> positions = new List<Vector3>();
        
        foreach(ItemSpawnLocation isl in itemSpawnLocations)
        {
            positions.Add(isl.gameObject.transform.position);
            isl.gameObject.SetActive(false);
        }
        
        return positions;
    }

    public void RandomizeItemAndPositionOrder(List<string> items, List<Vector3> positions)
    {
        items = items.OrderBy(x => random.Next()).ToList();
        positions = positions.OrderBy(x => random.Next()).ToList();
    }

    // Receives list of item names and where to spawn them, then spawns them there
    public void SpawnItems(List<string> items, List<Vector3> positions)
    {
        int numOfItems = items.Count;

        if (numOfItems > positions.Count) {
            Debug.Log("Error: More items than possible positions");
            return;
        }
        
        for (int i = 0; i < numOfItems; i++) {
            GameObject item = Resources.Load(itemPaths[items[i]]) as GameObject;
            GameObject clone = Object.Instantiate(item, positions[i], Quaternion.identity);
            clone.name = item.name;
        }
    }
}
