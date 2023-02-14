using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{
    #region singleton
    public static BuildingSystem Ins;
    private void Awake()
    {
        Ins = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }
    #endregion

    public GridLayout gridLayout;

    private Grid grid;

    [SerializeField] private Tilemap tileMap;
    [SerializeField] private TileBase whiteTile;
    [SerializeField] private TileBase redTile;
    [SerializeField] private TileBase greenTile;

    [SerializeField] private Vector3 offset;
    public GameObject center;
    public bool _isDrag;

    [SerializeField] private GameObject isLand;

    public ObjectToPlace objectToPlace;

    public void Dat()
    {
        if (CanBePlace(objectToPlace))
        {

            Vector3Int start = gridLayout.WorldToCell(objectToPlace.GetStartPos());
            Start = start;
            objectToPlace.Place();
            TakeArea(start, objectToPlace.size);

            UiController.Ins.UiTop._isBuild = false;
            UiController.Ins.UiTop.setButton();
        }
        else
        {
            return;
        }
    }

    public void ChangLayer(int i)
    {
        switch (i)
        {
            case 0:
                tileMap.gameObject.layer = LayerMask.NameToLayer("Default");
                break;
            case 1:
                tileMap.gameObject.layer = LayerMask.NameToLayer("VisibleWall");
                break;
        }
    }

    public static Vector3 GetMousePostion()
    {
        if (Input.touchCount <= 0)
            return Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        if (Physics.Raycast(ray, out RaycastHit hit))
            return hit.point;
        else
            return Vector3.zero;
    }

    public Vector3 SnapCoordinateToGrid(Vector3 pos)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(pos);

        pos = grid.GetCellCenterWorld(cellPos);
        return pos;
    }


    public void InitializeWithObject(GameObject perfab)
    {
        if (GetMousePostion() == Vector3.zero)
            return;
        Vector3 pos = SnapCoordinateToGrid(new Vector3(GetMousePostion().x, GetMousePostion().y + 30f, GetMousePostion().z));
        GameObject obj = Instantiate(perfab, pos, Quaternion.identity);
        obj.transform.parent = isLand.transform;
        obj.name = perfab.name;
        objectToPlace = obj.GetComponentInChildren<ObjectToPlace>();
        UiController.Ins.UiTop._isBuild = true;
        UiController.Ins.UiTop.setButton();
        
    }
    private static TileBase[] GetTileBlocks(BoundsInt area, Tilemap tileMap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;
        foreach (var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tileMap.GetTile(pos);
            counter++;
        }

        return array;
    }
    public bool CanBePlace(ObjectToPlace otp)
    {
        BoundsInt area = new BoundsInt();
        area.position = gridLayout.WorldToCell(otp.GetStartPos());
        area.size = otp.size + new Vector3Int(1, 1, 1);

        TileBase[] baseArray = GetTileBlocks(area, tileMap);

        foreach(var v in baseArray)
        {
            if( v != whiteTile)
            {
                return false;
            }
        }
        return true;
    }

    public Vector3Int Start;
    public void TakeArea(Vector3Int start, Vector3Int size)
    {
        tileMap.BoxFill(start, greenTile, start.x, start.y, start.x + size.x, start.y + size.y);
        Debug.Log(Start);
        Debug.Log(start);
    }
}
