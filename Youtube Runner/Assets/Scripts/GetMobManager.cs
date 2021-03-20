using UnityEngine;

public class GetMobManager : MonoBehaviour
{
    public static GetMobManager Instance;

    [SerializeField] private int howManyEnemiesUnlocked;
    [SerializeField] private int howManyEnemiesUnlockedMax;

    [SerializeField] private float chanceToSpawnBooty = 50;

    [SerializeField] private EntityType.EntityTypes[] entitiesIDs;

    private bool previousSpawnWasOctopus;

    private void Awake()
    {
        Instance = this;
        howManyEnemiesUnlockedMax = entitiesIDs.Length - 2;
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
            return EntitiesMagazine.Instance.RequestEntityFromMagazine(entitiesIDs[0]);//return booty
        }

        EntityType.EntityTypes entityToChoose;
        while (true)
        {
            isOctopus = false;
            entityToChoose = entitiesIDs[Random.Range(1, 2 + howManyEnemiesUnlocked)];
            if (entityToChoose == EntityType.EntityTypes.octopus)
                isOctopus = true;

            if (previousSpawnWasOctopus && isOctopus)
                continue;
            else if (isOctopus)
                previousSpawnWasOctopus = true;
            else
                previousSpawnWasOctopus = false;

            UnlockAchievement(entityToChoose);

            break;
        }

        return EntitiesMagazine.Instance.RequestEntityFromMagazine(entityToChoose);
    }

    private void UnlockAchievement(EntityType.EntityTypes entityType)
    {
        switch (entityType)
        {
            case EntityType.EntityTypes.octopus:
                AchievementsManager.Instance.UnlockAchievement(Achievement.AchievementTypes.ocotpus);
                break;

            case EntityType.EntityTypes.orca:
                AchievementsManager.Instance.UnlockAchievement(Achievement.AchievementTypes.horca);
                break;

            case EntityType.EntityTypes.whirlwind:
                AchievementsManager.Instance.UnlockAchievement(Achievement.AchievementTypes.whirlwind);
                break;
        }
    }

    public void AddEnemy()
    {
        if (howManyEnemiesUnlocked < howManyEnemiesUnlockedMax)
            howManyEnemiesUnlocked++;
    }
}