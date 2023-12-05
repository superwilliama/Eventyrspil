using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private Player _player;

    [Header("Components")]
    [SerializeField] private TMP_Text _text;
    private Queue<Sentence> _sentences;
    private Sentence _currentSentence;

    [SerializeField] private GameObject _interactionBox;
    [SerializeField] private GameObject _pressEBox;

    private bool _isTyping = false;
    [HideInInspector] public bool interactionIsActive = false;
    [HideInInspector] public bool interactionHasEnded = false;

    [Header("Configuration")]
    [SerializeField] private float _nextTimeToInteract = 1f;
    private float _interactTimer = 0f;

    private float _previousSpeed;

    private static InteractionManager _instance;
    public static InteractionManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = this;

        _sentences = new Queue<Sentence>();

        _interactionBox.SetActive(false);
    }

    private void Update()
    {
        if (!interactionIsActive)
        {
            _interactTimer += Time.deltaTime;
        }
    }

    public void Interact(Sentence[] sentences)
    {
        _pressEBox.SetActive(false);

        if (!interactionIsActive && _interactTimer >= _nextTimeToInteract)
        {
            StartInteraction(sentences);
        }
        else if (interactionIsActive)
        {
            NextSentence();
        }
    }

    private void StartInteraction(Sentence[] sentences)
    {
        interactionIsActive = true;
        _interactTimer = 0f;

        _previousSpeed = _player.speed;
        _player.speed = 0;

        _interactionBox.SetActive(true);
        _sentences.Clear();

        foreach (Sentence sentence in sentences)
        {
            _sentences.Enqueue(sentence);
        }

        NextSentence();
    }

    private void NextSentence()
    {
        if (_sentences.Count == 0 && !_isTyping)
        {
            EndInteraction();
        }
        else if (_isTyping)
        {
            SkipTyping();
        }
        else if (!_isTyping)
        {
            _currentSentence = _sentences.Dequeue();

            StartCoroutine(TypeSentence(_currentSentence));
        }
    }

    private IEnumerator TypeSentence(Sentence sentence)
    {
        _isTyping = true;

        _text.text = ""; // Clear the text
        foreach (char letter in sentence.text)
        {
            _text.text += letter;

            if (letter == '.' || letter == '!' || letter == '?' || letter == ',')
                yield return new WaitForSeconds(0.5f);
            else
                yield return new WaitForSeconds(sentence.secondsPerWord);
        }

        _isTyping = false;
    }

    private void SkipTyping()
    {
        StopAllCoroutines();

        _text.text = _currentSentence.text;
        _isTyping = false;
        return;
    }

    public void EndInteraction()
    {
        StopAllCoroutines();

        _player.speed = _previousSpeed;

        _interactionBox.SetActive(false);

        interactionIsActive = false;

        interactionHasEnded = true;
    }
}