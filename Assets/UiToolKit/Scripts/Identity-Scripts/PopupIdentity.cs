using UI.ToolKit;
using UnityEngine;

public class PopupIdentity : MonoBehaviour
{
    UiManager uiManager;
    CanvasGroup canvasGroup;
    ICanvasAnimation customAnimation;

    [SerializeField] private PopUpNames myPopupName;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        customAnimation = GetComponentInChildren<ICanvasAnimation>();
    }

    private void Start()
    {
        uiManager = UiManager.instance;
        uiManager.AddPopup(this);
    }

    public PopUpNames GetPopupName()
    {
        return myPopupName;
    }

    public void EnablePopup()
    {
        if (customAnimation != null)
        {
            customAnimation.EnableAnimation();
        }

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void DisablePopup()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
