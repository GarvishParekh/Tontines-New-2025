using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.ToolKit
{

    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasIdentity : MonoBehaviour
    {
        UiManager uiManager;
        CanvasGroup canvasGroup;
        ICanvasAnimation customAnimation;

        [SerializeField] private CanvasNames myCanvasName;
        [SerializeField] private List<GameObject> syntyObjectsWithAnimation = new List<GameObject>();

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            customAnimation = GetComponentInChildren<ICanvasAnimation>();
        }

        private void Start()
        {
            uiManager = UiManager.instance;
            uiManager.AddCanvas(this);
        }

        public CanvasNames GetCanvasName()
        {
            return myCanvasName;
        }

        public void EnableCanvas()
        {
            foreach (GameObject animatedObjects in syntyObjectsWithAnimation)
            {
                animatedObjects.SetActive(false);
            }
            if (customAnimation != null)
            {
                customAnimation.EnableAnimation();
            }

            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            StartCoroutine(nameof(EnalbleAnimation));
        }

        public void DisableCanvas()
        {
            if (customAnimation != null)
            {
                customAnimation.DisableAnimation();
            }

            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }

        private IEnumerator EnalbleAnimation()
        {
            yield return new WaitForSeconds(0.3f);
            foreach (GameObject animatedObjects in syntyObjectsWithAnimation)
            {
                animatedObjects.SetActive(false);
                animatedObjects.SetActive(true);
            }
        }
    }
}