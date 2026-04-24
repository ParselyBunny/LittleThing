using System.Collections.Generic;
using UnityEngine;

// TODO: assetmenu stuff here
public class Interaction : ScriptableObject
{
    string _label;
    string _description;
    Constants.TabCategories _tabCategory;
    bool _locked;
    Texture2D _icon;
    Dictionary<string, float> _statsChanges;
}