﻿using UnityEngine;

public class UnlockEnemiesManager : MonoBehaviour
{
    [SerializeField] private Animator yardsTextAnim;
    [SerializeField] private int yardsToBeAtToIncreaseDifficulty;
    private int previousYardsMet;
    private int difficultyLevel;

    private void Update()
    {
        int currentYards = (int)YardsManager.Instance.yardsTraveled;
        if (currentYards % yardsToBeAtToIncreaseDifficulty == 0 
            && currentYards != previousYardsMet)
        {
            previousYardsMet = currentYards;
            IncreaseDifficulty();
        }
    }

    private void IncreaseDifficulty()
    {
        yardsTextAnim.Play("TextPopUp");
        //AudioManager.Instance.PlayClipByName("unlockEnemySound");
        difficultyLevel++;
        switch (difficultyLevel)
        {
            case 1:
                GetMobManager.Instance.AddEnemy();
                break;

            case 2:
                Fog.Instance.ActivateFog(false);
                break;

            case 3:
                GetMobManager.Instance.AddEnemy();
                break;

            case 4:
                Wind.Instance.ActivateWind();
                break;

            case 5:
                GetMobManager.Instance.AddEnemy();
                break;

            case 6:
                Thunderstorm.Instance.ActivateThunderstorm();
                break;

            case 7:
                NPCQuests.OnUnlockAllEnemies();
                enabled = false;
                break;
        }
    }
}