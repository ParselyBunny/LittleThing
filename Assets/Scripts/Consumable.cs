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
    public float Health = 0f;
    public float Hunger = 0f;
    public float Hygiene = 0f;
    public float Happy = 0f;
}