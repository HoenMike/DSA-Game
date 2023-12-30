/* Name: #20
 Mai Nguyen Hoang - ITITIU21208
 Purpose: A vampire survivors clone that implements DSA.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//* Handle gear's property *//
public class Gear : MonoBehaviour
{
    // GameObjects
    public ItemData.ItemType type;
    // Variables
    public float rate;
    private const float INITIAL_WEAPON_SPEED = 150f;
    private const float INITIAL_WEAPON_RATE = .5f;

    //* Custom Functions *//
    public void Init(ItemData data)
    {
        // Basic Set
        name = "Gear" + data.itemId;
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector2.zero;

        // Property Set 
        type = data.itemType;
        rate = data.damages[0];
        ApplyGear();
    }
    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }
    void ApplyGear()
    {
        switch (type)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }
    void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach (Weapon weapon in weapons)
        {
            switch (weapon.id)
            {
                case 0:
                    float speed = INITIAL_WEAPON_SPEED * Character.WeaponSpeed;
                    weapon.speed = speed + (speed * rate);
                    break;
                default:
                    speed = INITIAL_WEAPON_RATE * Character.WeaponRate;
                    weapon.speed = speed * (1f - rate);
                    break;
            }
        }
    }
    void SpeedUp()
    {
        float speed = 3 * Character.Speed;
        GameManager.instance.player.speed = speed + speed * rate;
    }

}
