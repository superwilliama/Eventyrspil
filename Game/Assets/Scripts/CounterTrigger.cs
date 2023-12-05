using UnityEngine;

public class CounterTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _pressEBox;

    private LevelManager _level;
    private InputManager _input;
    private CharacterManager _character;

    private bool canLoad;

    private void Start()
    {
        _input = InputManager.Instance;
        _level = LevelManager.Instance;
        _character = CharacterManager.Instance;

        _pressEBox.SetActive(false);
    }

    private void Update()
    {
        if (_character.seated)
        {
            if (canLoad && _input.OnInteract())
            {
                _character.seated = false;
                _level.LoadLevel(1);
            }
            else
                _pressEBox.SetActive(true);
        }
        else
        {
            _pressEBox.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            canLoad = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            canLoad = false;
    }
}
