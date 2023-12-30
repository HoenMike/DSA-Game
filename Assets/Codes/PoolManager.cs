/* Name: #20
 Mai Nguyen Hoang - ITITIU21208
 Purpose: A vampire survivors clone that implements DSA.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//* Pool manager to store and reuse objects using Array *//
public class PoolManager : MonoBehaviour
{
    //* GameObjects *//
    // prefabs is an special Unity's Object with predefined properties used to create instances of an Object
    public GameObject[] prefabs;
    List<GameObject>[] pools;

    //* Unity's Functions *//
    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    //* Custom Functions *//
    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select); // add to pool
        }

        return select;
    }
}
