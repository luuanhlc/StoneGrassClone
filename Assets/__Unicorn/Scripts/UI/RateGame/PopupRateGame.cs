using Google.Play.Common;
using Google.Play.Review;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using MoreMountains.Tools;

[Singleton("Popup/PopupRateGame", true)]
public class PopupRateGame : Singleton<PopupRateGame>
{
    public bool isShow { get; set; }
    [SerializeField] private Button btnConfirm;
    [SerializeField] private List<Button> listBtnStar;
    [SerializeField] private List<Sprite> listSprStar;
    [SerializeField] private List<Image> listImgStar;

    private int star;

    public override void Awake()
    {
        base.Awake();

        if (!isDestroy)
        {
            Instance.Init();
        }
        if (!isShow)
        {
            Hide();
        }

        btnConfirm.onClick.AddListener(Hide);
    }

    private void Start()
    {
        for (int i = 0; i < listBtnStar.Count; i++)
        {
            int index = i + 1;
            listBtnStar[i].onClick.AddListener(() => { OnClickStar(index); });

            listImgStar[i].sprite = listSprStar[0];
        }
        star = 0;
        // btnConfirm.interactable = false;
    }

    public void Show()
    {
        isShow = true;
        gameObject.SetActive(true);

    }

    public void Hide()
    {
        if (star <= 0)
            return;

        if (star == 5)
        {
            //Application.OpenURL(RocketConfig.OPEN_LINK_RATE);
           StartCoroutine(RequestForReview());
        }else 
            gameObject.SetActive(false);

        if (gameObject == null)
            return;
        MMSoundManagerSoundPlayEvent.Trigger(MMSoundManagerMySeft.Ins._btnClickSound, MMSoundManager.MMSoundManagerTracks.UI, Camera.main.gameObject.transform.position);


    }

    private void OnClickStar(int index)
    {
        star = index;

        for (int i = 0; i < listImgStar.Count; i++)
        {
            listImgStar[i].sprite = listSprStar[0];
        }


        for (int i = 0; i < index; i++)
        {
            listImgStar[i].sprite = listSprStar[1];
        }
        MMSoundManagerSoundPlayEvent.Trigger(MMSoundManagerMySeft.Ins._btnClickSound, MMSoundManager.MMSoundManagerTracks.UI, Camera.main.gameObject.transform.position);


    }

    #region Review in app

    private ReviewManager _reviewManager;
    private PlayReviewInfo _playReviewInfo;

    private IEnumerator RequestForReview()
    {
        WaitingCanvas.Instance.Show("");
        _reviewManager = new Google.Play.Review.ReviewManager();
        //request object reqview flow
        var requestFlowOperation = _reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;
        if (requestFlowOperation.Error != Google.Play.Review.ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            Debug.Log(requestFlowOperation.Error.ToString());
            WaitingCanvas.Instance.Hide();

            gameObject.SetActive(false);
            yield break;
        }
        _playReviewInfo = requestFlowOperation.GetResult();

        //lauch request review
        var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
        yield return launchFlowOperation;
        _playReviewInfo = null;
        if (launchFlowOperation.Error != Google.Play.Review.ReviewErrorCode.NoError)
        {
            Debug.Log(requestFlowOperation.Error.ToString());
            WaitingCanvas.Instance.Hide();

            gameObject.SetActive(false);
            yield break;
        }
        
        WaitingCanvas.Instance.Hide();
        gameObject.SetActive(false);
    }

    #endregion
}
