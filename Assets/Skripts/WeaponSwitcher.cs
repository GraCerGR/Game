using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] private Transform weaponHolder;
    private int selectedWeaponIndex = 0;

    private void Start()
    {
        SelectWeapon();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeaponIndex = 0;
            SelectWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedWeaponIndex = 1;
            SelectWeapon();
        }
        // ƒобавь больше, если у теб€ больше оружи€
    }

    private void SelectWeapon()
    {
        for (int i = 0; i < weaponHolder.childCount; i++)
        {
            weaponHolder.GetChild(i).gameObject.SetActive(i == selectedWeaponIndex);
        }
    }
}