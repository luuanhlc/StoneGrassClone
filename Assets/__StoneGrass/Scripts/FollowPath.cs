using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FollowPath : MonoBehaviour
{
    public List<GameObject> pos = new List<GameObject>();

    #region Singleton
    public static FollowPath Ins;
    private void Awake()
    {
        Ins = this;
    }
    #endregion

    private void Start()
    {
        StartCoroutine(Action(2f));
    }

    IEnumerator Action(float time)
    {
        yield return Yielders.Get(time);
        anhxa();
    }

    public void anhxa()
    {
        Transform[] p = LobbyManager.Ins.patch.GetComponentsInChildren<Transform>();
        pos = new List<GameObject>();
        foreach (Transform i in p)
        {
            pos.Add(i.gameObject);
        }
        pos.Remove(pos[0]);
    }

    public int search()
    {
        int index = 0;
        float min = Vector3.Distance(this.gameObject.transform.position, pos[0].transform.position);
        for(int i = 1; i < pos.Count; i++)
        {
            float x = Vector3.Distance(this.gameObject.transform.position, pos[i].transform.position);
            if(x < min)
            {
                min = x;
                index = i;
            }
        }
        return index;
    }
    public bool go;
    public int currentPos;
    private void Update()
    {
        if (!go)
            return;
        moveToHome();
    }

    public float t = .075f;
    public void moveToHome()
    {
        PlayerController.Ins._isInputAble = false;
        if (transform.position != pos[currentPos].transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos[currentPos].transform.position, t);
            transform.LookAt(new Vector3(pos[currentPos].transform.position.x, this.transform.position.y, pos[currentPos].transform.position.z));
        }
        else
        {
            if (currentPos == 0)
            {
                go = false;
                
                PlayerController.Ins._isInputAble = true;
                PlayerController.Ins._isReturnHome = false;
                CinemachineBrain _cineBrain = Camera.main.gameObject.GetComponent<CinemachineBrain>();
                _cineBrain.m_BlendUpdateMethod = CinemachineBrain.BrainUpdateMethod.FixedUpdate;
                _cineBrain.m_UpdateMethod = CinemachineBrain.UpdateMethod.SmartUpdate;
                return;
            }
            currentPos = currentPos - 1;
        }
    }
}
