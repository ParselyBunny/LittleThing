using System;

public class Need
{
	string label;
	float value;
	float maximum;

    public Update()
	{
		float decay = -1.0; // TODO: calculate decay based on diff between val and max
		var new_val = value - decay
		
		add_value(new_val)
	}
	# _val can be negative or positive
	func add_value(_val):
		var new_val = value + _val
		
		if (new_val < 0):
			value = 0

        elif(new_val < maximum):
			value = new_val

        else:
			value = maximum
}
