using UnityEngine;
using System.Collections;

public class MainMenuAnimationCharacterController : MonoBehaviour
{
    [SerializeField] private Animator maleAnimator;

    private void Start()
    {
        StartCoroutine(nameof(TrackAnimation));
    }
    private IEnumerator TrackAnimation()
    {
        while (true)
        {
            maleAnimator.SetTrigger("GoBackToIdel");
            yield return new WaitForSeconds(12);
            maleAnimator.ResetTrigger("GoBackToIdel");
            Debug.Log("Do some action");
            
            int chooseRandomAnimation = UnityEngine.Random.Range(0, 5);
            switch (chooseRandomAnimation)
            {
                case 0:
                    maleAnimator.SetTrigger("HandsOnHip");
                    break;
                case 1:
                    maleAnimator.SetTrigger("CheckWatch");
                    break;
                case 2:
                    maleAnimator.SetTrigger("LookBehind");
                    break;
                case 3:
                    maleAnimator.SetTrigger("StreachLegs");
                    break;
                case 4:
                    maleAnimator.SetTrigger("StreachQuads");
                    break;
            }

            yield return new WaitForSeconds(5);
            Debug.Log("Back to idel");
            maleAnimator.SetTrigger("GoBackToIdel");
        }
    }
}
