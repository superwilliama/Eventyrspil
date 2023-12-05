using UnityEngine;

public class Glass : MonoBehaviour
{
    private InputManager _input;

    [Header("Physics")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private SpringJoint2D _joint;
    [SerializeField] private float _gravityScale = 1f;
    private Vector2 _offset;

    [HideInInspector] public bool isDragging;

    [Header("Line")]
    [SerializeField] private Transform _pullPoint;
    [SerializeField] private float _lineSize;
    private LineDrawer _lineDrawer;


    [Header("Drinks")]
    [SerializeField] private Drink[] _drinks;
    private int _sweetLiquidAmount;
    private int _bitterLiquidAmount;
    private int _sparklyLiquidAmount;

    private void Start()
    {
        _input = InputManager.Instance;
        _rb.gravityScale = _gravityScale;
        _lineDrawer = new LineDrawer();
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
        if (isDragging)
        {
            _lineDrawer.DrawLine(_pullPoint.position, (Vector2)Camera.main.ScreenToWorldPoint(_input.OnCursorPos()), Color.white, _lineSize);
        }
        else
        {
            _lineDrawer.DestroyLine();
        }
    }

    public void CalculateDrink()
    {
        for (int i = 0; i < _drinks.Length; i++)
        {
            if (_sweetLiquidAmount >= _drinks[i].sweetLiquidNeeded && _bitterLiquidAmount >= _drinks[i].bitterLiquidNeeded && _sparklyLiquidAmount >= _drinks[i].sparklyLiquidNeeded)
            {
                StaticData.currentDrink = _drinks[i].name;
                switch (_drinks[i].name)
                {
                    case "Apple Juice":
                        StaticData.drinkDescription = "Sweet! I made some apple juice.";
                        break;
                    case "Cranberry Juice":
                        StaticData.drinkDescription = "Sour-prise! Just whipped up some cranberry juice!";
                        break;
                    case "Bubbly Lemonade":
                        StaticData.drinkDescription = "Wow! This lemonade sparks with freshness!";
                        break;
                    case "Apple-Cranberry Punch":
                        StaticData.drinkDescription = "Mmm... a punch of contrasting flavors.";
                        break;
                    case "Bubbly Cranberry-Lemonade":
                        StaticData.drinkDescription = "An acquired taste for sure!";
                        break;
                    case "Bubbly Apple-Lemonade":
                        StaticData.drinkDescription = "Mmm... the taste of a hot summer breeze.";
                        break;
                    case "Exotic Elixir":
                        StaticData.drinkDescription = "Woah... this looks enchanting...";
                        break;
                }

                return;
            }
        }
        StaticData.drinkDescription = "EEEEEWWWWWW- HOW DID I EVEN MANAGE TO MAKE THIS!?!?";
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
}
