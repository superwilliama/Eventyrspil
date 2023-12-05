using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;

    public float speed = 5f;

    private Dictionary<string, string> _walkToIdleMap = new Dictionary<string, string>
    {
        {"PlayerWalkSouth", "PlayerIdleSouth"},
        {"PlayerWalkEast", "PlayerIdleEast"},
        {"PlayerWalkNorth", "PlayerIdleNorth"},
        {"PlayerWalkWest", "PlayerIdleWest"},
    };

    private InputManager _input;
    private CharacterManager _character;
    private InteractionManager _interaction;

    private void Start()
    {
        _input = InputManager.Instance;
        _character = CharacterManager.Instance;
        _interaction = InteractionManager.Instance;
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _input.OnMove().normalized * speed * Time.fixedDeltaTime);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print(StaticData.currentDrink);
        }

        if (speed != 0)
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
                case "(1.00, 0.00)":
                    _animator.Play("PlayerWalkEast");
                    break;
                case "(0.00, 1.00)":
                    _animator.Play("PlayerWalkNorth");
                    break;
                case "(-1.00, 0.00)":
                    _animator.Play("PlayerWalkWest");
                    break;
            }
        }

        if (_character.currentCharacter != null)
        {
            if (Vector2.Distance(transform.position, _character.currentCharacter.transform.position) <= 2 && _input.OnInteract())
            {
                _interaction.Interact(_character.currentCharacter.startSentences);
            }
            else if (Vector2.Distance(transform.position, _character.currentCharacter.transform.position) > 2 && _interaction.interactionIsActive)
            {
                _interaction.EndInteraction();
            }
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
