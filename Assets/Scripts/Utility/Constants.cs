using UnityEngine;

public static class Constants
{
    public enum TabCategories
    {
        FOOD,
        ACTIVITIES,
    }

    public enum State
    { 
        IDLE, 
        WALK, 
        EAT,
    }

    public enum StatNames
    {
        HUNGER,
        LOVE,
        FUN,
    }

    public static int WORLD_FLOOR = -50;
    public static Vector3 START_POS = new(1, 3, 2);
}
