using UnityEngine;
using System;

public class Pet : MonoBehaviour
{
    public int MoveStrength = 100;

    private const float MAX_SCALE = 0.75f;

    private Stats _stats;
    private Vector3 _acceleration;
    private Vector3 _baseScale;
    private Rigidbody _rb;
    private Animator _anim;

    private void Start()
    {
        _stats = new Stats();
        _acceleration = new Vector3();
        _baseScale = transform.localScale;
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // Movement
        if (Time.frameCount % 60 == 0)
        {
            _acceleration.x = UnityEngine.Random.Range(-50f, 50f);
            _acceleration.y = 1f;
            _acceleration.z = UnityEngine.Random.Range(-50f, 50f);

            _rb.AddForce(MoveStrength * _acceleration);
        }

        // Animation
        transform.localScale = SquashAndStretch();

        // Handle falling out of world
        if (transform.position.y < Constants.WORLD_FLOOR)
        {
            _rb.position = Constants.START_POS;
            _rb.angularVelocity = Vector3.zero;
            _rb.linearVelocity = Vector3.zero;
        }
    }

    private Vector3 SquashAndStretch()
    {
        float velocityMagnitude = _rb.linearVelocity.magnitude;
        float modifiedScale = _baseScale.x * velocityMagnitude;

        if (modifiedScale <= _baseScale.x)
        {
            modifiedScale = _baseScale.x;
        }
        else if (modifiedScale >= MAX_SCALE)
        {
            modifiedScale = MAX_SCALE;
        }

        return new Vector3(
            modifiedScale,
            _baseScale.y,
            _baseScale.z);
    }

    private void Eat(Interaction interaction)
    {
        throw new NotImplementedException();
    }

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