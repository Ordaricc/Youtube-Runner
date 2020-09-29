using UnityEngine;

public class Whirlwind : EntityType
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float dragForce = 2;
    [SerializeField] private float minDragForce = 1;
    [SerializeField] private float maxDragForce = 5;
    [SerializeField] private float dragRadius = 2;
    [SerializeField] private float minValueFromDragRadius = 1;

    private bool isPlayerBeingDragged;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, dragRadius);
    }

    public override void StartEntity()
    {
        dragForce = Random.Range(minDragForce, maxDragForce);
    }

    private void Update()
    {
        Collider2D hitPlayerCollider = Physics2D.OverlapCircle(transform.position, dragRadius, playerLayer);
        if (hitPlayerCollider)
        {
            float distance = Vector2.Distance(transform.position, hitPlayerCollider.transform.position);
            distance = Mathf.Clamp(distance, minValueFromDragRadius, dragRadius);
            if (transform.position.x < hitPlayerCollider.transform.position.x)
                distance *= -1;

            float forceToGive = dragForce / distance;

            BoatMovement.Instance.SetWhirlwind(forceToGive);

            isPlayerBeingDragged = true;
        }
        else if (isPlayerBeingDragged)
        {
            BoatMovement.Instance.SetWhirlwind(0);

            isPlayerBeingDragged = false;
        }
    }

    public override void OnHitFinishLine()
    {
        BoatMovement.Instance.SetWhirlwind(0);
    }
}