using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private GameObject bomb;

    [SerializeField]
    private GameObject explosion;

    void explode()
    {
        Destroy(bomb);
        GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
        Destroy(newExplosion, 1f);
    }

    void OnTriggerEnter(Collider col)
    {
        explode();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > 4f)
        {
            explode();
        }
    }
}
