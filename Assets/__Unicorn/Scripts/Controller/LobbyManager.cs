using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Cinemachine;
using PathCreation;
public class LobbyManager : LevelManager
{
    //public GameObject cv;
    #region Declare
    public CinemachineVirtualCamera BuildBridgerCam;
    public GameObject patch;

    public Material planeMaterial;

    public MeshRenderer[] plane;
    public ParticleSystem weather;

    public float Vignette = 1f;
    [Range(0, 1)]
    public float Transparency = 1f;

    public Texture2D frost;
    public Texture2D normal;

    public PoolingObject _pool;

    public Transform _et;

    #endregion
    #region Singleton
    public static LobbyManager Ins;
    public float totalproduct;
    public GameObject fillcau;
    public BuildBridgerTut _tut;
    protected override void Awake()
    {
        Ins = this;
        _et = Camera.main.gameObject.transform;

        UiController.Ins.UiInGame.totalProduct = this.totalproduct;
        FollowPath.Ins.anhxa();
        /*if (cv != null)
        {
            cv.SetActive(false);
        }*/
        base.Awake();
        if (weather != null)
        {
            weather.Play();
            weather.gameObject.transform.parent = _et.transform;
        }
        GameManager.Instance.changeMaterial(planeMaterial);
        for (int i = 0; i < plane.Length; i++)
        {
            if (plane[i].gameObject.CompareTag("tron"))
                plane[i].material = planeMaterial;
            else
            {
                var mat = plane[i].materials;
                mat[1] = planeMaterial;
                plane[i].materials = mat;
            }
        }
        /*MobileFrost _mbF = Camera.main.GetComponent<MobileFrost>();
        _mbF.frost = frost;
        _mbF.normal = normal;
        _mbF.Vignette = Vignette;
        _mbF.Transparency = Transparency;*/
    }
    #endregion
    public override void StartLevel()
    {

    }
    public override void EndLevel()
    {
        EndGame(LevelResult.Win);
    }

    public void OpenUpdate()
    {
        OpenUpdateUI();
    }


}
