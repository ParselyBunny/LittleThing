using UnityEditor;
using UnityEngine;

public class Pet : MonoBehaviour
{
    public int MoveStrength = 1000;

    private Stats _stats;
    private Vector3 _acceleration;
    private Rigidbody _rb;

    private void Start()
    {
        _stats = new Stats();
        _acceleration = new Vector3();
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Time.frameCount % 5 == 0)
        {
            _acceleration = new Vector3(
                Random.Range(-50f, 50f), 
                0f, 
                Random.Range(-50f, 50f)
            );

            _rb.AddForce(MoveStrength * Time.deltaTime * _acceleration);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(0f, 1000f * Time.deltaTime, 0f);
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ResetVelocity();
        }

        HandleFallOutOfWorld();
    }

    public void HandleFallOutOfWorld()
    {
        if (transform.position.y < Constants.WORLD_FLOOR)
        {
            transform.position = Constants.START_POS;
            ResetVelocity();
        }
    }

    public void ResetVelocity()
    {
        _rb.angularVelocity = Vector3.zero;
        _rb.linearVelocity = Vector3.zero;
    }

    public void Eat(Interaction interaction) { }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying && Application.isEditor)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, _acceleration);
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, _rb.angularVelocity);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, _rb.linearVelocity);
        }
    }
}