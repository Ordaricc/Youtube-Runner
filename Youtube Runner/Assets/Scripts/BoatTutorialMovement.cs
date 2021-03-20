using UnityEngine;

public class BoatTutorialMovement : MonoBehaviour
{
    public static BoatTutorialMovement Instance;

    private Camera mainCamera;
    private Rigidbody2D rb;

    [SerializeField] private float yMarginForInput = 2;
    [SerializeField] private float xMargin = 2;
    [SerializeField] private float boatSpeed = 50;

    private bool canMove;
    
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

        if (posX >= xMargin)
        {
            TutorialManager.Instance.OnBoatReachRightPartOfScreen();
        }
        else if (posX <= -xMargin)
        {
            TutorialManager.Instance.OnBoatReachLeftPartOfScreen();
        }

        posX = Mathf.Clamp(posX, -xMargin, xMargin);
        transform.position = new Vector3(posX, transform.position.y, transform.position.z);
    }

    public void ChangeCanMoveTo(bool value)
    {
        canMove = value;
        GetComponent<Animator>().enabled = false;
    }
}