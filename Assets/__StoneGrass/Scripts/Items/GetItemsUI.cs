/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GetItemsUI : MonoBehaviour
{
    public Button _bttAd;
    public Button _bttPay;

    [SerializeField] private float time;
    private void Awake()
    {
        _bttPay.interactable = false;
    }

    void Start()
    {
        if (PlayerDataManager.GetDimond() >= 10)
            _bttPay.interactable = true;
        StartCoroutine(waitTime(time));
    }

    IEnumerator waitTime(float time) {
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
    }

    public void adClick()
    {
        PlayerController._namcham = true;
        this.gameObject.SetActive(false);
    }

    public void payClick()
    {
        GameManager.Instance.Profile.AddDimond(-10);
        this.gameObject.SetActive(false);
        if(Items.name == "NamCham")
        {
            PlayerController._namcham = true;
        }
        if(Items.name == "BigerSaw")
        {
            PlayerController._bigerSaw = true;
        }
        if(Items.name == "InfinityDame")
        {
            PlayerController._infinityDame = true;
        }
        if(Items.name == "InfinityStronger")
        {
            PlayerController._infinityStronge = true;
        }
    }
}
*/