using UnityEngine;

public class PlayAnimationByString : MonoBehaviour
{
    public Animation animationComponent;
    public string idleAnimationName = "Idle";

    private void Start()
    {
        PlayAnimation(idleAnimationName);
    }

    public void PlayAnimation(string animationName)
    {
        if (animationComponent == null) return;

        if (animationComponent.GetClip(animationName) != null)
        {
            animationComponent.CrossFade(animationName);
        }
        else
        {
            if (!string.IsNullOrEmpty(idleAnimationName) && animationComponent.GetClip(idleAnimationName) != null)
            {
                animationComponent.CrossFade(idleAnimationName);
            }
        }
    }
}
