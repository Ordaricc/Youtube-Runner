﻿using System.Collections;
using UnityEngine;

public class Thunderstorm : MonoBehaviour
{
    public static Thunderstorm Instance;

    [SerializeField] private Animator nightPanelAnimator;
    [SerializeField] private Animator lightningPanelAnimator;

    [SerializeField] private float lightningCooldownMin = 2;
    [SerializeField] private float lightningCooldownMax = 7;

    private void Awake()
    {
        Instance = this;
    }

    public void ActivateThunderstorm()
    {
        if (AchievementsManager.Instance != null)//check if we are in the game and not the main menu
            AchievementsManager.Instance.UnlockAchievement(Achievement.AchievementTypes.thunderstorm);

        nightPanelAnimator.Play("FadeInNight");
        StartCoroutine(ThunderstormLoop());
    }

    private IEnumerator ThunderstormLoop()
    {
        while (true)
        {
            float coolDown = Random.Range(lightningCooldownMin, lightningCooldownMax);
            yield return new WaitForSeconds(coolDown);
            CastLightning();
        }
    }

    private void CastLightning()
    {
        lightningPanelAnimator.Play("Lightning");
        //audio effect
        //camera shake
    }
}