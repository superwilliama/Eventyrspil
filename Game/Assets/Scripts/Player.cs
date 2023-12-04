using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _speed = 5f;

    private Dictionary<string, string> _walkToIdleMap = new Dictionary<string, string>
    {
        {"PlayerWalkSouth", "PlayerIdleSouth"},
        {"PlayerWalkSouthEast", "PlayerIdleEast"},
        {"PlayerWalkEast", "PlayerIdleEast"},
        {"PlayerWalkNorthEast", "PlayerIdleNorthEast"},
        {"PlayerWalkNorth", "PlayerIdleNorth"},
        {"PlayerWalkNorthWest", "PlayerIdleNorthWest"},
        {"PlayerWalkWest", "PlayerIdleWest"},
        {"PlayerWalkSouthWest", "PlayerIdleSouthWest"}
    };

    private InputManager _input;

    private void Start() => _input = InputManager.Instance;

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _input.OnMove().normalized * _speed * Time.fixedDeltaTime);
    }

    private void Update()
    {
        string inputValue = _input.OnMove().ToString();

        switch (inputValue)
        {
            case "(0.00, 0.00)":
                FaceCorrectDirection();
                break;
            case "(0.00, -1.00)":
                _animator.Play("PlayerWalkSouth");
                break;
            case "(0.71, -0.71)":
                _animator.Play("PlayerWalkSouthEast");
                break;
            case "(1.00, 0.00)":
                _animator.Play("PlayerWalkEast");
                break;
            case "(0.71, 0.71)":
                _animator.Play("PlayerWalkNorthEast");
                break;
            case "(0.00, 1.00)":
                _animator.Play("PlayerWalkNorth");
                break;
            case "(-0.71, 0.71)":
                _animator.Play("PlayerWalkNorthWest");
                break;
            case "(-1.00, 0.00)":
                _animator.Play("PlayerWalkWest");
                break;
            case "(-0.71, -0.71)":
                _animator.Play("PlayerWalkSouthWest");
                break;
        }
    }

    private void FaceCorrectDirection()
    {
        foreach (var walkState in _walkToIdleMap)
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName(walkState.Key))
            {
                _animator.Play(walkState.Value);
                break;
            }
        }
    }
}
