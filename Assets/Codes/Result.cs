/* Name: #20
 Mai Nguyen Hoang - ITITIU21208
 Purpose: A vampire survivors clone that implements DSA.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//* Result class to handle result screen *//
//* Called by GameManager *//
public class Result : MonoBehaviour
{
    //* GameObject *//
    public GameObject[] titles;

    //* Custom Functions *//
    public void Lose()
    {
        titles[0].SetActive(true);
    }
    public void Win()
    {
        titles[1].SetActive(true);
    }

}
