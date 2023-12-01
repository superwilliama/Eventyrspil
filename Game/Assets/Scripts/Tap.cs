using UnityEngine;

public class Tap : MonoBehaviour
{
    [SerializeField] private GameObject _liquidParticlePrefab;

    [SerializeField] private Draggable glass;

    private InputManager _input;

    private void Start() => _input = InputManager.Instance;

    private void OnMouseOver()
    {
        if (_input.OnClickHold() && !glass.isDragging)
        {
            float randomX = Random.Range(transform.position.x + 0.1f, transform.position.x - 0.1f);
            float randomY = Random.Range(transform.position.y + 0.1f, transform.position.y - 0.1f);
            Vector3 randomPos = new Vector3(randomX, randomY);

            Instantiate(_liquidParticlePrefab, randomPos, Quaternion.identity);
        }
    }
}
