using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBonusFocus : MonoBehaviour
{
    private Image parentImage;
    
    private void OnEnable()
    {
        if (NeedToWatchAds())
        {
            gameObject.SetActive(false);
        }
        else
        {
            parentImage = transform.parent.GetComponent<Image>();
        }
    }

    private void Update()
    {
        if (parentImage && !parentImage.isActiveAndEnabled)
        {
            gameObject.SetActive(false);
        }
    }

    private bool NeedToWatchAds() => false;
}
