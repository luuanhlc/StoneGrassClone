using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine.Tilemaps;
using System.IO;

public class DataBase : MonoBehaviour
{
    public static DataBase Ins { get; set; }
    public ItemDB db;

    public ItemDB defaulItems;
    string rootFolder;

    [SerializeField] private Tilemap tileMap;
    public TileBase greenTile;

    private void Awake()
    {
    #if UNITY_EDITOR
        rootFolder = Application.dataPath + "/_DataBase/data.xml";
    #elif UNITY_ANDROID || UNITY_IOS
        rootFolder = Application.persistentDataPath + "/data.xml";
    #endif
        Ins = this;
        LoadItem();
    }

    public void AddItem(GameObject obj)
    {
        Item item = new Item();
        item.PerfabId = obj.GetComponent<BuildingData>().perfabID;
        item.pos = obj.transform.position;
        item.rotation = new Vector3(0, obj.transform.rotation.y, 0);
        item.ItemsId = item.PerfabId + db.items.Count + ToString();
        item.size = obj.GetComponent<ObjectToPlace>().size;
        item.start = BuildingSystem.Ins.Start;
        obj.name = item.ItemsId;
        db.items.Add(item);
        SaveItem();
    }

    public void SaveItem()
    {
        XmlSerializer xml = new XmlSerializer(typeof(ItemDB));
        FileStream steam = new FileStream(rootFolder, FileMode.Create);
        xml.Serialize(steam, db);
        steam.Close();
    }
    ObjectToPlace obj;

    public GameObject island;
    public void LoadItem()
    {
        if (!File.Exists(rootFolder))
            return;
        XmlSerializer xml = new XmlSerializer(typeof(ItemDB));
    
        FileStream steam = new FileStream(rootFolder, FileMode.Open);
        db = xml.Deserialize(steam) as ItemDB;
        steam.Close();

        foreach(Item item in db.items)
        {
            GameObject go = Instantiate(PerfabDataBase.Ins.RequestPerfabs(item.PerfabId));
            go.transform.position = item.pos;
            go.transform.parent = island.transform;
            go.transform.rotation = Quaternion.Euler(item.rotation);
            TakeArea(item.start, item.size);
        }
    }

    public void TakeArea(Vector3Int start, Vector3Int size)
    {
        tileMap.BoxFill(start, greenTile, start.x, start.y, start.x + size.x, start.y + size.y);

    }


    public void DeleteItem(string itemId)
    {
        Item item = db.items.Where(p => p.ItemsId == itemId).First();
        db.items.Remove(item);
    }

}

[System.Serializable]
public class ItemDB
{
    public List<Item> items = new List<Item>();
}

[System.Serializable]
public class Item
{
    public string PerfabId;
    public string ItemsId;
    public Vector3 pos;
    public Vector3 rotation;
    public Vector3Int start;
    public Vector3Int size;
}
