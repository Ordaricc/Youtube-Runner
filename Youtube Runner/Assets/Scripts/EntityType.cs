using UnityEngine;

public class EntityType : MonoBehaviour
{
    protected Rigidbody2D rb;

    public enum EntityTypes { booty, rock, octopus, tentacle, orca, whirlwind }
    public EntityTypes entityType;

    [SerializeField] private bool isAnimal;
    public bool _isAnimal { get { return isAnimal; } }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void StartEntity()
    {

    }

    public void UpdateSpeed(float newSpeed)
    {
        if (rb != null)
            rb.velocity = new Vector2(rb.velocity.x, -newSpeed);
    }

    public virtual void OnHitFinishLine()
    {
        if (entityType != EntityTypes.tentacle)
            EntititesMagazine.Instance.PutEntityIntoMagazine(gameObject, entityType);
    }

    public bool OnHitPlayerLoseLife()
    {
        if (entityType == EntityTypes.booty)
        {
            PlayerMoney.Instance.CollectBooty(1);
            EntititesMagazine.Instance.PutEntityIntoMagazine(gameObject, EntityTypes.booty);
            return false;
        }
        else
        {
            if (isAnimal)
            {
                if (Nets.Instance.TryRemoveNet())
                {
                    if (entityType == EntityTypes.tentacle)
                    {
                        EntititesMagazine.Instance.PutEntityIntoMagazine(gameObject.transform.parent.gameObject, EntityTypes.octopus);
                    }
                    else
                    {
                        EntititesMagazine.Instance.PutEntityIntoMagazine(gameObject, entityType);
                    }
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
    }
}