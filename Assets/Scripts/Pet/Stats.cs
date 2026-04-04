using UnityEngine;

// Handle the Pet's vital statistics
public class Stats
{
	Need Hunger = new("Hunger", 100f, 100f);
	Need Fun = new("Fun", 100f, 100f);
	Need Love = new("Love", 100f, 100f);

	public void Update()
	{
		Hunger.Update(Time.deltaTime);
		Fun.Update(Time.deltaTime);
		Love.Update(Time.deltaTime);
	}

	public float GetHunger() => Hunger.GetValue();
	public float GetFun() => Fun.GetValue();
	public float GetLove() => Love.GetValue();
}