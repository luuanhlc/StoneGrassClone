using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PoolingObject : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject perfab;
        public int size;
        public int vaule;
    }
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public List<Pool> pools;

    #region Singleton
    public static PoolingObject Isn;

    private void Awake()
    {
        Isn = this;
    }
    #endregion

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i < pool.size; i++)
            {
                //assetBundle
                /*pool.perfab.InstantiateAsync(this.gameObject.transform).Completed += op =>
                {
                    //obj.GetComponentInChildren<Renderer>().material.color = pool.basecolor;
                    op.Result.GetComponent<inforProduct>().vaule = pool.vaule;
                    op.Result.SetActive(false);
                    op.Result.GetComponent<inforProduct>().poolTag = pool.tag;
                    objectPool.Enqueue(op.Result);
                    pool.perfab.ReleaseAsset();
                };*/


                //nomarl perfab
                GameObject op = Instantiate(pool.perfab);
                op.transform.parent = this.transform;
                op.GetComponent<inforProduct>().vaule = pool.vaule;
                op.SetActive(false);
                objectPool.Enqueue(op);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public void spawmpool(string tag, Transform pos, Quaternion rotation, Transform sawPos)
    {
        if (!poolDictionary.ContainsKey(tag))
            return;
        infor.Ins.countProduct++;
        GameObject spawObj = poolDictionary[tag].Dequeue();
        spawObj.SetActive(true);
        spawObj.transform.DOScale(new Vector3( .42f, 0.4199999f, 0.5299999f), .2f);
        spawObj.transform.position = new Vector3(pos.transform.position.x, pos.transform.position.y + 0.3f, pos.transform.position.z);
        spawObj.GetComponent<productMove>().Nay(sawPos.position);
    }
}
