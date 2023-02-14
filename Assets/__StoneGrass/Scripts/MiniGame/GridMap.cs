using UnityEngine;
using MoreMountains.Tools;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;

public class GridMap : MonoBehaviour
{
    #region Declare
    GameObject ui;
    UiController uiCtl;

    [HideInInspector] public int maxX;
    [HideInInspector] public int maxZ;

    [HideInInspector] public int minX;
    [HideInInspector] public int minZ;

    public int Rows;
    public int Cols;

    private float scale = 1.5f;

    public float _offsetDis = .5f;

    public GameObject _perfab;

    public GameObject[,] squareGrid;
    public GameObject[,] squareGridComplete;
    public bool[,] _set;

    public Transform _frispos;

    public GameObject gridList;
    Vector3 empty;

    public int x;
    private MaterialPropertyBlock _propBlock;

    public static GridMap Isn;

    [SerializeField] private LayerMask tileMask;

    [SerializeField] public GameObject wallLeft;

    Shape shape;
    #endregion

    //public GameObject[,] changerColor;

    private void Awake()
    {
        Isn = this;

        _propBlock = new MaterialPropertyBlock();
        squareGridComplete = new GameObject[Rows, Cols];
        squareGrid = new GameObject[Rows, Cols];
        _set = new bool[Rows, Cols];

        minX = (int)_frispos.position.x;
        maxZ = (int)_frispos.position.z;

        maxX = (int)(_frispos.position.x + (Cols - 1) * scale + (Cols - 1) * _offsetDis);
        minZ = (int)(_frispos.position.z - (Rows - 1) * scale - (Rows - 1) * _offsetDis);
        _propBlock.SetColor("_Color", UiController.Ins._colorGridMapPlace);
    }

    private void Start()
    {
        ui = GameObject.Find("UiController");
        if(ui != null)
            uiCtl = ui.GetComponent<UiController>();

        shape = Shape.Isn;
        SpawmGrid();
        if(FeedBackTutorialList.Ins != null)
            FeedBackTutorialList.Ins._gridCubeFeedBacks = gridList.GetComponentsInChildren<CubeGrid>();
    }

    #region CreateGrid
    private void SpawmGrid()
    {
        empty = new Vector3(_frispos.position.x, _frispos.position.y, _frispos.position.z);
        
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                _set[i, j] = false;

                squareGrid[i, j] = Instantiate(_perfab, empty, Quaternion.Euler(0, 0, 0));
                squareGrid[i, j].name = "Grid" + i + "" + j;
                squareGrid[i, j].transform.GetChild(0).name = squareGrid[i, j].name;
                squareGrid[i, j].transform.parent = gridList.transform;
                if (j == Cols - 1)
                    empty = new Vector3(_frispos.position.x, _frispos.position.y, _frispos.position.z - ((i + 1) * scale + _offsetDis * (i + 1)));
                else
                    empty = new Vector3(_frispos.position.x + ((j + 1) * scale + _offsetDis * (j + 1)), _frispos.position.y, empty.z);
            }
        }
    }
    #endregion

    #region swapMearial

    private void Update()
    {
        playing();
    }
    public bool swaping;
    public LayerMask shapeMask;
    public void playing()
    {
        /*if (!swaping && !Complete)
            return;
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                RaycastHit hit;
                if (squareGrid[i, j] == null)
                    return;
                if (Physics.Raycast(squareGrid[i, j].transform.position, Vector3.up, out hit, 2f, tileMask))
                {
                    squareGrid[i, j].GetComponent<Renderer>().SetPropertyBlock(_propBlock);
                }
                else
                    squareGrid[i, j].GetComponent<Renderer>().SetPropertyBlock(null);
            }
        }*/
        //swaping = false;
    }
    #endregion
    public void resetCOlor()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                squareGrid[i, j].GetComponent<Renderer>().SetPropertyBlock(null);
            }
        }
    }
    public void ChangerColor(Pos[] pos)
    {
        
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                squareGrid[i, j].GetComponent<Renderer>().SetPropertyBlock(null);
                    for (int m = 0; m < pos.Length; m++)
                    {
                        if (pos[m] == null)
                            continue;
                        squareGrid[pos[m].i, pos[m].j].GetComponent<Renderer>().SetPropertyBlock(_propBlock);
                    }
            }
        }
    }

    public bool Complete = false;

    public void CheckMap()
    {
        for(int i = 0; i < Rows; i++)
        {
            for(int j = 0; j < Cols; j++)
            {
                if (!_set[i, j])
                {
                    Complete = false;
                    return;
                }
                else
                    Complete = true;
                if (!Complete)
                    return;
            }
        }
        Shape._isBuilding = false;

        MMSoundManagerSoundPlayEvent.Trigger(MMSoundManagerMySeft.Ins.completeSound, MMSoundManager.MMSoundManagerTracks.Sfx, Camera.main.gameObject.transform.position);
        StartCoroutine(wave());
        BuildBridgerPopup.Ins._ani.SetBool("_out", false);
        wallLeft.SetActive(false);
        Destroy(gridList);
        uiCtl.CompleteBuild();
    }

    public void resetMiniGame()
    {
        if (Complete)
            return;
        //SpawmGrid();
        squareGridComplete = new GameObject[Rows, Cols];
        _set = new bool[Rows, Cols];
        resetCOlor();
        foreach (GameObject brick in GameObject.FindGameObjectsWithTag("Block"))
        {
            brick.transform.DOKill();

            if (brick.GetComponent<Shape>()._Done)
            {
                brick.GetComponent<Shape>()._resetOffset();
            }
        }
    }
    Vector3 punchForce = new Vector3(1.1f, 1.1f, 1.1f);
    IEnumerator wave()
    {
        int rong = Cols > Rows ? Rows : Cols;
        int dai = rong == Rows ? Cols : Rows;
        /*Debug.Log(Cols + " = " + Rows);
        Debug.Log(rong);
        Debug.Log(dai);*/
        for(int i = 0; i < Rows; i++)
        {
            yield return Yielders.Get(.05f);
            for(int j = 0; j < Cols; j++)
            {
                squareGridComplete[i, j].transform.DOJump(squareGridComplete[i, j].transform.position, 1, 1, .2f);
                squareGridComplete[i, j].transform.DOPunchScale(punchForce, .2f, 1);
                Destroy(squareGridComplete[i, j].GetComponent<Snap>());
                Destroy(squareGridComplete[i, j].GetComponent<Rigidbody>());
                Destroy(squareGridComplete[i, j].GetComponent<BoxCollider>());
            }
        }
        Destroy(this.gameObject.GetComponent<GridMap>());
    }
    
}

