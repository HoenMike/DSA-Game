/* Name: #20
 Mai Nguyen Hoang - ITITIU21208
 Purpose: A vampire survivors clone that implements DSA.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimation : MonoBehaviour
{
    //* GameObject *//
    public Text text;

    //* Variables *//
    public float minScale = 0.8f;
    public float maxScale = 1.2f;
    public float speed = 2f;
    public float offset = 0f;

    //* Unity's Functions *//
    // Update is called once per frame
    void Update()
    {
        // Calculate the scale factor based on a sine wave function
        float scaleFactor = Mathf.Lerp(minScale, maxScale, (Mathf.Sin(Time.time * speed + offset) + 1f) / 2f);

        // Apply the scale factor to the text
        text.transform.localScale = Vector3.one * scaleFactor;
    }
}
