using UnityEngine;
using MoonAntonio.UI;

public class BuildingDrag : MonoBehaviour
{
    Vector3 offset;
    private MaterialPropertyBlock _propBlock;
    private MaterialPropertyBlock _propCanPlace;

    public Color colorCanPlace;

    public GameObject body;

    public bool _isDrag;

    public static BuildingDrag Ins;

    Touch touch;
    Vector3 fristPos;
    private void Awake()
    {
        Ins = this;
        _propBlock = new MaterialPropertyBlock();
        _propBlock.SetColor("_Color", Color.red);

        _propCanPlace = new MaterialPropertyBlock();
        _propCanPlace.SetColor("_Color", colorCanPlace);
    }
    Vector3 begin;

    private void OnMouseDown()
    {
        if (this.gameObject.GetComponent<ObjectToPlace>().Placed)
            return;
        begin = Input.mousePosition;
        offset = transform.position - BuildingSystem.GetMousePostion();
        BuildingSystem.Ins._isDrag = true;
    }
    public float dis;
    private void OnMouseDrag()
    {
        dis = Vector3.Distance(begin, Input.mousePosition);
        if (dis <= 25f)
            return;
        if (!Interactivo.Ins._isOn)
        {
            _isDrag = true;
            Vector3 pos = BuildingSystem.GetMousePostion() + offset;
            transform.position = BuildingSystem.Ins.SnapCoordinateToGrid(pos);
            BuildingSystem.Ins.ChangLayer(0);
        }
    }

    private void Update()
    {
        if (BuildingSystem.Ins.CanBePlace(this.gameObject.GetComponent<ObjectToPlace>()))
        {
            body.gameObject.GetComponent<Renderer>().SetPropertyBlock(_propCanPlace);
            return;
        }
        else
            body.gameObject.GetComponent<Renderer>().SetPropertyBlock(_propBlock);
    }

    public void DatComplete()
    {
        body.gameObject.GetComponent<Renderer>().SetPropertyBlock(null);
    }
    private void OnMouseUp()
    {
        _isDrag = false;
        BuildingSystem.Ins.ChangLayer(1);
        BuildingSystem.Ins._isDrag = false;
    }
}
