using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameplayUiController : MonoBehaviour
{

    [SerializeField] private Animator openingUI;

    [SerializeField] private PlayerData playerData;
    [SerializeField] private Image playerHealthBar;

    private void OnEnable()
    {
        ActionHandler.PlayerGothit += UpdateHealthUi;
    }

    private void OnDisable()
    {
        ActionHandler.PlayerGothit -= UpdateHealthUi;
    }

    private void Start()
    {
        StartCoroutine(nameof(StartTheGame));
    }

    private IEnumerator StartTheGame()
    {
        openingUI.SetBool("Active", true);
        yield return new WaitForSeconds(3);
        openingUI.SetBool("Active", false);
        yield return new WaitForSeconds(2);
        UI.ToolKit.ActionHandler.OpenCanvasAction(UI.ToolKit.CanvasNames.GAMEPLAY_CANVAS);
    }

    private void UpdateHealthUi(float m_healthToReduce)
    {
        playerHealthBar.fillAmount = playerData.playerHealth;
    }
}
