using UnityEngine;

public class GetMobManager : MonoBehaviour
{
    public static GetMobManager Instance;

    [SerializeField] private int howManyEnemiesUnlocked;
    [SerializeField] private int howManyEnemiesUnlockedMax;

    [SerializeField] private float chanceToSpawnBooty = 50;

    [SerializeField] private GameObject[] entitiesPrefabs;

    private bool previousSpawnWasOctopus;

    private void Awake()
    {
        Instance = this;
        howManyEnemiesUnlockedMax = entitiesPrefabs.Length - 2;
    }

    public void StartScript()
    {
        chanceToSpawnBooty += (float)PlayerPrefs.GetInt(ShopItem.ShopItems.pirateLuck.ToString()) / 2;
    }

    public GameObject GetMob(out bool isOctopus)
    {
        float randomNumberToChooseBetweenBootyOrEnemy = Random.Range(1f, 100f);
        if (randomNumberToChooseBetweenBootyOrEnemy <= chanceToSpawnBooty)
        {
            isOctopus = false;
            return entitiesPrefabs[0];
        }

        GameObject entityToChoose;
        while (true)
        {
            isOctopus = false;
            entityToChoose = entitiesPrefabs[Random.Range(1, 2 + howManyEnemiesUnlocked)];
            EntityType.EntityTypes entityType = entityToChoose.GetComponent<EntityType>().entityType;
            if (entityType == EntityType.EntityTypes.octopus)
                isOctopus = true;

            if (previousSpawnWasOctopus && isOctopus)
                continue;
            else if (isOctopus)
                previousSpawnWasOctopus = true;
            else
                previousSpawnWasOctopus = false;

            UnlockAchievement(entityType);

            break;
        }
        
        return entityToChoose;
    }

    private void UnlockAchievement(EntityType.EntityTypes entityType)
    {
        switch (entityType)
        {
            case EntityType.EntityTypes.octopus:
                AchievementsManager.Instance.UnlockAchievement(Achievement.AchievemntTypes.ocotpus);
                break;

            case EntityType.EntityTypes.orca:
                AchievementsManager.Instance.UnlockAchievement(Achievement.AchievemntTypes.horca);
                break;

            case EntityType.EntityTypes.whirlwind:
                AchievementsManager.Instance.UnlockAchievement(Achievement.AchievemntTypes.whirlwind);
                break;
        }
    }

    public void AddEnemy()
    {
        if (howManyEnemiesUnlocked < howManyEnemiesUnlockedMax)
            howManyEnemiesUnlocked++;
    }
}