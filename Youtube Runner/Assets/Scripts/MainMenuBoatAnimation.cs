using UnityEngine;

public class MainMenuBoatAnimation : MonoBehaviour
{
    [SerializeField] private string animationClipName = "MainMenuBoat";
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Invoke("StartAnimation", Random.Range(1f, 3f));
    }

    private void StartAnimation()
    {
        animator.Play(animationClipName);
    }

    private void OnAnimationEnd()
    {
        Invoke("StartAnimation", Random.Range(0f, 2f));
    }
}