/* Name: #20
 Mai Nguyen Hoang - ITITIU21208
 Purpose: A vampire survivors clone that implements DSA.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//* Store character special trait *//
public class Character : MonoBehaviour
{
    //* Move speed *//
    public static float Speed
    {
        get { return GameManager.instance.playerId == 0 ? 1.1f : 1f; }
    }
    //* Weapon speed *//
    public static float WeaponSpeed
    {
        get { return GameManager.instance.playerId == 1 ? 1.1f : 1f; }
    }
    //* Weapon rate (Ex: +X% attackSpeed) *//
    public static float WeaponRate
    {
        get { return GameManager.instance.playerId == 1 ? 0.9f : 1f; }
    }
    //* Weapon damage *//
    public static float Damage
    {
        get { return GameManager.instance.playerId == 2 ? 1.2f : 1f; }
    }
    //* Number of weapon *//
    public static int Count
    {
        get { return GameManager.instance.playerId == 3 ? 1 : 0; }
    }
}
