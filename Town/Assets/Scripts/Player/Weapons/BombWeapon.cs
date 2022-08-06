using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWeapon : MonoBehaviour, IPlayerWeapon
{
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private float maxDistance;

    [SerializeField]
    private float bombSpeed;

    private bool launchedBomb = false;

    void Start()
    {
        rb.useGravity = false;
    }

    public void weaponAttack()
    {
        transform.parent = null;
        launchedBomb = true;
        rb.useGravity = true;
        rb.AddForce(Vector3.up * bombSpeed);
    }

    void Update()
    {
        if (launchedBomb)
        {
            rb.velocity = Vector3.forward * bombSpeed;
        }
    }
}
