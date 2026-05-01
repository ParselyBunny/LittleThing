using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CapsuleCollider))]
public class Pet : MonoBehaviour
{
    private Stats _stats;
    private Vector3 _acceleration;
    private Rigidbody _rb;

    private void Reset()
    {
        _stats = new Stats();
        _acceleration = new Vector3();
    }

    private void FixedUpdate()
    {
        if (Time.fixedDeltaTime % 1f == 0)
        {
            _acceleration = new Vector3(
                Random.Range(-5f, 5f), 
                Random.Range(-5f, 5f), 
                Random.Range(-5f, 5f)
            );

            _rb.AddForce(_acceleration);
        }
    }

    // Enums
    public enum State { IDLE, WALK, EAT }

	public void Eat(Interaction interaction) { }
}