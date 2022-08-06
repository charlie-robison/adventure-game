using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GameControls controls;

    [SerializeField]
    private GameObject gunHolster;

    [SerializeField]
    private GameObject weapon = null;

    private IPlayerWeapon currentWeapon = null;
    private bool shotBullet = false;

    void Awake()
    {
        controls = new GameControls();
        controls.Gameplay.Shoot.performed += ctx => shotBullet = true;
        controls.Gameplay.Shoot.canceled += ctx => shotBullet = false;
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void attack()
    {
        if (currentWeapon != null && shotBullet && Time.timeScale == 1f)
        {
            currentWeapon.weaponAttack();
        }
    }

    void checkWeapon()
    {
        if (gunHolster.transform.childCount > 0)
        {
            if (gunHolster.transform.GetChild(0).gameObject.activeInHierarchy)
            {
                weapon = gunHolster.transform.GetChild(0).gameObject;
            }
        }
        else
        {
            weapon = null;
            currentWeapon = null;
        }

        if (weapon != null)
        {
            currentWeapon = weapon.GetComponent<IPlayerWeapon>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        attack();
        checkWeapon();
    }
}
