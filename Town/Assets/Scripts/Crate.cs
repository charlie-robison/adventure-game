using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField]
    private GameObject[] possibleItems;

    [SerializeField]
    private int numberOfPossibleItems;

    public void spawnItems()
    {
        // Creates the number of items the crate has from a range of 0 to the number of possible items.
        int numberOfItems = Random.Range(0, numberOfPossibleItems);

        // Instantiates all the items the crate contains. All items are randomized and their positions are randomized
        // within the area of the crate.
        for (int i = 0; i < numberOfItems; i++)
        {
            GameObject newItem = Instantiate(possibleItems[Random.Range(0, possibleItems.Length)]);
            Vector3 itemPosition = new Vector3(transform.position.x + Random.Range(0, 2f), transform.position.y, transform.position.z + Random.Range(0, 2f));
            newItem.transform.position = itemPosition;
        }
    }
}
