using UnityEngine;
using System;

public class Pet : MonoBehaviour
{
    public int MoveStrength = 100;

    private const float MAX_SCALE = 0.75f;

    private Stats _stats;
    private float _t;  // Scale interpolator
    private Vector3 _acceleration;
    private Vector3 _targetScale;
    private Vector3 _baseScale;
    private Rigidbody _rb;
    private Animator _anim;

    private void Start()
    {
        _t = 0.0f;
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
        _targetScale = SquashAndStretch();  // Change scale based on velocity
        _t = UpdateScaleInterpolator();  // Handle update of interpolator
        transform.localScale = InterpolateScale();  // Interpolate scale based on new interpolator and scale

        // Handle falling out of world
        if (transform.position.y < Constants.WORLD_FLOOR)
        {
            _rb.position = Constants.START_POS;
            _rb.angularVelocity = Vector3.zero;
            _rb.linearVelocity = Vector3.zero;
        }
    }

    private float UpdateScaleInterpolator()
    {
        float t = _t;

        if (transform.localScale.x <= _targetScale.x)
        {
            t += 0.1f * Time.fixedDeltaTime;
        }
        else
        {
            t -= 0.1f * Time.fixedDeltaTime;
        }

        if (t < 0)
        {
            t = 0;
        }
        else if (t > 1)
        {
            t = 1;
        }

        return t;
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

    private Vector3 InterpolateScale()
    {
        Vector3 baseScale = transform.localScale;
        return new Vector3(
            Mathf.Lerp(baseScale.x, _targetScale.x, _t),
            baseScale.y, 
            baseScale.z
        );
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