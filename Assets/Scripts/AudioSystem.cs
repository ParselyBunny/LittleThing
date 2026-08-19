using UnityEngine;
using UnityEngine.Audio;
using static Constants;

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

    public void Play(AudioResource resource, 
        AudioChannel audioType = AudioChannel.Sfx, 
        bool loop = false)
    {
        switch (audioType)
        {
            case AudioChannel.Music:
                MusicSource.resource = resource;
                MusicSource.loop = true;
                MusicSource.Play();
                break;
            case AudioChannel.Sfx:
                SfxSource.resource = resource;
                SfxSource.loop = loop;
                SfxSource.Play();
                break;
        }
    }
}
