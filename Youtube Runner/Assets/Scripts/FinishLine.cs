using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Entity"))
        {
            collision.GetComponent<EntityType>().OnHitFinishLine();
            
            Destroy(collision.gameObject);
        }
    }
}