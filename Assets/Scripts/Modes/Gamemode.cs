using UnityEngine;
using System.Collections.Generic;
//using Mirror;

// TODO:
// - Fix building creation in MenuButton and Hotbar (commented out)
// - Get resources properly connected to survival

public class Gamemode : MonoBehaviour
{
    // Active instance
    public static Gamemode active;
    public static Stage stage;

    // Static save variables
    public static DifficultyData difficulty;
    public static string seed = "Vectorio";
    public static float time = 0;

    // Create instantion handler
    public GameObject instantiationHandler;
    public GameObject droneHandler;
    public GameObject enemyHandler;

    // Gamemode information
    [Header("Gamemode Info")]
    public new string name;
    public string version;
    public Stage _stage;
    public Difficulty _difficulty;
    public float naturalHeatTimer = 5f;

    [Header("Gamemode Settings")]
    public bool naturalHeatGrowth;
    public bool useGroupSpawning;
    public bool useEnergizers;
    public bool useResources;
    public bool useDroneConstruction;
    public bool useEngineering;
    public bool spawnBlueprints;
    public bool generateWorld;
    public bool unlockEverything;
    public bool initBuildings;
    public bool initEnemies;
    public bool initGuardians;

    // Set active instance
    public void Awake()
    {
        // Get active instance
        active = this;

        // Create handlers
        //Instantiate(instantiationHandler, Vector3.zero, Quaternion.identity);
    }

    // Start method
    public void Start()
    {
        instantiationHandler.SetActive(true);
        droneHandler.SetActive(true);
        enemyHandler.SetActive(true);

        Setup();
    }

    // Setup game
    public virtual void Setup()
    {
        Debug.Log("Override to setup game functionality!");

        // Generate all scriptables
        ScriptableLoader.GenerateAllScriptables();
        LowresMap.active.Setup();

        // Check difficulty variable
        if (difficulty == null)
        {
            Debug.Log("Difficulty data missing. Creating new one");
            difficulty = _difficulty.SetData(new DifficultyData());
        }

        // Check stage variable
        if (stage == null)
        {
            Debug.Log("Stage data missing. Setting to default");
            stage = _stage;
        }

        NewSaveSystem.loadGame = false;
    }
    
    // Update playtime
    public virtual void Update()
    {
        // Increment time
        time += Time.deltaTime;

        // Check heat growth
        if (naturalHeatGrowth)
        {
            naturalHeatTimer -= Time.deltaTime;
            if (naturalHeatTimer <= 0)
            {
                Resource.active.Apply(Resource.CurrencyType.Heat, 1, false);
                difficulty.startingHeat += 1;
                naturalHeatTimer = 5f;
            }
        }
    }

    // Save game
    public virtual void SaveGame()
    {
        NewSaveSystem.saveData = NewSaveSystem.SaveGame();
    }

    // Auto save
    public virtual void AutoSave()
    {
        if (Settings.autoSave && GuardianHandler.active.guardians.Count == 0)
        {
            SaveGame();
            Events.active.AutoSave();
        }
    }

    // Tells the gamemode how to generate inventory
    public virtual void InitGamemode()
    {
        useDroneConstruction = !difficulty.enableInstaPlace;
        naturalHeatGrowth = difficulty.naturalHeatGrowth;

        #pragma warning disable CS0612
        if (!NewSaveSystem.loadGame && generateWorld) WorldGenerator.active.GenerateWorldData(seed);
        #pragma warning restore CS0612
    }

    // Setup starting resources
    public virtual void SetupStartingResources()
    {
        // Adjust power storage
        Resource.active.ApplyStorage(Resource.CurrencyType.Power, 5000);

        // Setup heat storage
        Resource.active.SetStorage(Resource.CurrencyType.Heat, stage.heat);
        Resource.active.Apply(Resource.CurrencyType.Heat, difficulty.startingHeat, false);
    }
}
