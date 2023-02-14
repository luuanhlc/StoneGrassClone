using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectToPlace : MonoBehaviour
{
    public bool Placed;
    public Vector3Int size { get; private set; }

    private Vector3[] Vertices;
    private void Start()
    {
        GetColliderVertexPosLocal();
        CaculerSizeInSell();
    }

    private void GetColliderVertexPosLocal()
    {
        BoxCollider b = gameObject.GetComponent<BoxCollider>();
        Vertices = new Vector3[4];
        Vertices[0] = b.center + new Vector3(-b.size.x / 2, -b.size.y / 2, -b.size.z / 2);
        Vertices[1] = b.center + new Vector3(b.size.x / 2, -b.size.y / 2, -b.size.z / 2);
        Vertices[2] = b.center + new Vector3(b.size.x / 2, -b.size.y, b.size.z);
        Vertices[3] = b.center + new Vector3(-b.size.x / 2, -b.size.y / 2, b.size.z / 2);
    }

    private void CaculerSizeInSell()
    {
        Vector3Int[] vertices = new Vector3Int[Vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 wordPos = transform.TransformPoint(Vertices[i]);
            vertices[i] = BuildingSystem.Ins.gridLayout.WorldToCell(wordPos);
        }

        size = new Vector3Int(Mathf.Abs((vertices[0] - vertices[1]).x),
                                            Mathf.Abs((vertices[0] - vertices[3]).y),
                                            1);
    }

    public Vector3 GetStartPos()
    {
        return transform.TransformPoint(Vertices[0]);
    }

    public virtual void Place()
    {
        DataBase.Ins.AddItem(this.gameObject);
        BuildingDrag drag = gameObject.GetComponent<BuildingDrag>();
        this.gameObject.GetComponent<BuildingData>().Apply();
        Destroy(drag);
        Placed = true;
    }

    public void Rotate()
    {
        transform.Rotate(0, 90, 0);
        size = new Vector3Int(size.y, size.x, size.z);
        Vector3[] vertices = new Vector3[Vertices.Length];
        for(int i = 0; i < Vertices.Length; i++)
        {
            vertices[i] = Vertices[(i + 1) % Vertices.Length];
        }
        Vertices = vertices;
    }

}
