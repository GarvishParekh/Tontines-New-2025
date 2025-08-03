using UnityEngine;

namespace UI.ToolKit
{

    [RequireComponent(typeof(CanvasGroup))]
    public class ZoomOutFadeInCanvasAnimation : MonoBehaviour, ICanvasAnimation
    {
        CanvasGroup canvasGroup;
        [Header("<b>Scriptable")]
        [SerializeField] private ZoomOutFadeInAnimationData animationData;


        [InspectorName("objectToZoom-Optional")]
        [SerializeField]
        private Transform objectToZoom;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void DisableAnimation()
        {
            canvasGroup.alpha = 0;

            if (objectToZoom == null) transform.localScale = animationData.defaultSize;
            else objectToZoom.localScale = animationData.defaultSize;
        }

        public void EnableAnimation()
        {
            ResetCanvas();
            LeanTween.alphaCanvas(canvasGroup, 1, animationData.fadeInDuration).setEaseInOutSine();

            if (objectToZoom == null)
                LeanTween.scale(gameObject, animationData.endSize, animationData.zoomOutDuration).setEaseInOutSine();
            else
                LeanTween.scale(objectToZoom.gameObject, animationData.endSize, animationData.zoomOutDuration).setEaseInOutSine();
        }

        public void ResetCanvas()
        {
            canvasGroup.alpha = 0;
            if (objectToZoom == null) transform.localScale = animationData.defaultSize;
            else objectToZoom.localScale = animationData.defaultSize;
        }
    }
}