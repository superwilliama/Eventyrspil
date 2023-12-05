using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [Header("Characters")]
    [SerializeField] private Character _tinkerBell;
    [SerializeField] private Character _littleRedRidingHood;
    [SerializeField] private Character _snowWhite;

    [Header("Locations")]
    [SerializeField] private Vector2 _enterPos;
    [SerializeField] private Vector2[] _barPos;
    [SerializeField] private Vector2[] _tinkerBellTablePos;
    [SerializeField] private Vector2[] _littleRedRidingHoodTablePos;
    [SerializeField] private Vector2[] _snowWhiteTablePos;

    [Header("UI")]
    [SerializeField] private GameObject _pressEBox;

    private Dictionary<string, string> _walkToIdleMap = new Dictionary<string, string>
    {
        {"PlayerWalkSouth", "PlayerIdleSouth"},
        {"PlayerWalkEast", "PlayerIdleEast"},
        {"PlayerWalkNorth", "PlayerIdleNorth"},
        {"PlayerWalkWest", "PlayerIdleWest"},
    };

    [HideInInspector] public Character currentCharacter;

    [HideInInspector] public bool seated;

    private InteractionManager _interaction;

    private static CharacterManager _instance;
    public static CharacterManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = this;
    }

    private void Start()
    {
        _interaction = InteractionManager.Instance;

        _pressEBox.SetActive(false);

        StartCoroutine(CharacterEnterBar(_tinkerBell));
    }

    private void Update()
    {
        if (_interaction.interactionHasEnded && !seated)
        {
            _interaction.interactionHasEnded = false;
            StartCoroutine(CharacterGoToTable(_tinkerBell, _tinkerBellTablePos));
        }
    }

    private IEnumerator CharacterEnterBar(Character character)
    {
        character.transform.position = _enterPos;

        yield return new WaitForSeconds(1);

        for (int i = 0; i < _barPos.Length; i++)
        {
            float elapsedTime = 0;

            Vector2 startPos;

            if (i == 0)
                startPos = _enterPos;
            else
                startPos =_barPos[i - 1];

            while (elapsedTime < character.timeToWalk * Vector2.Distance(startPos, _barPos[i]))
            {
                elapsedTime += Time.deltaTime;
                character.transform.position = Vector2.Lerp(startPos, _barPos[i], elapsedTime / (character.timeToWalk * Vector2.Distance(startPos, _barPos[i])));
                WalkCorrectDirection(character, startPos, _barPos[i]);

                yield return null;
            }

            character.transform.position = _barPos[i];
        }

        FaceCorrectDirection(character);
        currentCharacter = character;

        _pressEBox.SetActive(true);
    }

    private IEnumerator CharacterGoToTable(Character character, Vector2[] tablePos)
    {
        seated = true;

        character.transform.position = _barPos[_barPos.Length - 1];

        for (int i = 0; i < tablePos.Length; i++)
        {
            float elapsedTime = 0;

            Vector2 startPos;

            if (i == 0)
                startPos = _barPos[_barPos.Length - 1];
            else
                startPos = tablePos[i - 1];

            while (elapsedTime < character.timeToWalk * Vector2.Distance(startPos, tablePos[i]))
            {
                elapsedTime += Time.deltaTime;
                character.transform.position = Vector2.Lerp(startPos, tablePos[i], elapsedTime / (character.timeToWalk * Vector2.Distance(startPos, tablePos[i])));
                WalkCorrectDirection(character, startPos, tablePos[i]);

                yield return null;
            }

            character.transform.position = tablePos[i];
        }

        FaceCorrectDirection(character);
        currentCharacter = character;
    }

    private void WalkCorrectDirection(Character character, Vector2 fromPos, Vector2 toPos)
    {
        if (fromPos.x - toPos.x < 0)
        { 
            character.animator.Play("PlayerWalkEast");
        }
        else if (fromPos.x - toPos.x > 0)
        {
            character.animator.Play("PlayerWalkWest");
        }
        else if (fromPos.y - toPos.y < 0)
        {
            character.animator.Play("PlayerWalkNorth");
        }
        else if (fromPos.y - toPos.y > 0)
        {
            character.animator.Play("PlayerWalkSouth");
        }
    }

    private void FaceCorrectDirection(Character character)
    {
        foreach (var walkState in _walkToIdleMap)
        {
            if (character.animator.GetCurrentAnimatorStateInfo(0).IsName(walkState.Key))
            {
                character.animator.Play(walkState.Value);
                break;
            }
        }
    }
}
