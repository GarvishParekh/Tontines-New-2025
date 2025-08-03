using System.Data.SqlTypes;
using UI.ToolKit;
using UnityEngine;

public class CharacterSelectionUiController : MonoBehaviour
{
    [Header ("<b>Camera")]
    [SerializeField] private Camera characterSelectionCamera;
    [SerializeField] private Camera mainMenuCamera;
    private enum SelectedCharacter { NULL, LEFT, RIGHT }
    [SerializeField] private SelectedCharacter selectedCharacter;   
    
    [Header ("<b>User interface")]
    [SerializeField] private GameObject nextButton;

    [Header ("<b>Character selection animator")]
    [SerializeField] private Animator leftCharacter;
    [SerializeField] private Animator rightCharacter;

    [Header ("<b>Main menu components")]
    [Header ("<b>Main menu components")]
    [SerializeField] private GameObject[] charactersObject;
    [SerializeField] private GameObject mainMenuCharacterHolder;
    [SerializeField] private GameObject characterSelectionCharacterHolder;

    private void Awake()
    {
        Application.targetFrameRate = 60;   
    }

    private void OnEnable()
    {
        ActionHandler.MainMenuCharacterSelected += OnSelectCharacter;
        UI.ToolKit.ActionHandler.CanvasChanged += OnUiCanvasChange;
    }

    private void OnDisable()
    {
        ActionHandler.MainMenuCharacterSelected -= OnSelectCharacter;
        UI.ToolKit.ActionHandler.CanvasChanged -= OnUiCanvasChange;
    }

    public void B_OpenNameSelection()
    {
        UI.ToolKit.ActionHandler.OpenCanvasWithTransitionAction(CanvasNames.MAIN_MENU);
    }

    public void B_ResetScene()
    {
        UI.ToolKit.ActionHandler.ChangeSceneWithTransitionAction?.Invoke("MainMenuScene");
    }

    public void OnSelectCharacter(int m_selectedCharacter)
    {
        nextButton.SetActive(true);
        switch(m_selectedCharacter)
        {
            case 0:
                break;
            case 1:
                leftCharacter.SetBool("OnSelected", true);
                rightCharacter.SetBool("OnSelected", false);

                SwitchMainMenuCharacter(0);
                break;
            case 2:
                leftCharacter.SetBool("OnSelected", false);
                rightCharacter.SetBool("OnSelected", true);
                
                SwitchMainMenuCharacter(1);
                break;
        }
    }

    private void SwitchMainMenuCharacter(int m_index)
    {
        foreach (GameObject m_character in charactersObject) 
        {
            m_character.SetActive(false);   
        }
        charactersObject[m_index].SetActive(true);
    }

    private void OnUiCanvasChange(CanvasNames m_canvasName)
    {
        switch (m_canvasName)
        {
            case CanvasNames.MAIN_MENU:
                mainMenuCharacterHolder.SetActive(true);
                characterSelectionCharacterHolder.SetActive(false);

                mainMenuCamera.gameObject.SetActive(true);
                characterSelectionCamera.gameObject.SetActive(false);
                break;
        }
    }

   
}
