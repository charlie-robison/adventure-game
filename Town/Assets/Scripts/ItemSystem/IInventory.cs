using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory
{
    void fillSlots(Dictionary<string, int> items);
    void presentSelectedItemInfo(Dictionary<string, int> items);
}
