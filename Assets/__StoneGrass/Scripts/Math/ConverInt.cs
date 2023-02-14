using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConverInt : MonoBehaviour
{
    public static string Sign(int i)
    {
        if (i >= 1000000000)
            return ((int)i / 1000000000).ToString() + "B";
        if (i >= 1000000)
            return ((int)i / 1000000).ToString() + "M";
        if (i >= 100000)
            return ((int)i / 1000).ToString() + "K";
        else
            return i.ToString();
    }
}
