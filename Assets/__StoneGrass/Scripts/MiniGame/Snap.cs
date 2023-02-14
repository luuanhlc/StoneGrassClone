using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Snap : MonoBehaviour
{

    private Vector3 gridSize = new Vector3(2, 0, 2);

    GridMap _grdMap;
   
    public Vector3 _originPos;
    public Vector3 _offsetPos;
    public Vector3 mt;
    public static Snap Isn;
    int i, j;

    public Vector2 matrixPos;

    Vector3 snapPos;

    [SerializeField] LayerMask gridMask;
    #region singleton
    private void Awake()
    {
        Isn = this;
    }
    #endregion
    private Shape _shape;
    Pos pos = new Pos();
    private void Start()
    {
        _shape = GetComponentInParent<Shape>();
        _grdMap = GridMap.Isn;
        _offsetPos = this.gameObject.transform.localPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, -5f, transform.position.z));
    }

    private bool _hit = false;

    private void check()
    {
        var postion = new Vector3(
               Mathf.RoundToInt(this.transform.position.x / this.gridSize.x) * this.gridSize.x,
               0,
               Mathf.RoundToInt(this.transform.position.z / this.gridSize.z) * this.gridSize.z
               );
        mt = postion;
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(postion.x, 1f, postion.z), Vector3.down, out hit, 2f, gridMask))
        {
            _hit = true;

            string name = hit.collider.name;
            i = int.Parse(name[4].ToString());
            j = int.Parse(name[5].ToString());
            
            if(name.Length == 7)
                j = int.Parse(name[5].ToString() + name[6].ToString());
        }
        else
        {
            _hit = false;
        }
    }

    #region CheckTile
    Vector2 minPos;
    public List<Vector2> skipPos = new List<Vector2>();
    bool done;
    public void Find(Vector2 canplaceShape)
    {
        float min;
        float dis;
        minPos = matrixPos;
        min = 30;
        for(int i = (int)matrixPos.x; i <= canplaceShape.x + matrixPos.x; i++)
        {
            for(int j = (int)matrixPos.y; j <= canplaceShape.y + matrixPos.y; j++)
            {
                dis = Vector3.Distance(this.gameObject.transform.position, GridMap.Isn.squareGrid[i, j].transform.position);

                if (skipPos.Count > 0)
                {
                    for (int k = 0; k < skipPos.Count; k++)
                    {
                        if (i == skipPos[k].x && j == skipPos[k].y)
                        {
                            //min = 30;
                        }
                        else
                        {
                            if (dis <= min && !_grdMap._set[i, j])
                            {
                                minPos = new Vector2(i, j);
                                min = dis;
                            }
                        }
                    }
                }
                else
                {
                    if (dis <= min && !_grdMap._set[i, j])
                    {
                        minPos = new Vector2(i, j);
                        min = dis;
                    }
                }

            }
        }
        pos.i = (int)minPos.x;
        pos.j = (int)minPos.y;
        _shape._pos[0] = pos;
        snapPos = new Vector3(
            Mathf.RoundToInt(_grdMap.squareGrid[pos.i, pos.j].transform.position.x / this.gridSize.x) * this.gridSize.x,
            _grdMap._frispos.position.y + .04f,
            Mathf.RoundToInt(_grdMap.squareGrid[pos.i, pos.j].transform.position.z / this.gridSize.z) * this.gridSize.z
            );
        _shape.center = minPos;
        _shape.CheckLessTile(1);
    }

    public void _reset()
    {
        transform.localPosition = _offsetPos;
    }

    public void notCenterCheck(Vector2 centerPos, int i)
    {
        if(_grdMap._set[(int)centerPos.x - (int)_shape._tileBlock[0].GetComponent<Snap>().matrixPos.x + (int)matrixPos.x, (int)centerPos.y - (int)_shape._tileBlock[0].GetComponent<Snap>().matrixPos.y + (int)matrixPos.y])
        {
            
            _shape._tileBlock[0].GetComponent<Snap>().skipPos.Add(centerPos);
            _shape._can = false;
            return;
        }
        _shape._can = true;
        pos.i = (int)centerPos.x - (int)_shape._tileBlock[0].GetComponent<Snap>().matrixPos.x + (int)matrixPos.x;
        pos.j = (int)centerPos.y - (int)_shape._tileBlock[0].GetComponent<Snap>().matrixPos.y + (int)matrixPos.y;
        
        snapPos = new Vector3(
            Mathf.RoundToInt(_grdMap.squareGrid[pos.i, pos.j].transform.position.x / this.gridSize.x) * this.gridSize.x,
            _grdMap._frispos.position.y + .04f,
            Mathf.RoundToInt(_grdMap.squareGrid[pos.i, pos.j].transform.position.z / this.gridSize.z) * this.gridSize.z
            );
        
        _shape._pos[i] = pos;
        GetComponentInParent<Shape>().CheckLessTile(i + 1);
    }
    public bool _checkTile()
    {
        var postion = new Vector3(
                  Mathf.RoundToInt(this.transform.position.x / this.gridSize.x) * this.gridSize.x,
                  transform.position.y,
                  Mathf.RoundToInt(this.transform.position.z / this.gridSize.z) * this.gridSize.z
                  );
        check();
        if (_hit)
        {
            if (_grdMap._set[i, j])
                return false;
            else if (postion.x >= _grdMap.minX && postion.x <= _grdMap.maxX && postion.z >= _grdMap.minZ && postion.z <= _grdMap.maxZ)
                return true;
            else
                return false;
        }
        else
            return false;
    }
    #endregion

    public void SnaptoGrid2()
    {
        /*var postion = new Vector3(
            Mathf.RoundToInt(this.transform.position.x / this.gridSize.x) * this.gridSize.x,
            _grdMap._frispos.position.y + .04f,
            Mathf.RoundToInt(this.transform.position.z / this.gridSize.z) * this.gridSize.z
            );*/
        transform.position = snapPos;
        _grdMap._set[pos.i, pos.j] = true;
        _grdMap.squareGridComplete[pos.i, pos.j] = this.gameObject;
    }
}
