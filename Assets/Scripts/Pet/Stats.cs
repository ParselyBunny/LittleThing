using UnityEngine;
using System;

// Handle the Pet's vital statistics
public class Stats
{
	Need _hunger = new("Hunger", 100f, 100f);
	Need _fun = new("Fun", 100f, 100f);
	Need _love = new("Love", 100f, 100f);

	public void Update()
	{
		_hunger.Update(Time.deltaTime);
		_fun.Update(Time.deltaTime);
		_love.Update(Time.deltaTime);
	}

	public float GetHunger() => _hunger.GetValue();
	public float GetFun() => _fun.GetValue();
	public float GetLove() => _love.GetValue();
	public void AddHunger(float val) => _hunger.Add(val);
	public void AddHunger(Food food) => AddHunger(food.Hunger);
	public void AddFun(float val) => _fun.Add(val);
	public void AddFun(Food food) => AddFun(food.Fun);
	public void AddLove(float val) => _love.Add(val);
	public void AddLove(Food food) => AddLove(food.Love);
	public void Resolve(Consumable interact)
	{
		if (interact is Food)
		{
			Food f = interact as Food;
			AddHunger(f);
		}
		else
		{
			throw new NotImplementedException();
		}
	}
}