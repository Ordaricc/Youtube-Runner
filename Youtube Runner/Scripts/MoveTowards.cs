using UnityEngine;

public class MoveTowards : MonoBehaviour, IHeadStartReceiver
{
    [SerializeField] private Vector3 positionToGoBackTo;  
    [SerializeField] private Vector3 destination;

    [SerializeField] private float scalingMultiplier;
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    private bool isSpeedingUp;
    
    private void Update()
    {
        if (Time.timeScale == 0)
            return;

        transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.timeScale);

        if (transform.position == destination)
            transform.position = positionToGoBackTo;

        if (isSpeedingUp)
        {
            speed *= scalingMultiplier;
            if (speed >= maxSpeed)
            {
                speed = maxSpeed;
                isSpeedingUp = false;
            }
        }
    }

    public void StartScript()
    {
        isSpeedingUp = true;
    }

    public void ActivateHeadstart(float speedMultiplier)
    {
        isSpeedingUp = false;
        speed = maxSpeed * speedMultiplier;
    }

    public void EndHeadstart()
    {
        speed = maxSpeed;
    }
}