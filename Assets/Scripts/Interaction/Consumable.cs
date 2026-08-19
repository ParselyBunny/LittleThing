using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "Consumable", menuName = "Scriptable Objects/Consumable")]
public class Consumable : ScriptableObject
{
    public string Label;
    public string Description;
    public Constants.TabCategories TabCategory;
    public Texture2D Icon;
    public AudioResource Sound;
    public bool Locked;
    public float Hunger = 0f;
    public float Fun = 0f;
    public float Love = 0f;
}