using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Image))]
public class Pet : MonoBehaviour
{
    private Stats _stats;
    private float _velocity;

	// Enums
	public enum State { IDLE, WALK, EAT }

	// Core functions
	private void Start()
	{

	}

	public void Eat(Interaction interaction) { }
}