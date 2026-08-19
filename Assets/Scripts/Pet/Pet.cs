using UnityEngine;
using System;

public class Pet : MonoBehaviour
{
    public int MoveStrength = 100;
    public Consumable DebugConsumable;

    private AudioSystem _audioSystem;
    private Rigidbody _rb;
    private Animator _anim;
    private Stats _stats;
    private Vector3 _acceleration;
    private bool _showDebug = true;

    public float GetStat(Constants.StatNames name)
    {
        switch (name)
        {
            case Constants.StatNames.HEALTH:
                return _stats.GetHealth;
            case Constants.StatNames.HUNGER:
                return _stats.GetHunger;
            case Constants.StatNames.HYGIENE:
                return _stats.GetHygiene;
            case Constants.StatNames.HAPPY:
                return _stats.GetHappy;
            default:
                throw new Exception();
        }
    }

    private void Start()
    {
        _stats = new Stats();
        _acceleration = new Vector3();
        _audioSystem = FindAnyObjectByType<AudioSystem>();
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

        // Handle falling out of world
        if (transform.position.y < Constants.WORLD_FLOOR)
        {
            _rb.position = Constants.START_POS;
            _rb.angularVelocity = Vector3.zero;
            _rb.linearVelocity = Vector3.zero;
        }

        // DEBUG
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Consume(DebugConsumable);
        }
    }

    private void Consume(Consumable consumable)
    {
        _stats.Resolve(consumable);
        _audioSystem.Play(consumable.Sound);
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
                $"Local scale vector: {transform.localScale}\n";
            GUILayout.Box(text);
        }

        GUILayout.Space(10);

        string stats = $"Hunger: {_stats.GetHunger}, ";
        GUILayout.Box(stats);

        if (GUILayout.Button("Toggle Debug Info"))
        {
            _showDebug = !_showDebug;
        }
    }
}