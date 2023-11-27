using UnityEngine;

public class Draggable : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private SpringJoint2D _joint;

    private Vector2 _offset;

    private void OnMouseDown()
    {
        _rb.gravityScale = 0;
        _joint.enabled = true;
        _offset = (Vector2)transform.position - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        _joint.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + _offset;
    }

    private void OnMouseUp()
    {
        _joint.enabled = false;
        _rb.gravityScale = 1;
    }
}
