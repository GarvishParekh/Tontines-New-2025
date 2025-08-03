using UnityEngine;

namespace UI.ToolKit
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeInCanvasAnimation : MonoBehaviour, ICanvasAnimation
    {
        CanvasGroup canvasGroup;

        [Header("<b>Scriptable")]
        [SerializeField] private FadeInAnimationData animationData;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void EnableAnimation()
        {
            ResetCanvas();
            LeanTween.alphaCanvas(canvasGroup, 1, animationData.animationDuration).setEaseInOutSine();
        }

        public void DisableAnimation()
        {
            canvasGroup.alpha = 0;
        }

        public void ResetCanvas()
        {
            canvasGroup.alpha = 0;
        }
    }
}