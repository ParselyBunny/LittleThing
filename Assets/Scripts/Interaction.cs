using System.Collections.Generic;
using UnityEngine;

// TODO: assetmenu stuff here
public class Interaction : ScriptableObject
{
    string label;
    string description;
    Constants.TabCategories tab_category;
    bool locked;
    Texture2D icon;
    Dictionary<string, float> stats_changes;
}