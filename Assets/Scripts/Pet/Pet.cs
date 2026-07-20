using UnityEngine;
using System;
using UnityEditor;

public class Pet : MonoBehaviour
{
    public int MoveStrength = 100;

    private const float MAX_SCALE = 0.6f;

    private Rigidbody _rb;
    private Animator _anim;
    private Stats _stats;
    private Vector3 _acceleration;
    private Vector3 _targetScale;
    private Vector3 _baseScale;
    private float _t;  // Scale interpolator
    private float _tRate;
    private bool _showDebug = true;

    private void Start()
    {
        _t = 0.0f;
        _tRate = 0.5f;
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
        _targetScale = SquashAndStretch();  // Change target scale based on velocity
        _t = UpdateScaleInterpolator(_t);  // Handle update of interpolator
        transform.localScale = InterpolateScale(_t);  // Interpolate scale based on new interpolator and scale

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
            modifiedScale,
            _baseScale.z);
    }

    private float UpdateScaleInterpolator(float t)
    {
        if (transform.localScale.x < _targetScale.x)
        {
            t += _tRate * Time.fixedDeltaTime;
        }
        else if (transform.localScale.x >= _targetScale.x)
        {
            t -= _tRate * Time.fixedDeltaTime;
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

    private Vector3 InterpolateScale(float t)
    {
        float x = Mathf.Lerp(_baseScale.x, _targetScale.x, t);
        float y = Mathf.Lerp(_baseScale.y, _targetScale.y, t);

        if (x >= MAX_SCALE)
        {
            x = MAX_SCALE;
        }

        if (y >= MAX_SCALE)
        {
            y = MAX_SCALE;
        }

        return new Vector3(x, y, transform.localScale.z);
    }

    private void Eat(Interaction interaction)
    {
        throw new NotImplementedException();
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying && Application.isEditor)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, _acceleration);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, _rb.linearVelocity);
        }
    }

    private void OnGUI()
    {
        if (_showDebug)
        {
            string text = $"PET DEBUG INFO\n" +
                $"Linear velocity vector (v): {_rb.linearVelocity}\n" +
                $"v-magnitude: {MathF.Round(_rb.linearVelocity.magnitude, 3)}\n" +
                $"Acceleration vector (a): {_acceleration}\n" +
                $"a-magnitude: {MathF.Round(_acceleration.magnitude, 3)}\n" +
                $"Base scale vector: {_baseScale}\n" +
                $"Local scale vector: {transform.localScale}\n" +
                $"Target scale vector: {_targetScale}\n" +
                $"t:{_t})";
            GUILayout.Box(text);
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Toggle Debug Info"))
        {
            _showDebug = !_showDebug;
        }
    }
}