using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace UI.ToolKit
{
    public class UiManager : MonoBehaviour
    {
        public static UiManager instance;

        [Header("<b>Default opened ui")]
        [SerializeField] private CanvasNames defaultCanvas;
        [SerializeField] private PopUpNames defaultPopup;

        [Header("<b>Canvas collections")]
        [SerializeField] private List<CanvasIdentity> allCanvas = new List<CanvasIdentity>();
        [SerializeField] private List<PopupIdentity> allPopup = new List<PopupIdentity>();

        [Header("<b>Transition image values")]
        [SerializeField] private Image transitionImage;

        private float imageFillAmt = 0;
        private CanvasNames openedCanvas;
        private WaitForSeconds pointOne = new WaitForSeconds(0.1f);

        private void Awake()
        {
            instance = this;
            openedCanvas = defaultCanvas + 3;
        }

        private void Start()
        {
            StartCoroutine(nameof(OpenDefaultUi));
        }

        #region ACTION-HANDLERS
        private void OnEnable()
        {
            ActionHandler.OpenCanvasAction += OpenCanvas;
            ActionHandler.CloseCavnvasAction += CloseCanvas;
            ActionHandler.OpenCanvasWithTransitionAction += OpenCanvasWithTransition;

            ActionHandler.OpenPopup += OpenPopup;
            ActionHandler.ClosePopup += ClosePopup;

            ActionHandler.ChangeSceneWithTransitionAction += ChangeScene;
        }

        private void OnDisable()
        {
            ActionHandler.OpenCanvasAction -= OpenCanvas;
            ActionHandler.CloseCavnvasAction -= CloseCanvas;
            ActionHandler.OpenCanvasWithTransitionAction -= OpenCanvasWithTransition;

            ActionHandler.OpenPopup -= OpenPopup;
            ActionHandler.ClosePopup -= ClosePopup;
            
            ActionHandler.ChangeSceneWithTransitionAction -= ChangeScene;
        }
        #endregion

        private IEnumerator OpenDefaultUi()
        {
            transitionImage.fillAmount = 1;
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(nameof(OpenSceneWithTransition));
            OpenCanvas(defaultCanvas);
            OpenPopup(defaultPopup);
        }

        #region CANVAS-HANDLERS
        private void OpenCanvas(CanvasNames m_desireCanvas)
        {
            if (m_desireCanvas == openedCanvas) return;

            foreach (CanvasIdentity canvas in allCanvas)
            {
                if (canvas.GetCanvasName() == m_desireCanvas)
                {
                    canvas.EnableCanvas();
                    openedCanvas = m_desireCanvas;
                }
                else
                {
                    canvas.DisableCanvas();
                }
            }
        }


        private void OpenCanvasWithTransition(CanvasNames m_desireCanvas)
        {
            if (m_desireCanvas == openedCanvas) return;
            StartCoroutine(nameof(TransitionImageAnimation), m_desireCanvas);
        }

        #region Transition-animation
        IEnumerator TransitionImageAnimation(CanvasNames m_desireCanvas)
        {
            imageFillAmt = 0;
            transitionImage.fillOrigin = 0;
            transitionImage.raycastTarget = true;

            // fill the image
            while (imageFillAmt < 1)
            {
                imageFillAmt += Time.deltaTime * 4;
                transitionImage.fillAmount = imageFillAmt;
                yield return null;
            }

            // change of canvas
            foreach (CanvasIdentity canvas in allCanvas)
            {
                if (canvas.GetCanvasName() == m_desireCanvas)
                {
                    canvas.EnableCanvas();
                    openedCanvas = m_desireCanvas;

                    UI.ToolKit.ActionHandler.CanvasChanged?.Invoke(m_desireCanvas);
                }
                else
                {
                    canvas.DisableCanvas();
                }
            }

            // change the direction
            yield return pointOne;
            transitionImage.fillOrigin = 1;

            // un-fill the image
            while (imageFillAmt > 0)
            {
                imageFillAmt -= Time.deltaTime * 4;
                transitionImage.fillAmount = imageFillAmt;
                yield return null;
            }
            transitionImage.raycastTarget = false;
        }
        #endregion

        private void CloseCanvas(CanvasNames m_desireCanvas)
        {
            foreach (CanvasIdentity canvas in allCanvas)
            {
                if (canvas.GetCanvasName() == m_desireCanvas)
                {
                    canvas.DisableCanvas();
                }
            }
        }

        public void CloseAllCanvas()
        {
            foreach (CanvasIdentity canvas in allCanvas)
            {
                canvas.DisableCanvas();
            }
        }

        public void AddCanvas(CanvasIdentity m_canvasToAdd)
        {
            allCanvas.Add(m_canvasToAdd);
        }
        #endregion

        #region POPUP-HANDLER
        private void OpenPopup(PopUpNames m_desirePopup)
        {
            foreach (PopupIdentity popup in allPopup)
            {
                if (popup.GetPopupName() == m_desirePopup)
                {
                    popup.EnablePopup();
                }
                else
                {
                    popup.DisablePopup();
                }
            }
        }

        private void ClosePopup(PopUpNames m_desirePopup)
        {
            foreach (PopupIdentity popup in allPopup)
            {
                if (popup.GetPopupName() == m_desirePopup)
                {
                    popup.DisablePopup();
                }
            }
        }

        public void CloseAllPopup()
        {
            foreach (PopupIdentity popup in allPopup)
            {
                popup.DisablePopup();
            }
        }
        public void AddPopup(PopupIdentity m_popupToAdd)
        {
            allPopup.Add(m_popupToAdd);
        }
        #endregion


        private void ChangeScene(string sceneName)
        {
            StartCoroutine(nameof(ChangeSceneWithTransition), sceneName);
        }
        IEnumerator ChangeSceneWithTransition(string m_sceneName)
        {
            imageFillAmt = 0;
            transitionImage.fillOrigin = 0;
            transitionImage.raycastTarget = true;

            // fill the image
            while (imageFillAmt < 1)
            {
                imageFillAmt += Time.deltaTime * 4;
                transitionImage.fillAmount = imageFillAmt;
                yield return null;
            }

           SceneManager.LoadScene(m_sceneName);
        }

        IEnumerator OpenSceneWithTransition()
        {
            float m_imageFillAmt = 1;
            transitionImage.raycastTarget = true;
            transitionImage.fillAmount = 1;
            transitionImage.fillOrigin = 1;

            // un-fill the image
            while (m_imageFillAmt > 0)
            {
                m_imageFillAmt -= Time.deltaTime * 4;
                transitionImage.fillAmount = m_imageFillAmt;
                yield return null;
            }
            transitionImage.raycastTarget = false;
        }
    }
}