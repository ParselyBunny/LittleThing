// Handle the Pet's vital statistics

public class Stats
{
	Need[] needs =
	[
		// label, value, maximum, decay
		MyNeed.new ("Hunger", 100.0, 100.0),
		MyNeed.new ("Fun", 100.0, 100.0),
		MyNeed.new ("Health", 100.0, 100.0)
	];

	public void _update_needs()
	{
		// for each Need in Needs, update that needs
		foreach (Need need in needs)
		{
			need.update_value();
		}
	}

	public void _update_needs_change(changes: Dictionary)
	{
		needs[0].add_value(changes["hunger"]);
		needs[1].add_value(changes["fun"]);
		needs[2].add_value(changes["health"]);
	}
}