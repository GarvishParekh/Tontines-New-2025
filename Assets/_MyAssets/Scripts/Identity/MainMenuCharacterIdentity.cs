using UnityEngine;

public class MainMenuCharacterIdentity : MonoBehaviour
{
    [Tooltip ("1: Left | 2: Right")]
    [SerializeField] private int characterIndex;
    private void OnMouseDown()
    {
        ActionHandler.MainMenuCharacterSelected?.Invoke(characterIndex);
    }
}
