using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class moneyFly : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void RunAnim()
    {
        this.gameObject.transform.DOLocalMove(new Vector3(this.gameObject.transform.localPosition.x, this.gameObject.transform.localPosition.y + Random.Range(90, 150), this.gameObject.transform.localPosition.z), 1f, false).SetEase(Ease.OutCubic);
        text.DOFade(0, .5f).SetDelay(.5f);
    }

    public void Reset()
    {
        PlayerController.Ins.iconmoney.Add(this.gameObject);
        this.gameObject.SetActive(false);
    }
}
