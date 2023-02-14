using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorManager : MonoBehaviour
{
    public List<Color> colors = new List<Color>();

    public static ColorManager Ins;

    private void Awake()
    {
        Ins = this;
    }
}
