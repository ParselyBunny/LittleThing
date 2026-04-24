using System;
using System.IO;
using UnityEngine;

public class FileUtility
{
    private string saveDirectoryPath;
    private string saveFileName;

    public FileUtility(string directoryPath, string fileName)
    {
        saveDirectoryPath = directoryPath;
        saveFileName = fileName;
    }

    public GameData LoadFile(string suffix)
    {
        // use Path.Combine to account for different OSs having different path separators
        string path = Path.Combine(saveDirectoryPath, saveFileName + suffix);
        GameData gameData = null;

        if (File.Exists(path))
        {
            try
            {
                // load the serialized data from the file
                string jsonData = string.Empty;

                using (FileStream stream = new(path, FileMode.Open))
                {
                    using StreamReader reader = new(stream);
                    jsonData = reader.ReadToEnd();
                }

                // deserialize the data from Json back into the C# object
                gameData = JsonUtility.FromJson<GameData>(jsonData);

                // Validate version
                if (gameData != null && gameData.Version != GameData.CurrentVersion)
                {
                    Debug.LogWarning($"Save file version mismatch. File version: {gameData.Version}, Current version: {GameData.CurrentVersion}");

                    // Future: implement migration logic here
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error occurred when trying to load data from file: "
                    + path + "\n" + e);
            }
        }

        return gameData;
    }

    public void SaveFile(GameData gameData, string suffix)
    {
        // use Path.Combine to account for different OSs having different path separators
        string path = Path.Combine(saveDirectoryPath, saveFileName + suffix);

        try
        {
            // create directory if it doesn't already exist
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            // serialize the C# game data object into Json
            string json = JsonUtility.ToJson(gameData, true);

            // write the serialized data to the file
            using FileStream stream = new(path, FileMode.Create);
            using StreamWriter writer = new(stream);
            writer.Write(json);
        }
        catch (Exception e)
        {
            Debug.LogError("Error occurred when trying to save data to file: "
                + path + "\n" + e);
        }
    }
}
