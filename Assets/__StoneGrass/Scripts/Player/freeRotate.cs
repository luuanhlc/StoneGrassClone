using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freeRotate : MonoBehaviour
{
    public static freeRotate Ins;
    
    
    public float orsawScale;

    public Vector3 center;

    private void Awake()
    {
        Ins = this;
        orsawScale = transform.localScale.x;
    }

    private void Start()
    {
        infor.Ins.sawSpeed = PlayerDataManager.GetSawSpeed();
    }

    void Update()
    {
        if (GameManager.Instance._isIsland || Time.frameCount % 3 != 0)
            return;
        transform.RotateAround(transform.position, center, Time.deltaTime * infor.Ins.sawSpeed);
    }

    public void ChangerSize(float sawScale)
    {
        transform.localScale = new Vector3(sawScale, sawScale, transform.localScale.z);
    }
}
