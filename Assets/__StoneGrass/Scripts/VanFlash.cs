using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

    public class VanFlash : MonoBehaviour
    {
    public static VanFlash Ins;
    public List<GameObject> bounce = new List<GameObject>();
    public List<GameObject> xa = new List<GameObject>();
    private void Awake()
    {
        Ins = this;
    }
    private void Start()
    {
    
        changerTranform(PlayerDataManager.GetStorage());    
    }
    public void changerTranform(int h)
    {
        for(int i = 0; i < bounce.Count; i++)
        {
            bounce[i].transform.localScale = new Vector3(bounce[i].transform.localScale.x, h * 0.2f, bounce[i].transform.localScale.z);
            bounce[i].transform.localPosition = new Vector3(bounce[i].transform.localPosition.x, h * .1f + .2f, bounce[i].transform.localPosition.z);
            xa[i].transform.localPosition = new Vector3(xa[i].transform.localPosition.x, bounce[0].transform.localPosition.y + (bounce[0].transform.localScale.y / 2), xa[i].transform.localPosition.z);
        }

       
    }

    public void SetActive(bool active)
    {
        for(int i = 0; i < bounce.Count; i++)
        {
            bounce[i].gameObject.SetActive(active);
            xa[i].gameObject.SetActive(active);
        }
    }
}
