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
        "Squirrel","Spider", "Bee", "Sheep", "Turtle",
        "Pig","Chicken", "Duck"};

        LevelItems["Beach"] = new List<string>() {
        // Chairs
        "Orange Chair", "Red Chair", "Green Chair", "Pink Chair", "White Blue Chair", "White Orange Chair",
        "White Green Chair",
        
        // Surfboard
        "Blue Yellow Surfboard", "Green Pink Surfboard", "Orange Pink Surfboard", "Red Surfboard", "Yellow White Surfboard",

        // Buckets
        "Yellow Bucket", "Red Bucket", "Blue Bucket",

        // Wheels
        "White Green Sea Wheel", "White Red Sea Wheel"
        };




        /*==================================================================*/

        // set the name of each item to it's Resource location
        itemPaths = new Dictionary<string, string>();
        itemPaths["Banana"] = "Items/Food/Banana";
        itemPaths["Orange"] = "Items/Food/Orange";
        itemPaths["Pineapple"] = "Items/Food/Pineapple";
        itemPaths["Corn"] = "Items/Food/Corn";
        itemPaths["Tomato"] = "Items/Food/Tomato";
        itemPaths["Watermelon"] = "Items/Food/Watermelon";

        // ------------------ Park Scene ---------------//
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
        itemPaths["Spider"] = "Items/Animals/Spider";

        // ------------------ Beach Scene ---------------//

        // Chairs
        itemPaths["Orange Chair"] = "Items/Beach/Colored_chairs/Orange Chair";
        itemPaths["Red Chair"] = "Items/Beach/Colored_chairs/Red Chair";
        itemPaths["Green Chair"] = "Items/Beach/Colored_chairs/Green Chair";
        itemPaths["Pink Chair"] = "Items/Beach/Colored_chairs/Pink Chair";
        itemPaths["White Blue Chair"] = "Items/Beach/Colored_chairs/White Blue Chair";
        itemPaths["White Orange Chair"] = "Items/Beach/Colored_chairs/White Orange Chair";
        itemPaths["White Green Chair"] = "Items/Beach/Colored_chairs/White Green Chair";

        // Surfboard
        itemPaths["Blue Yellow Surfboard"] = "Items/Beach/Colored_surfboards/Blue Yellow Surfboard";
        itemPaths["Red Surfboard"] = "Items/Beach/Colored_surfboards/Red Surfboard";
        itemPaths["Blue Yellow Surfboard"] = "Items/Beach/Colored_surfboards/Blue Yellow Surfboard";
        itemPaths["Orange Pink Surfboard"] = "Items/Beach/Colored_surfboards/Orange Pink Surfboard";
        itemPaths["Yellow White Surfboard"] = "Items/Beach/Colored_surfboards/Yellow White Surfboard";

        // Buckets
        itemPaths["Yellow Bucket"] = "Items/Beach/Colored_buckets/Yellow Bucket";
        itemPaths["Red Bucket"] = "Items/Beach/Colored_buckets/Red Bucket";
        itemPaths["Blue Bucket"] = "Items/Beach/Colored_buckets/Blue Bucket";

        // Wheels
        itemPaths["White Green Sea Wheel"] = "Items/Beach/Colored_wheels/White Green Sea Wheel";
        itemPaths["White Red Sea Wheel"] = "Items/Beach/Colored_wheels/White Red Sea Wheel";
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
            if (isl.isActiveAndEnabled)
            {
                positions.Add(isl.gameObject.transform.position);
                isl.gameObject.SetActive(false);
            }
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
