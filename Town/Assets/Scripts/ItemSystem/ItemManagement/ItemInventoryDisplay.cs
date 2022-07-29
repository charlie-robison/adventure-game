using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventoryDisplay : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0f, 40f * Time.deltaTime, 0f);
    }
}
