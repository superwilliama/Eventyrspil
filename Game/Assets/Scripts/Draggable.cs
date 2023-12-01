using UnityEngine;

public class Draggable : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private SpringJoint2D _joint;

    [HideInInspector] public bool isDragging;

    private Vector2 _offset;

    private InputManager _input;

    private void Start() => _input = InputManager.Instance;

    private void OnMouseDown()
    {
        isDragging = true;

        _rb.gravityScale = 0;
        _joint.enabled = true;
        _offset = (Vector2)transform.position - (Vector2)Camera.main.ScreenToWorldPoint(_input.OnCursorPos());
    }

    private void OnMouseDrag()
    {
        _joint.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(_input.OnCursorPos()) + _offset;
    }

    private void OnMouseUp()
    {
        isDragging = false;

        _joint.enabled = false;
        _rb.gravityScale = 1;
    }

    private void OnDrawGizmos()
    {
        if (isDragging)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, _joint.transform.position);
        }
    }
}
