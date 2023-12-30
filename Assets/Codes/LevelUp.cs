/* Name: #20
 Mai Nguyen Hoang - ITITIU21208
 Purpose: A vampire survivors clone that implements DSA.
*/
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//* Handle Level up popup screen *//
public class LevelUp : MonoBehaviour
{
    //* GameObjects *//
    RectTransform rect;
    Item[] items;

    //* Unity's Functions *//
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    //* Custom Functions *//
    public void Show()
    {
        Next();
        rect.localScale = Vector2.one;
        GameManager.instance.Stop();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
        AudioManager.instance.EffectBgm(true);
    }
    public void Hide()
    {
        rect.localScale = Vector2.zero;
        GameManager.instance.Resume();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.instance.EffectBgm(false);
    }
    public void Select(int index)
    {
        items[index].OnClick();
    }
    void Next() // randomly choose 3 item to show
    {
        // make sure all item is inactive
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }


        // get 3 unique random number
        int[] random = new int[3];
        while (true)
        {
            random[0] = Random.Range(0, items.Length);
            random[1] = Random.Range(0, items.Length);
            random[2] = Random.Range(0, items.Length);

            if (random[0] != random[1] && random[0] != random[2] && random[1] != random[2])
                break;
        }
        for (int i = 0; i < random.Length; i++)
        {
            Item aRandomItem = items[random[i]];
            if (aRandomItem.level == aRandomItem.data.damages.Length)
            {
                items[4].gameObject.SetActive(true);
            }
            else
            {
                aRandomItem.gameObject.SetActive(true);
            }
        }
    }
}
