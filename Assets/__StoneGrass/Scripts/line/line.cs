/*using UnityEngine;

public class line : MonoBehaviour
{
    LineRenderer _line;
    
    public GameObject saw;

    public float _width = 1;

    public static line Isn;

    public GameObject lineDrawPrefabs;

    private GameObject lineDrawPrefab;

    private void Awake()
    {
        _line = gameObject.GetComponent<LineRenderer>();
        Isn = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _line.SetWidth(Mathf.Max(PlayerDataManager.GetNumberOfPongs() - 1.5f, 1f), Mathf.Max(PlayerDataManager.GetNumberOfPongs() - 1.5f, 1f));
        _line.positionCount = 1;
        _line.SetPosition( 0, saw.transform.position);
    }

    private float nexttime;

    public void SetWidth(float width)
    {
        Debug.Log(gameObject.name);
        _line.SetWidth(width, width);
    }

    public void newLine(float width)
    {
        lineDrawPrefab = GameObject.Instantiate(lineDrawPrefabs) as GameObject;
        _line = lineDrawPrefab.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (!PlayerController._isMove)
            return;
        if(nexttime < Time.time)
        {
            _line.positionCount += 1;
            _line.SetPosition(_line.positionCount - 1, saw.transform.position);
            nexttime = Time.time + .05f;
        }
    }
}
*/