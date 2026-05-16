using UnityEngine;
using UnityEngine.Audio;

public class AudioSystem : MonoBehaviour, IDataPersistence
{
    public AudioSource MusicSource;
    public AudioSource SfxSource;

    public void LoadData(GameData data)
    {
        MusicSource.volume = data.MusicVolume;
        SfxSource.volume = data.SfxVolume;
    }

    public void SaveData(ref GameData data)
    {
        data.MusicVolume = MusicSource.volume;
        data.SfxVolume = SfxSource.volume;
    }

    public void Play(AudioType audioType, AudioResource resource)
    {
        switch (audioType)
        {
            case AudioType.Music:
                MusicSource.resource = resource;
                MusicSource.loop = true;
                MusicSource.Play();
                break;
            case AudioType.Sfx:
                SfxSource.resource = resource;
                SfxSource.Play();
                break;
        }
    }

    public enum AudioType
    {
        Music,
        Sfx,
    }
}
