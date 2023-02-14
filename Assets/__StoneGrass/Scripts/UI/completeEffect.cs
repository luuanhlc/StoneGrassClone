using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;

public class completeEffect : MonoBehaviour
{
    public DOTweenAnimation dt;

    [SerializeField] private GameObject perfab;
    public GameObject panel;
    Vector3 originPos;
    Vector3 originScale;
    public GameObject uitop;
    private UIOutline _UIoutline;
    private void Start()
    {
        outline.transform.localScale = new Vector3(.6f, .6f, 1);
        originPos = this.gameObject.transform.position;
        originScale = this.gameObject.transform.localScale;
        _UIoutline = outline.GetComponent<UIOutline>();
    }

    public void Spawm()
    {
        int n = Random.Range(6, 12);
        if (perfab != null)
        {
            for (int i = 0; i < n; i++)
            {
                GameObject d = Instantiate(perfab, transform.position, Quaternion.identity);
                d.transform.parent = uitop.transform;
            }
        }
    }
    public void _Reset()
    {
        this.gameObject.transform.position = originPos;
        this.gameObject.transform.localScale = originScale;
        this.gameObject.SetActive(false);
    }
    public bool ran;
    public void RunAnim(int i)
    {
        if (ran)
            return;
        Spawm();
        this.gameObject.transform.DOLocalMove(new Vector3(53f + 145f * i, 8.9f, 0), 1, false ).SetDelay(1.2f).OnComplete(scale);
        this.gameObject.transform.DOScale(1, 1).SetDelay(1.3f);
        this.gameObject.transform.DOLocalRotate(new Vector3(0, 360, 0), 1, RotateMode.FastBeyond360);
        ran = true;
    }
    public GameObject outline;


    public void scale()
    {
        outline.transform.DOScale(0, .7f);
    }

}
