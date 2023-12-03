using UnityEngine;

public class Glass : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private SpringJoint2D _joint;
    [SerializeField] private Transform _pullPoint;
    [SerializeField] private float _gravityScale = 1.0f;

    [SerializeField] private Drink[] _drinks;

    [HideInInspector] public bool isDragging;

    [SerializeField] private int _sweetLiquidAmount;
    [SerializeField] private int _bitterLiquidAmount;
    [SerializeField] private int _sparklyLiquidAmount;

    private Vector2 _offset;

    private InputManager _input;

    private void Start()
    {
        _input = InputManager.Instance;
        _rb.gravityScale = _gravityScale;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int blueLiquidLayer = 6;
        int redLiquidLayer = 7;
        int greenLiquidLayer = 8;

        switch(collision.gameObject.layer)
        {
            case var value when value == blueLiquidLayer:
                _sweetLiquidAmount++;
                break;
            case var value when value == redLiquidLayer:
                _bitterLiquidAmount++;
                break;
            case var value when value == greenLiquidLayer:
                _sparklyLiquidAmount++;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        int blueLiquidLayer = 6;
        int redLiquidLayer = 7;
        int greenLiquidLayer = 8;

        switch (collision.gameObject.layer)
        {
            case var value when value == blueLiquidLayer:
                _sweetLiquidAmount--;
                break;
            case var value when value == redLiquidLayer:
                _bitterLiquidAmount--;
                break;
            case var value when value == greenLiquidLayer:
                _sparklyLiquidAmount--;
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CalculateDrink();
        }
    }

    private void CalculateDrink()
    {
        for (int i = 0; i < _drinks.Length; i++)
        {
            if (_sweetLiquidAmount >= _drinks[i].sweetLiquidNeeded && _bitterLiquidAmount >= _drinks[i].bitterLiquidNeeded && _sparklyLiquidAmount >= _drinks[i].sparklyLiquidNeeded)
            {
                print("you made " + _drinks[i].name + "!");
                switch (_drinks[i].name)
                {
                    case "Apple Juice":
                        print("Sweet! I made some apple juice.");
                        break;
                    case "Cranberry Juice":
                        print("Sour-prise! Just whipped up some cranberry juice!");
                        break;
                    case "Bubbly Lemonade":
                        print("Wow! This lemonade sparks with freshness!");
                        break;
                    case "Apple-Cranberry Punch":
                        print("Mmm... a punch of contrasting flavors.");
                        break;
                    case "Bubbly Cranberry-Lemonade":
                        print("An acquired taste for sure!");
                        break;
                    case "Bubbly Apple-Lemonade":
                        print("Mmm... the taste of a hot summer breeze.");
                        break;
                    case "Exotic Elixir":
                        print("Woah... this looks enchanting...");
                        break;
                }

                return;
            }
        }
        print("WHAT THE HELL HAVE YOU MADE?!?!?");
    }

    private void OnMouseDown()
    {
        _pullPoint.position = (Vector2)Camera.main.ScreenToWorldPoint(_input.OnCursorPos());

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
        _rb.gravityScale = _gravityScale;
    }

    private void OnDrawGizmos()
    {
        if (isDragging)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(_pullPoint.position, (Vector2)Camera.main.ScreenToWorldPoint(_input.OnCursorPos()));
        }
    }
}
