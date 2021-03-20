using System;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesMagazine : MonoBehaviour
{
    public static EntitiesMagazine Instance;

    [SerializeField] private EntityInMagazine[] entitiesInMagazine;
    public List<GameObject> entitiesInGame { get; private set; } = new List<GameObject>();

    private void Awake()
    {
        Instance = this;

        foreach (EntityInMagazine e in entitiesInMagazine)
        {
            e.entityID = e.entityPrefab.GetComponent<EntityType>().entityType;
            for (int i = 0; i < 4; i++)
            {
                InstantiateNewEntityIntoMagazine(e);
            }
        }
    }

    private void InstantiateNewEntityIntoMagazine(EntityInMagazine magazineToSpawnInto)
    {
        GameObject entitySpawned = Instantiate(magazineToSpawnInto.entityPrefab);
        entitySpawned.SetActive(false);
        magazineToSpawnInto.entitiesSpawned.Add(entitySpawned);
    }

    public GameObject RequestEntityFromMagazine(EntityType.EntityTypes entityID)
    {
        EntityInMagazine rightMagazine = Array.Find(entitiesInMagazine, dummyFind => dummyFind.entityID == entityID);
        if (rightMagazine == null)
        {
            Debug.LogError("Entity " + entityID + " not found!");
            return null;
        }

        if (rightMagazine.entitiesSpawned.Count == 0)
        {
            InstantiateNewEntityIntoMagazine(rightMagazine);
        }

        GameObject entityToReturn = rightMagazine.entitiesSpawned[0];
        entityToReturn.SetActive(true);
        entityToReturn.transform.rotation = Quaternion.identity;
        rightMagazine.entitiesSpawned.Remove(entityToReturn);
        entitiesInGame.Add(entityToReturn);
        return entityToReturn;
    }

    public void PutEntityIntoMagazine(GameObject entityToPutBack, EntityType.EntityTypes entityID)
    {
        EntityInMagazine rightMagazine = Array.Find(entitiesInMagazine, dummyFind => dummyFind.entityID == entityID);
        if (rightMagazine == null)
        {
            Debug.LogError("Entity " + entityID + " not found!");
            return;
        }

        entityToPutBack.SetActive(false);
        rightMagazine.entitiesSpawned.Add(entityToPutBack);
        entitiesInGame.Remove(entityToPutBack);
    }
}

[Serializable]
public class EntityInMagazine
{
    public GameObject entityPrefab;
    [HideInInspector] public List<GameObject> entitiesSpawned;
    [HideInInspector] public EntityType.EntityTypes entityID;
}