using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemSpawner
{
    public  Dictionary<string, List<string>> LevelItems;
    private Dictionary<string, string>       itemPaths;
    private System.Random                    random;

    public ItemSpawner()
    {
        random = new System.Random();

        LevelItems = new Dictionary<string, List<string>>();
                    /* Set which items each level can spawn */
    /* If the level doesn't spawn items, write the items you'd want matchobject to take*/
        /*==================================================================*/
        LevelItems["SampleScene"] = new List<string>() {
            "Banana", "Orange", "Pineapple", "Corn", "Tomato", "Watermelon"};

        LevelItems["Scene 1"] = new List<string>() {
            "Corn", "Orange", "Tomato"};

        LevelItems["Scene 2"] = new List<string>() {
            "Watermelon", "Corn", "Banana"};

        LevelItems["Scene 3"] = new List<string>() {
            "Pineapple", "Watermelon", "Orange"};

        LevelItems["Scene 4"] = new List<string>() {
            "Banana", "Orange", "Tomato"};

        LevelItems["Scene 5"] = new List<string>() {
            "Watermelon", "Banana"};

        LevelItems["Random"] = new List<string>() {
            "Watermelon", "Banana", "Corn", "Pineapple", "Tomato"};

        LevelItems["Market"] = new List<string>() {
            "Duck","Chicken", "Cucumber", "Carrot", "Lemon", "Tomato", "Orange", "Bread",
            "Corn", "Watermelon","Pumpkin", "Broccoli", "Weight","Pizza","Wine","Junk food",
            "Banana"};

        LevelItems["Market-Ariel"] = new List<string>() {
            "Banana", "Orange", "Pineapple", "Corn", "Tomato", "Watermelon"};

        LevelItems["Park"] = new List<string>() {
        "Mushroom","Cow","Bird",
        "Squirrel","Spiders", "Bee", "Sheep", "Turtle",
        "Pig","Chicken", "Duck"};
        


        /*==================================================================*/

        // set the name of each item to it's Resource location
        itemPaths = new Dictionary<string, string>();
        itemPaths["Banana"] = "Items/Food/Banana";
        itemPaths["Orange"] = "Items/Food/Orange";
        itemPaths["Pineapple"] = "Items/Food/Pineapple";
        itemPaths["Corn"] = "Items/Food/Corn";
        itemPaths["Tomato"] = "Items/Food/Tomato";
        itemPaths["Watermelon"] = "Items/Food/Watermelon";
        itemPaths["Mushroom"] = "Items/Animals/Mushroom";
        itemPaths["Cow"] = "Items/Animals/Cow";
        itemPaths["Bird"] = "Items/Animals/Bird";
        itemPaths["Squirrel"] = "Items/Animals/Squirrel";
        itemPaths["Bee"] = "Items/Animals/Bee";
        itemPaths["Sheep"] = "Items/Animals/Sheep";
        itemPaths["Turtle"] = "Items/Animals/Turtle";
        itemPaths["Pig"] = "Items/Animals/Pig";
        itemPaths["Chicken"] = "Items/Animals/Chicken";
        itemPaths["Duck"] = "Items/Animals/Duck";

    }

    // returns which items it chose should be spawned
    public List<string> ChooseSpawnItems(string levelName, int amountOfItems)
    {
        int          itemCount;
        List<string> spawnItems;
        List<string> possibleItems;

        if (!LevelItems.ContainsKey(levelName)) {
            Debug.Log("Level name not in LevelItem dictionary.");
            return null;
        }

        possibleItems = LevelItems[levelName];

        // take random items from list of possible items
        spawnItems = possibleItems.OrderBy(x => random.Next()).Take(amountOfItems).ToList();
        
        // if fewer items than in scene we requested, pull duplicates to reach the number requested
        itemCount = spawnItems.Count;
        while (itemCount < amountOfItems) {
            spawnItems.AddRange(possibleItems.OrderBy(x => random.Next()).Take(amountOfItems-itemCount).ToList());
            itemCount = spawnItems.Count;
        }

        return spawnItems;
    }

    public List<Vector3> GetPossiblePositions()
    {
        ItemSpawnLocation[] itemSpawnLocations = Object.FindObjectsOfType<ItemSpawnLocation>();
        List<Vector3>       positions          = new List<Vector3>();
        
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
        // int numOfItems = items.Count;
        int numOfItems = Mathf.Min(items.Count, positions.Count);
        
        for (int i = 0; i < numOfItems; i++) {
            GameObject item = Resources.Load(itemPaths[items[i]]) as GameObject;

            if (item == null) {
                Debug.Log("Unable to load item \"" + itemPaths[items[i]] + 
                "\" from Resources. Check LevelItems disctionary.");
                continue;
            }

            GameObject clone = Object.Instantiate(item, positions[i], Quaternion.identity);
            clone.name = item.name;
        }
    }
}
