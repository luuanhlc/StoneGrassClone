using UnityEngine;
using DG.Tweening;
using System.Collections;
public class LoadScreen : MonoBehaviour
{
    public RectTransform leftClould;
    public RectTransform rightClould;

    public static LoadScreen Ins;

    Vector2 originPosLeft;
    Vector2 originPosRight;

    public float closeTime = .1f;
    public float openTime = .1f;

    public float waitTimeToOpen = .3f;

    private void Awake()
    {
        Ins = this;

        originPosLeft = leftClould.anchoredPosition;
        originPosRight = rightClould.anchoredPosition;
    }

    public void OpenGame()
    {
        leftClould.anchoredPosition = Vector2.zero;
        rightClould.anchoredPosition = Vector2.zero;

        StartCoroutine(open(openTime));
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Changer(waitTimeToOpen);
        }
    }
    public void Changer(float time)
    {
        leftClould.gameObject.SetActive(true);
        rightClould.gameObject.SetActive(true);

        leftClould.DOAnchorPos(Vector2.zero, closeTime).SetEase(Ease.Linear);
        rightClould.DOAnchorPos(Vector2.zero, closeTime).SetEase(Ease.Linear);

        StartCoroutine(open(time));
    }

    IEnumerator open(float time)
    {
        yield return Yielders.Get(time);
        leftClould.DOAnchorPos(originPosLeft, openTime).SetEase(Ease.Linear);
        rightClould.DOAnchorPos(originPosRight, openTime).SetEase(Ease.Linear).OnComplete(complete);
    }

    void complete()
    {
        leftClould.gameObject.SetActive(false);
        rightClould.gameObject.SetActive(false);
    }

}
