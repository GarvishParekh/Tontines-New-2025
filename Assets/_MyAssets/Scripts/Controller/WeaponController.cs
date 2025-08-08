using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Animator weaponAnime;
    [SerializeField] private Transform corsshairImage;

    [SerializeField] private WeaponState weaponState;

    [SerializeField] private float weaponCooldown = 0.20f;
    [SerializeField] private float crossHairUpdatedSize = 1;

    private float timer = 0;
    private float crossHairCurrentSize = 1;

    private void Awake()
    {
        weaponState = WeaponState.IDLE;
    }

    private void Update()
    {
        UpdateCrossHairSize();
    }

    public void B_Shoot()
    {
        if (weaponState == WeaponState.IDLE) StartCoroutine(nameof(ShootingSequence));
    }

    private IEnumerator ShootingSequence()
    {
        crossHairUpdatedSize += 0.5f;
        crossHairUpdatedSize = Mathf.Clamp (crossHairUpdatedSize, 1f, 2f);

        weaponState = WeaponState.SHOOTING;
        timer = 0;
        weaponAnime.SetTrigger("isShooting");
        while (timer <= weaponCooldown)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        weaponState = WeaponState.IDLE;
    }

    private void UpdateCrossHairSize()
    {
        crossHairUpdatedSize = Mathf.MoveTowards(crossHairUpdatedSize, 1, 0.6f * Time.deltaTime);
        corsshairImage.localScale = Vector3.one * crossHairUpdatedSize;
    }

    public void B_WeaponReload()
    {
        if (weaponState == WeaponState.IDLE) StartCoroutine(nameof(ReloadingSequence));
    }

    private IEnumerator ReloadingSequence()
    {
        weaponAnime.SetTrigger("isReloading");
        weaponState = WeaponState.RELOADING;
        yield return new WaitForSeconds(1.40f);
        weaponState = WeaponState.IDLE;
    }
}

public enum WeaponState
{
    IDLE,
    SHOOTING,
    RELOADING
}
