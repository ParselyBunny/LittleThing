
public class Need
{
    private string _label;
	private float _value;
	private float _maximum;
	private float _decay = 0.01f;

    public Need(string label, float value, float maximum)
    {
        _label = label;
        _value = value;
        _maximum = maximum;
    }

    public void Update(float deltaTime)
	{
		float val = deltaTime * (_value - _decay);

		Add(val);
	}

	public float GetValue() => _value;

	private float Add(float change)
	{
		float val = _value + change;

		if (val < 0)
		{
            val = 0;
		}
		else if (val > _maximum)
		{
            val = _maximum;
		}

		return val;
    }
}
