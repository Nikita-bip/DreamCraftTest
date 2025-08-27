using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponBase[] weapons;
    private int currentWeaponIndex = 0;

    void Start()
    {
        EquipWeapon(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) EquipWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) EquipWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) EquipWeapon(2);

        if (weapons.Length == 0) return;

        // ЛКМ для стрельбы
        if (Input.GetMouseButton(0))
            weapons[currentWeaponIndex].Shoot();
    }

    void EquipWeapon(int index)
    {
        if (index < 0 || index >= weapons.Length) return;

        for (int i = 0; i < weapons.Length; i++)
            weapons[i].gameObject.SetActive(i == index);

        currentWeaponIndex = index;
        Debug.Log("Выбрано оружие: " + weapons[index].weaponName);
    }
}
