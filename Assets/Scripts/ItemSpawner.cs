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
        LevelItems["SampleScene"] = new List<string>() 
        {
            "Banana", "Orange", "Pineapple", "Corn", "Tomato", "Watermelon"
        };

        LevelItems["Scene 1"] = new List<string>() 
        {
            "Pineapple"
        };

        LevelItems["Scene 2"] = new List<string>() 
        {
            "Watermelon", "Corn", "Banana"
        };

        LevelItems["Scene 3"] = new List<string>() 
        {
            "Pineapple", "Watermelon", "Orange"
        };

        LevelItems["Scene 4"] = new List<string>() 
        {
            "Banana", "Orange", "Tomato"
        };

        LevelItems["Scene 5"] = new List<string>() 
        {
            "Watermelon", "Banana"
        };

        LevelItems["Random"] = new List<string>() 
        {
            "Watermelon", "Banana", "Corn", "Pineapple", "Tomato"
        };

        LevelItems["Market"] = new List<string>() 
        {
            "Chicken", "Cucumber", "Carrot", "Lemon", "Tomato", "Orange", "Bread",
            "Corn", "Watermelon", "Pumpkin", "Pizza", "Wine", "Hamburger",
            "Ice Cream", "French Fries", "Banana", "Apple"
        };

        LevelItems["Park"] = new List<string>() 
        {
            "Mushroom","Cow","Bird",
            "Squirrel","Spider", "Bee", "Sheep", "Turtle",
            "Pig","Hen", "Duck", "Butterfly"
        };

        LevelItems["Beach"] = new List<string>() 
        {
        // Chairs
        "Orange Chair", "Red Chair", "Green Chair", "Pink Chair", "White Blue Chair", "White Orange Chair",
        "White Green Chair",
        
        // Surfboard
        "Blue Yellow Surfboard", "Green Pink Surfboard", /*"Orange Pink Surfboard", "Red Surfboard", "Yellow White Surfboard",*/

        // Buckets
        "Yellow Bucket", "Red Bucket", "Blue Bucket",

        // Wheels
        "White Green Sea Wheel", "White Red Sea Wheel"
        };

        LevelItems["PlaygroundLowPoly"] = new List<string>() 
        {
            "Ant","Bucket", "Bus Toy", "Car Toy", "Ball", "Garbage Bin",  "Motorcycle Toy", "Rubber Duck", /*"Stones",*/
            "Tank Toy", "Truck Toy", "Trumpet", "Hat", "Police Car Toy", "Rubber Duck"
        };

        LevelItems["Forest"] = new List<string>() 
        {
            "Banana", "Orange", "Corn", "Pumpkin", "Cucumber", "Apple", "Tomato", "Carrot", "Lemon"
        };

        /*==================================================================*/

        // set the name of each item to it's Resource location
        itemPaths = new Dictionary<string, string>();

        // ----------------- Market Scene -------------- //
        itemPaths["Banana"] = "Items/Food/Banana";
        itemPaths["Orange"] = "Items/Food/Orange";
        itemPaths["Pineapple"] = "Items/Food/Pineapple";
        itemPaths["Corn"] = "Items/Food/Corn";
        itemPaths["Tomato"] = "Items/Food/Tomato";
        itemPaths["Watermelon"] = "Items/Food/Watermelon";
        itemPaths["French Fries"] = "Items/Food/French Fries";
        itemPaths["Hamburger"] = "Items/Food/Hamburger";
        itemPaths["Ice Cream"] = "Items/Food/Ice Cream";
        itemPaths["Wine"] = "Items/Food/Wine";
        itemPaths["Chicken"] = "Items/Food/Chicken";
        itemPaths["Cucumber"] = "Items/Food/Vegtables/Cucumber";
        itemPaths["Apple"] = "Items/Food/Vegtables/Apple";
        itemPaths["Lemon"] = "Items/Food/Vegtables/Lemon";
        itemPaths["Carrot"] = "Items/Food/Vegtables/Carrot";
        itemPaths["Pumpkin"] = "Items/Food/Vegtables/Pumpkin";
        itemPaths["Pizza"] = "Items/Food/Pizza";
        itemPaths["Bread"] = "Items/Food/Bread";

        // ------------------ Park Scene --------------- //
        itemPaths["Mushroom"] = "Items/Mushroom";
        itemPaths["Cow"] = "Items/Animals/Cow";
        itemPaths["Bird"] = "Items/Animals/Bird";
        itemPaths["Squirrel"] = "Items/Animals/Squirrel";
        itemPaths["Bee"] = "Items/Animal Groups/Bee Swarm";
        itemPaths["Sheep"] = "Items/Animal Groups/Sheep Herd";
        itemPaths["Turtle"] = "Items/Animals/Turtle";
        itemPaths["Pig"] = "Items/Animal Groups/Pig Drove";
        itemPaths["Hen"] = "Items/Animal Groups/Chicken Flock";
        itemPaths["Duck"] = "Items/Animal Groups/Duck Flock";
        itemPaths["Butterfly"] = "Items/Animal Groups/Butterflies";
        itemPaths["Spider"] = "Items/Animals/Spider";

        // ------------------ Beach Scene --------------- //

        // Chairs
        itemPaths["Orange Chair"] = "Items/Beach/Colored_chairs/Orange Chair";
        itemPaths["Red Chair"] = "Items/Beach/Colored_chairs/Red Chair";
        itemPaths["Green Chair"] = "Items/Beach/Colored_chairs/Green Chair";
        itemPaths["Pink Chair"] = "Items/Beach/Colored_chairs/Pink Chair";
        itemPaths["White Blue Chair"] = "Items/Beach/Colored_chairs/White Blue Chair";
        itemPaths["White Orange Chair"] = "Items/Beach/Colored_chairs/White Orange Chair";
        itemPaths["White Green Chair"] = "Items/Beach/Colored_chairs/White Green Chair";

        // Surfboards
        itemPaths["Blue Yellow Surfboard"] = "Items/Beach/Colored_surfboards/Blue Yellow Surfboard";
        // itemPaths["Red Surfboard"] = "Items/Beach/Colored_surfboards/Red Surfboard";
        itemPaths["Blue Yellow Surfboard"] = "Items/Beach/Colored_surfboards/Blue Yellow Surfboard";
        // itemPaths["Orange Pink Surfboard"] = "Items/Beach/Colored_surfboards/Orange Pink Surfboard";
        // itemPaths["Yellow White Surfboard"] = "Items/Beach/Colored_surfboards/Yellow White Surfboard";
        itemPaths["Green Pink Surfboard"] = "Items/Beach/Colored_surfboards/Green Pink Surfboard";

        // Buckets
        itemPaths["Yellow Bucket"] = "Items/Beach/Colored_buckets/Yellow Bucket";
        itemPaths["Red Bucket"] = "Items/Beach/Colored_buckets/Red Bucket";
        itemPaths["Blue Bucket"] = "Items/Beach/Colored_buckets/Blue Bucket";

        // Wheels
        itemPaths["White Green Sea Wheel"] = "Items/Beach/Colored_wheels/White Green Sea Wheel";
        itemPaths["White Red Sea Wheel"] = "Items/Beach/Colored_wheels/White Red Sea Wheel";

        // ----------------- Playground Scene -------------- //
        itemPaths["Ant"] = "Items/Playground/Ant";
		itemPaths["Bucket"] = "Items/Playground/Bucket";
		itemPaths["Bus Toy"] = "Items/Playground/Bus Toy";
		itemPaths["Car Toy"] = "Items/Playground/Car Toy";
		itemPaths["Ball"] = "Items/Playground/Ball";
		itemPaths["Garbage Bin"] = "Items/Playground/Garbage Bin";
		itemPaths["Motorcycle Toy"] = "Items/Playground/Motorcycle Toy";
		itemPaths["Rubber Duck"] = "Items/Playground/Rubber Duck";
		// itemPaths["Stones"] = "Items/Playground/Stones";
		itemPaths["Tank Toy"] = "Items/Playground/Tank Toy";
		itemPaths["Truck Toy"] = "Items/Playground/Truck Toy";
		itemPaths["Trumpet"] = "Items/Playground/Trumpet";
		itemPaths["Hat"] = "Items/Playground/Hat";
		itemPaths["Police Car Toy"] = "Items/Playground/Police Car Toy";
		itemPaths["Rubber Duck"] = "Items/Playground/Rubber Duck";
		
		
    }

    // returns which items it chose should be spawned
    public List<string> ChooseSpawnItems(string levelName, int amountOfItems)
    {
        // int          itemCount;
        List<string> spawnItems;
        List<string> possibleItems;

        if (!LevelItems.ContainsKey(levelName)) 
        {
            Debug.Log("Level name not in LevelItem dictionary.");
            return null;
        }

        possibleItems = LevelItems[levelName];

        // take random items from list of possible items
        spawnItems = possibleItems.OrderBy(x => random.Next()).Take(amountOfItems).ToList();
        
        // // if fewer items than in scene we requested, pull duplicates to reach the number requested
        // itemCount = spawnItems.Count;
        // while (itemCount < amountOfItems) {
        //     spawnItems.AddRange(possibleItems.OrderBy(x => random.Next()).Take(amountOfItems-itemCount).ToList());
        //     itemCount = spawnItems.Count;
        // }

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
        List<string>  tempItems     = items.OrderBy(x => random.Next()).ToList();
        List<Vector3> tempPositions = positions.OrderBy(x => random.Next()).ToList();

        items.Clear();
        items.AddRange(tempItems);

        positions.Clear();
        positions.AddRange(tempPositions);
    }

    // Receives list of item names and where to spawn them, then spawns them there
    public void SpawnItems(List<string> items, List<Vector3> positions)
    {
        int numOfItems = Mathf.Min(items.Count, positions.Count);

        for (int i = 0; i < numOfItems; i++) 
        {

            if (!itemPaths.ContainsKey(items[i]))
            {
                Debug.Log("No item path for: " + items[i]);
                continue;
            }

            GameObject item = Resources.Load(itemPaths[items[i]]) as GameObject;

            if (item == null) 
            {
                Debug.Log("Unable to load item \"" + itemPaths[items[i]] + 
                "\" from Resources. Check LevelItems disctionary.");
                continue;
            }

            GameObject clone = Object.Instantiate(item, positions[i],
                                                 Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));

            clone.name = item.name;
        }
    }
}
