using UnityEngine;
using UnityEngine.UI;

public class NeedsWindow : MonoBehaviour
{
    public Slider Hunger;
    public Slider Love;
    public Slider Fun;

    private Pet _pet;

    void Start()
    {
        _pet = FindAnyObjectByType<Pet>();

        if (_pet == null)
        {
            Debug.LogError("Can't find Pet object.");
        }
    }

    void Update()
    {
        if (_pet != null)
        {
            Hunger.value = _pet.GetStat(Constants.StatNames.HUNGER);
            Love.value = _pet.GetStat(Constants.StatNames.LOVE);
            Fun.value = _pet.GetStat(Constants.StatNames.FUN);
        }
    }
}
