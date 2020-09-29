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

    private void FixedUpdate()
    {
        if (!canMove)
            return;

        int dirX = 0;
        transform.rotation = Quaternion.Euler(0, 0, 0);

        if (Application.isEditor)
        {
            if (Input.GetKey(KeyCode.D))
            {
                dirX = 1;
                transform.rotation = Quaternion.Euler(0, 0, -30);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                dirX = -1;
                transform.rotation = Quaternion.Euler(0, 0, 30);
            }
        }
        else//if application.isMobile
        {
            if (Input.touches.Length > 0)
            {
                Vector3 touchPosition = Input.touches[0].position;
                touchPosition = mainCamera.ScreenToWorldPoint(touchPosition);
                
                if (touchPosition.x < yMarginForInput)
                {
                    if (touchPosition.x > 0)
                    {
                        dirX = 1;
                        transform.rotation = Quaternion.Euler(0, 0, -30);
                    }
                    else
                    {
                        dirX = -1;
                        transform.rotation = Quaternion.Euler(0, 0, 30);
                    }
                }
            }
        }

        rb.velocity = new Vector2(dirX * boatSpeed * Time.fixedDeltaTime, 0);
        rb.velocity += new Vector2(windForce + whirlwindForce, 0) * Time.fixedDeltaTime;

        ClampPositionWithinScreen();
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
            boatSpeed += strongerOarsLevel * 50;
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