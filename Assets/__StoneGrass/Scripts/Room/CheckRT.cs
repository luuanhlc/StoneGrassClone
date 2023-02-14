using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CheckRT : MonoBehaviour
{
    public float phantram;

    public Texture2D noGrassTex;

    public Transform target;

    public bool _PlayerHere = false;

    private Color hideColor = new Color(1, 0, 1, 1);
    private Texture2D tex2d;
    private Vector3 screenPos;
    infor _infor;

    public Image mau;
         
    private void Start()
    {


        target = GameObject.Find("CheckPos").transform;
        _infor = infor.Ins;
    }
    public Texture2D tex;
    private void Update()
    {
        if (!_PlayerHere)
            return;

    }

    void setup()
    {

        float x;
        float y;

        mau.color = tex.GetPixel(20, 20);
    }

    void check()
    {
        setup();
        var color = tex2d.GetPixel((int)(Mathf.Abs(screenPos.x) * 1f / 0.75f), (int)(Mathf.Abs(screenPos.z) * 1f / 0.75f));
        mau.color = color;
        if (color != hideColor)
        {
        _infor.needcut = true;
        _infor.gn = this.gameObject.name.Substring(this.gameObject.name.Length - 1, 1);
    }
    else
        _infor.needcut = false;
    }
}
