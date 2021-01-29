using System;
using System.Collections.Generic;
using UnityEngine;

public class EntititesMagazine : MonoBehaviour
{
    public static EntititesMagazine Instance;

    [SerializeField] private EntityInMagazine[] entitiesInMagazine;

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
    }
}

[Serializable]
public class EntityInMagazine
{
    public GameObject entityPrefab;
    [HideInInspector] public List<GameObject> entitiesSpawned;
    [HideInInspector] public EntityType.EntityTypes entityID;
}