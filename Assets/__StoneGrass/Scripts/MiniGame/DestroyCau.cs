using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using DG.Tweening;
    public class DestroyCau : MonoBehaviour
    {
    public float time;
    public Shape[] manh;
    private bool _destroyed;

    private void OnTriggerEnter(Collider other)
    {

        if (!other.CompareTag("Player") || _destroyed)
            return;

        StartCoroutine(destroy(time));
    }


    IEnumerator destroy(float time)
    {
        yield return Yielders.Get(time);
        for(int i = 0; i < manh.Length; i++)
        {
            manh[i].DestroyCau();
        }
        _destroyed = true;
        GridMap.Isn.wallLeft.SetActive(true);
    }
}
