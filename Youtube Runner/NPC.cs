using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private Vector3 startingPosition;
    [SerializeField] private float speed = 3;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ActivateNPC()
    {
        gameObject.SetActive(true);
        transform.position = startingPosition;
        rb.velocity = new Vector2(0, -speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            NPCManager.Instance.StartNPCDialogue();
        }
    }

    public void DisableNPC()
    {
        Invoke("DisableNPCAfterDelay", 2);
    }

    private void DisableNPCAfterDelay()
    {
        SpawnManager.Instance.ChangeCanSpawnTo(true);
        gameObject.SetActive(false);
    }
}