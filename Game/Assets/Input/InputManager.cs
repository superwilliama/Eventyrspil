using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public static InputManager Instance { get { return _instance; } }

    private InputMaster _inputMaster;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        _inputMaster = new InputMaster();
    }

    private void OnEnable() => _inputMaster.Enable();

    private void OnDisable() => _inputMaster.Disable();

    public Vector2 OnMove() => _inputMaster.Player.Move.ReadValue<Vector2>();

    public Vector2 OnCursorPos() => _inputMaster.Player.Cursor.ReadValue<Vector2>();

    public bool OnClickPress() => _inputMaster.Player.Click.WasPressedThisFrame();

    public bool OnClickRelease() => _inputMaster.Player.Click.WasReleasedThisFrame();

    public bool OnClickHold() => _inputMaster.Player.Click.IsPressed();
}
