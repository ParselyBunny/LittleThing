using UnityEngine;

// Handle the Pet's vital statistics
public class Stats
{
	Need Health = new("Health", 100f, 100f);
	Need Hunger = new("Hunger", 100f, 100f);
	Need Hygiene = new("Hygiene", 100f, 100f);
	Need Happy = new("Happy", 100f, 100f);

	public void Update()
	{
		Health.Update(Time.deltaTime);
		Hunger.Update(Time.deltaTime);
		Hygiene.Update(Time.deltaTime);
		Happy.Update(Time.deltaTime);
	}

	public void Resolve(Consumable consumable)
	{
		if (consumable != null)
		{
			Health.Add(consumable.Health);
			Hunger.Add(consumable.Hunger);
			Hygiene.Add(consumable.Hygiene);
			Happy.Add(consumable.Happy);
        }
	}

	public float GetHealth => Health.GetValue();
	public float GetHunger => Hunger.GetValue();
	public float GetHygiene => Hygiene.GetValue();
    public float GetHappy => Happy.GetValue();
}