using UnityEngine;
using UnityEngine.UI;

public class NeedsWindow : MonoBehaviour
{
    public Slider Health;
    public Slider Hunger;
    public Slider Hygiene;
    public Slider Happy;

    private Pet _pet;

    private void Start()
    {
        _pet = FindAnyObjectByType<Pet>();

        if (_pet == null)
        {
            Debug.LogError("Can't find Pet object.");
        }

        SetStats();
    }

    private void Update()
    {
        SetStats();
    }

    private void SetStats()
    {
        Health.value = _pet.GetStat(Constants.StatNames.HEALTH);
        Hunger.value = _pet.GetStat(Constants.StatNames.HUNGER);
        Hygiene.value = _pet.GetStat(Constants.StatNames.HYGIENE);
        Happy.value = _pet.GetStat(Constants.StatNames.HAPPY);
    }
}
