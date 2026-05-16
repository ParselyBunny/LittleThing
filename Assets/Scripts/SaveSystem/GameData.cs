using System;

[Serializable]
public class GameData
{
    public static int CurrentVersion => 1;
    
    public int Version = CurrentVersion;
    public int SaveId;  // Save slot ID
    public float MusicVolume;
    public float SfxVolume;

    public GameData()
    {
        // Default constructor for deserialization
    }

    public GameData(int saveSlot)
    {
        SaveId = saveSlot;
    }
}
