using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Tools;
using DG.Tweening;

public class UiFactory : UICanvas
{
    [SerializeField] private Button btnBack;
    int showTime;
    public Animator _ani;

    public RectTransform rectTransform;
    public CanvasGroup canvas;

    public bool _isFactory;
    private void Start()
    {
        btnBack.onClick.AddListener(OnClickBack);
    }

    private void OnClickBack()
    {
        _ani.SetBool("_out", false);
        MMSoundManagerSoundPlayEvent.Trigger(MMSoundManagerMySeft.Ins._ClosePopUp, MMSoundManager.MMSoundManagerTracks.UI, Camera.main.gameObject.transform.position);
        canvas.DOFade(0f, UiController.Ins.FadeTime);
        rectTransform.DOAnchorPos(new Vector3(0, -1000f, 0), UiController.Ins.OutTime).OnComplete(Off);
    }

    private void Off()
    {
        Show(false);
    }

    public override void Show(bool _isShown, bool isHideMain = true)
    {
        base.Show(_isShown, isHideMain);
        if (!_isShown)
        {
            PlayerController.Ins._isInputAble = true;
            _isFactory = false;
            return;
        }
        canvas.alpha = 0;
        canvas.DOFade(1, UiController.Ins.FadeTime);
        PlayerController.Ins._isInputAble = false;
        _ani.SetBool("_out", true);
        ProductFactory.Ins.On();

        _isFactory = true;
        showTime++;
        if (showTime <= 1)
            return;
        ProductFactory.Ins.ResetBtnList();
    }
}
