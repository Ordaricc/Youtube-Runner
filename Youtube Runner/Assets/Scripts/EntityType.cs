using UnityEngine;

public class EntityType : MonoBehaviour
{
    public enum EntityTypes { booty, rock, octopus, tentacle, orca, whirlwind }
    public EntityTypes entityType;

    [SerializeField] private bool isAnimal;
    public bool _isAnimal { get { return isAnimal; } }

    public virtual void StartEntity()
    {

    }

    public virtual void OnHitFinishLine()
    {

    }
}