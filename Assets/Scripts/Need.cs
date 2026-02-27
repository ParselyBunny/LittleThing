
public class Need
{
    private string label;
	private float value;
	private float maximum;

    public Need(string label, float value, float maximum)
    {
        this.label = label;
        this.value = value;
        this.maximum = maximum;
    }

    public void Update()
	{
        float decay = -1.0f;
		var new_val = value - decay;

		AddValue(new_val);
	}

	public float AddValue(float val)
	{
		float new_val = value + val;

		if (new_val < 0)
		{
            new_val = 0;
		}
		else if (new_val > maximum)
		{
            new_val = maximum;
		}

		return new_val;
    }
}
