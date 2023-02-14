using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashingEffect : MonoBehaviour
{
    MeshRenderer meshRender;
    Color originColor;
    float flashTime = .15f;
    private Color red => Color.red;
    [SerializeField] private float loopspeed;

    private void Start()
    {
        meshRender = GetComponent<MeshRenderer>();
        originColor = meshRender.material.color;
    }

    public void Flashing()
    {
        meshRender.material.color = LerpRed();
    }

    public void Origin()
    {
        meshRender.material.color = originColor;
    }

    public Color LerpRed()
    {
        return Color.Lerp(originColor, red, Mathf.Sin(Time.time) - loopspeed);
    }
}
