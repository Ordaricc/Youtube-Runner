using UnityEngine;

public class BoatMovement : MonoBehaviour, IHeadStartReceiver
{
    public static BoatMovement Instance;

    private Camera mainCamera;
    private Rigidbody2D rb;
    
    [SerializeField] private float yMarginForInput = 2;
    [SerializeField] private float xMargin = 2;
    [SerializeField] private float boatSpeed = 50;

    private bool canMove;

    private float windForce;
    private float whirlwindForce;

    private void Awake()
    {
        Instance = this;

        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!canMove)
            return;

        int directionX = TakeInput();
        transform.rotation = Quaternion.Euler(0, 0, -30 * directionX);
        
        rb.velocity = new Vector2(directionX * boatSpeed, 0);
        rb.velocity += new Vector2(windForce + whirlwindForce, 0);

        ClampPositionWithinScreen();
    }

    private int TakeInput()
    {
        int directionX = 0;

        if (Application.isEditor)
        {
            if (Input.GetKey(KeyCode.D))
                directionX = 1;
            else if (Input.GetKey(KeyCode.A))
                directionX = -1;
        }
        else//if application.isMobile
        {
            if (Input.touches.Length > 0)
            {
                Vector3 touchPosition = Input.touches[0].position;
                touchPosition = mainCamera.ScreenToWorldPoint(touchPosition);

                if (touchPosition.y < yMarginForInput)
                {
                    if (touchPosition.x > 0)
                        directionX = 1;
                    else
                        directionX = -1;
                }
            }
        }

        return directionX;
    }

    private void ClampPositionWithinScreen()
    {
        float posX = transform.position.x;
        posX = Mathf.Clamp(posX, -xMargin, xMargin);
        transform.position = new Vector3(posX, transform.position.y, transform.position.z);
    }

    public void StartScript()
    {
        canMove = true;

        int strongerOarsLevel = PlayerPrefs.GetInt(ShopItem.ShopItems.strongerOars.ToString());

        if (strongerOarsLevel > 0)
            boatSpeed += strongerOarsLevel * 0.25f;
    }

    public void SetWind(float _windForce)
    {
        windForce = _windForce;
    }

    public void SetWhirlwind(float _whirlwindForce)
    {
        whirlwindForce = _whirlwindForce;
    }

    public void ActivateHeadstart(float speedMultiplier)
    {
        canMove = false;
    }

    public void EndHeadstart()
    {
        canMove = true;
    }
}