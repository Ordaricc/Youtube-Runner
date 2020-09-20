using UnityEngine;

public class OctopusEntity : EntityType
{
    [SerializeField] private GameObject[] tentacles;

    public void ChooseTentacle()
    {
        tentacles[Random.Range(0, 2)].SetActive(true);
    }

    public override void StartEntity()
    {
        ChooseTentacle();
    }
}