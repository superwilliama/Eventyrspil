using UnityEngine;

public class Draggable : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpringJoint2D joint;

    private Vector2 offset;

    private void OnMouseDown()
    {
        rb.gravityScale = 0;
        joint.enabled = true;
        offset = (Vector2)transform.position - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        joint.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
    }

    private void OnMouseUp()
    {
        joint.enabled = false;
        rb.gravityScale = 1;
    }
}
