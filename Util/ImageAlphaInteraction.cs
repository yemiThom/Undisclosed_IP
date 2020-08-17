using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageAlphaInteraction : MonoBehaviour
{
    void Start()
    {
        //Any desired value between 0 and 1.
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.15f;
    }
}
