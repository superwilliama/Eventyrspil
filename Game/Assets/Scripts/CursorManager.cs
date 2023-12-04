using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;

    [SerializeField] private Sprite _handOpenCursor;
    [SerializeField] private Sprite _handClosedCursor;

    private InputManager _input;

    private void Start()
    {
        _input = InputManager.Instance;
        Cursor.visible = false;
    }

    private void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(_input.OnCursorPos().x, _input.OnCursorPos().y, Camera.main.nearClipPlane));

        if (_input.OnClickPress())
            ChangeCursor(_handClosedCursor, 6f, 200);
        else if (_input.OnClickRelease())
            ChangeCursor(_handOpenCursor, 7.5f, 255);
    }

    private void ChangeCursor(Sprite cursor, float scale, byte brightness)
    {
        _renderer.sprite = cursor;
        transform.localScale = new Vector3(scale, scale, scale);
        _renderer.color = new Color32(brightness, brightness, brightness, 255);
    }
}
