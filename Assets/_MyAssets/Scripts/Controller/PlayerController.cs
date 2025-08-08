using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    private void Awake()
    {
        playerData.playerHealth = 1;
        ActionHandler.PlayerGothit?.Invoke(playerData.playerHealth);
    }

    public void B_OnPlayerGotHit(float m_healthToReduce)
    {
        if (playerData.playerHealth <= 0) playerData.playerHealth = 1;
        else playerData.playerHealth -= m_healthToReduce;

        ActionHandler.PlayerGothit?.Invoke(playerData.playerHealth);
    }
}
