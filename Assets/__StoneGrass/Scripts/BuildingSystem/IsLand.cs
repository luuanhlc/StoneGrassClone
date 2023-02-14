using UnityEngine;
using TMPro;
 public class IsLand : MonoBehaviour
{
    Animator _ani;
    [SerializeField] private TextMeshProUGUI Storage;
    [SerializeField] private TextMeshProUGUI Speed;

    public static IsLand Ins;
    private void Awake()
    {
        Ins = this;
        _ani = GetComponent<Animator>();
        setText();
    }

    private void OnEnable()
    {
        _ani.SetTrigger("In");
    }

    public void setText()
    {
        Storage.text = PlayerDataManager.GetSorageIsLand().ToString();
        Speed.text = PlayerDataManager.GetSpeedMineIsLand().ToString() + "/min";
    }
}

