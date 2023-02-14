using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class PathMap : MonoBehaviour
{
    private Image[] objs;
    int dotCount;
    public levelSelect _level;

    private void Awake()
    {
        dotCount = transform.childCount;
        objs = new Image[dotCount];
        for (int i = 0; i < dotCount; i++)
        {
            objs[i] = transform.GetChild(i).GetComponent<Image>();
        }
    }
    void Update()
    {
        //StartCoroutine(UnLock());
    }

    public IEnumerator UnLock()
    {
        Debug.Log("VCk");
        for(int i = 0; i < dotCount; i++)
        {
            objs[i].DOFade(1, .1f);
            objs[i].DOColor(Color.red, .3f);

            yield return Yielders.Get(.2f);
        }
        _level.Load();
    }

    public void Unlocked()
    {
        for (int i = 0; i < dotCount; i++)
        {
            objs[i].DOFade(1, 0.01f);
            objs[i].color = Color.red;
        }
        _level.Load();
    }
}
