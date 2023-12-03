using UnityEngine;
using UnityEngine.SceneManagement;

public class CounterTrigger : MonoBehaviour
{
    private InputManager _input;

    private void Start() => _input = InputManager.Instance;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && _input.OnInteract())
        {
            print("pressed");
        }
    }
}
