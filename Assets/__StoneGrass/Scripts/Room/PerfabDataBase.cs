using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
    public class PerfabDataBase : MonoBehaviour
    {
    public static PerfabDataBase Ins { private set; get; }

    private void Awake()
    {
        Ins = this;
    }

    public List<PerfabItems> prefabs = new List<PerfabItems>();

    public GameObject RequestPerfabs(string perfabId)
    {
        PerfabItems perfabItem = prefabs.Where(p => p.perfabId == perfabId).First();
        return perfabItem.Perfab_GameObject;
    }
}

[System.Serializable]
public class PerfabItems
{
    public GameObject Perfab_GameObject;
    public string perfabId;
}
