using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;
using DG.Tweening;
using Exoa.TutorialEngine;

public class UIMap : UICanvas
{
    [SerializeField] private Button btnClose;
    [SerializeField] private Button btnIsLand;

    public float loadTime;
    public MMFeedbacks FlyToIsLandFeedBack;

    public static UIMap Ins;

    public RectTransform rectTransform;
    public CanvasGroup canvas;

    [SerializeField] List<levelSelect> levelSelects = new List<levelSelect>();
    public BuildBridgerTut _tut;
    
    #region singleton
    private void Awake()
    {
        Ins = this;
    }
    #endregion
    private void Start()
    {
        btnClose.onClick.AddListener(OnClickClose);
    }

    public ScrollRect scrollRect;
    public RectTransform contentPanel;

    public void SnapTo(RectTransform target)
    {
        Canvas.ForceUpdateCanvases();
        Vector2 pos = contentPanel.anchoredPosition;
        contentPanel.anchoredPosition =
                (Vector2)scrollRect.transform.InverseTransformPoint(contentPanel.position)
                - (Vector2)scrollRect.transform.InverseTransformPoint(target.position);

        contentPanel.anchoredPosition = new Vector2(0, contentPanel.anchoredPosition.y);
    }

    #region showIsLand
    public override void Show(bool _isShown, bool isHideMain = true)
    {
        if (PlayerDataManager.GetTutorialDone() != 1 && PlayerDataManager.GetStar(Helper.Stats + "Level" + 1.ToString()) >= 1 && _isShown)
        {
            _tut.step1();
            PlayerDataManager.SetTutorialDone(1);
        }
        base.Show(_isShown, isHideMain);
        canvas.alpha = 0;
        canvas.DOFade(1, UiController.Ins.FadeTime);
        SnapTo(levelSelects[PlayerDataManager.GetCrrLevel()].GetComponent<RectTransform>());
        if (!isShow)
            return;
        MMSoundManagerSoundPlayEvent.Trigger(MMSoundManagerMySeft.Ins._tabClickSound, MMSoundManager.MMSoundManagerTracks.UI, Camera.main.gameObject.transform.position);
        loadSelect();
    }

    public void TutorialIsOver()
    {

    }

    private void loadSelect()
    {
        //var dataLevel = GameManager.Instance.DataLevel;
        int level = PlayerDataManager.GetMaxLevel();
        for (int i = 0; i < level + 1; i++)
            levelSelects[i].UpdateLevelStatus();
    }
    
    #endregion

    #region OnClick
    public void OnClickClose()
    {
        OnBackPressed();
    }

    public override void OnBackPressed()
    {
        rectTransform.gameObject.transform.DOScale(1f, UiController.Ins.FadeTime).OnComplete(Off);
        canvas.DOFade(0, UiController.Ins.FadeTime);
    }

    private void Off()
    {
        PlayerController.Ins._isInputAble = true;
        Show(false);
    }
    #endregion

}
