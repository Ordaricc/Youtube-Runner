using UnityEngine;

public class OctopusEntity : EntityType
{
    [SerializeField] private GameObject[] tentacles;

    private void ChooseTentacle()
    {
        foreach (GameObject t in tentacles)
        {
            t.SetActive(false);
        }

        tentacles[Random.Range(0, 2)].SetActive(true);
    }

    public override void StartEntity()
    {
        ChooseTentacle();
    }
}