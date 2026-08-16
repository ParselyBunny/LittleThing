using UnityEngine;

public abstract class Interaction : ScriptableObject
{
    public string Label;
    public string Description;
    public Constants.TabCategories TabCategory;
    public bool Locked;
    public Texture2D Icon;
}