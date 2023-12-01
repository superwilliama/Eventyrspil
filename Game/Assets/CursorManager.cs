using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;

    [SerializeField] private Texture2D _handOpenCursor;
    [SerializeField] private Texture2D _handClosedCursor;

    private InputManager _input;

    private void Start()
    {
        _input = InputManager.Instance;
        Cursor.SetCursor(_handClosedCursor, new Vector2(10, 10), CursorMode.Auto);
    }

    private void Update()
    {
        if (_input.OnClickPress())
            Cursor.SetCursor(_handClosedCursor, new Vector2(10, 10), CursorMode.Auto);
        else if (_input.OnClickRelease())
            Cursor.SetCursor(_handOpenCursor, new Vector2(10, 10), CursorMode.Auto);
    }
}
