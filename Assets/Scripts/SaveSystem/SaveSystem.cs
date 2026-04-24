using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Manage game save data.
/// </summary>
public class SaveDataSystem : MonoBehaviour
{
    [Header("File Storage Config")]
    [NonSerialized]
    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileUtility dataHandler;

    public static SaveDataSystem Main { get; private set; }

    private static string FileName => "save";

    public void NewGame()
    {
        gameData = new GameData();
    }

    /// <summary>
    /// Load data in autosave slot (slot 0).
    /// </summary>
    public void LoadGame()
    {
        LoadGame(0);
    }

    /// <summary>
    /// Load a default save state from Resources/DefaultSaves.
    /// </summary>
    public void LoadDefaultSave(string saveName)
    {
        // Load main save data
        TextAsset saveFile = Resources.Load<TextAsset>($"DefaultSaves/{saveName}");
        if (saveFile == null)
        {
            Debug.LogError($"Default save '{saveName}' not found in Resources/DefaultSaves/");
            return;
        }

        gameData = JsonUtility.FromJson<GameData>(saveFile.text);

        // Load dialogue data
        TextAsset dialogueFile = Resources.Load<TextAsset>($"DefaultSaves/{saveName}_dialogue");

        if (dialogueFile != null)
        {
            // Save dialogue file to persistent storage so Yarn can load it
            string dialoguePath = System.IO.Path.Combine(
                UnityEngine.Application.persistentDataPath,
                $"save_{gameData.SaveId}_dialogue");
            System.IO.File.WriteAllText(dialoguePath, dialogueFile.text);
        }

        // Push loaded data to all persistence objects
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void LoadGame(int saveSlotIndex)
    {
        // load any saved data from a file using the data handler
        gameData = dataHandler.LoadFile("_" + saveSlotIndex.ToString());

        // if no data can be loaded, initialize new game
        if (gameData == null)
        {
            Debug.Log($"No save file found for slot {saveSlotIndex}. Starting new game.");
            NewGame();
            return;
        }

        // push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }

        Debug.Log($"Game loaded from slot {saveSlotIndex}.");
    }

    /// <summary>
    /// Save to autosave slot (slot 0).
    /// </summary>
    public void SaveGame()
    {
        SaveGame(0);
    }

    public void SaveGame(int saveSlotIndex)
    {
        gameData = new GameData(saveSlotIndex);

        // pass the data to other scripts so they can update it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        // save that data to a file using the data handler
        dataHandler.SaveFile(gameData, "_" + saveSlotIndex.ToString());

        Debug.Log($"Game saved to slot {saveSlotIndex}.");
    }

    private void Awake()
    {
        if (Main != null)
        {
            Debug.LogError("Found more than one SaveDataSystem in the scene.");
        }

        Main = this;
    }

    private void Start()
    {
        dataHandler = new FileUtility(
            UnityEngine.Application.persistentDataPath, FileName);
        dataPersistenceObjects = FindAllDataPersistenceObjects();

        // Start with a fresh game - press F9 to load autosave
        NewGame();
    }

    /// <summary>
    /// Return a list of all objects that inherit from
    /// MonoBehaviour and implement IDataPersistence.
    /// </summary>
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects =
            FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<IDataPersistence>();

        var list = new List<IDataPersistence>(dataPersistenceObjects);

        if (list.Count == 0)
        {
            Debug.LogWarning("No data persistence objects found in scene.");
        }

        return list;
    }

    private void OnApplicationQuit()
    {
        // Autosave to slot 0
        SaveGame();
    }
}
