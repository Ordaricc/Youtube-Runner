using UnityEngine;

public class SpawnManager : MonoBehaviour, IHeadStartReceiver
{
    public static SpawnManager Instance;

    [SerializeField] private bool canSpawn;

    [SerializeField] private Vector3 spawnPosition;

    [SerializeField] private float xMargin = 2;
    [SerializeField] private float spawnTimer;

    [Header("Scaling Values")]
    [SerializeField] private float scalingMultiplier = 1.0001f;
    [SerializeField] private float spawnTimerMax = 3f;
    [SerializeField] private float scaledSpawnTimerMax = 0.5f;
    [SerializeField] private float entitiesSpeed = 50;
    [SerializeField] private float scaledEntitiesSpeed;
    private bool maxVelocityReached;

    private void Awake()
    {
        Instance = this;

        scaledEntitiesSpeed = spawnTimerMax / scaledSpawnTimerMax * entitiesSpeed;
    }

    private void Update()
    {
        if (!canSpawn)
            return;

        IncreaseSpeed();
        TrySpawn();   
    }

    private void TrySpawn()
    {
        if (spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
        }
        else
        {
            spawnTimer = spawnTimerMax;
            SpawnEntity();
        }
    }

    public void StartScript()
    {
        canSpawn = true;
        spawnTimer = spawnTimerMax;
    }

    private void SpawnEntity()
    {
        bool isEntityToSpawnAnOctopus;
        GameObject entityToSpawn = GetMobManager.Instance.GetMob(out isEntityToSpawnAnOctopus);

        spawnPosition.x = Random.Range(-xMargin, xMargin);
        if (isEntityToSpawnAnOctopus)
            spawnPosition.x = 0;

        GameObject spawnedEntity = Instantiate(entityToSpawn, spawnPosition, Quaternion.identity);
        spawnedEntity.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -entitiesSpeed);
        spawnedEntity.GetComponent<EntityType>().StartEntity();
    }

    private void IncreaseSpeed()
    {
        if (maxVelocityReached || Time.frameCount % 5 != 0 || Time.timeScale == 0)
            return;

        if (spawnTimerMax > scaledSpawnTimerMax)
            spawnTimerMax /= scalingMultiplier;
        else
            spawnTimerMax = scaledSpawnTimerMax;

        if (entitiesSpeed < scaledEntitiesSpeed)
            entitiesSpeed *= scalingMultiplier;
        else
            entitiesSpeed = scaledEntitiesSpeed;

        if (spawnTimerMax == scaledSpawnTimerMax
            && entitiesSpeed == scaledEntitiesSpeed)
            maxVelocityReached = true;

        GameObject[] entitiesInGame = GameObject.FindGameObjectsWithTag("Entity");
        foreach (GameObject e in entitiesInGame)
        {
            if (e.TryGetComponent(out Rigidbody2D entityRB))
                entityRB.velocity = new Vector2(entityRB.velocity.x, -entitiesSpeed);
        }
    }

    public void ActivateHeadstart(float speedMultiplier)
    {
        maxVelocityReached = true;
        spawnTimerMax = scaledSpawnTimerMax / speedMultiplier;
        spawnTimer = spawnTimerMax;
        entitiesSpeed = scaledEntitiesSpeed * speedMultiplier;
    }

    public void EndHeadstart()
    {
        spawnTimerMax = scaledSpawnTimerMax;
        entitiesSpeed = scaledEntitiesSpeed;
    }
}